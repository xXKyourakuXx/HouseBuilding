namespace HouseBuilding
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;
    using System.Drawing;

    public partial class BuildingForm : Form
    {
        IList<MainCategoryItem> items;

        private Point MouseDownLocation;

        /// <summary>
        /// To resolv flickering issues.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        /// <summary>
        /// To enable user to move form controls (mainly pictureboxes).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseDownLocation = e.Location;
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.panelHouse.SuspendLayout();
            if (e.Button == MouseButtons.Left)
            {
                PictureBox src = sender as PictureBox;
                src.Left = e.X + src.Left - MouseDownLocation.X;
                src.Top = e.Y + src.Top - MouseDownLocation.Y;
            }
            this.panelHouse.ResumeLayout(false);
            this.panelHouse.PerformLayout();
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        public BuildingForm()
        {
            InitializeComponent();
            
            this.SuspendLayout();

            foreach (Control c in GetAll(this.panelHouse, typeof(PictureBox)))
            {
                c.MouseMove += pictureBox_MouseMove;
                c.MouseDown += pictureBox_MouseDown;
            }
            
            #region Set parents for transparency issues
            this.pictureBoxChimney.Parent = this.pictureBoxRoof;
            this.pictureBoxWindowLeft.Parent = this.pictureBoxWall;
            this.pictureBoxWindowRight.Parent = this.pictureBoxWall;
            this.pictureBoxDoor.Parent = this.pictureBoxWall;
            this.pictureBoxBell.Parent = this.pictureBoxWall;
            this.pictureBoxDecoration.Parent = this.pictureBoxDoor;
            this.pictureBoxChristmasLights.Parent = this.pictureBoxWall;
            this.pictureBoxElf.Parent = this.pictureBoxFenceLeft;
            this.pictureBoxLetterBox.Parent = this.pictureBoxFenceLeft;
            this.pictureBoxReindeer.Parent = this.pictureBoxTree;
            #endregion


            #region Set locations inside new parent controls
            this.pictureBoxChimney.Location = new Point(
                this.pictureBoxRoof.Width - this.pictureBoxChimney.Width - 20,
                30);

            this.pictureBoxDoor.Location = new Point(
                (this.pictureBoxWall.Width - this.pictureBoxDoor.Width)/2,
                this.pictureBoxWall.Height - this.pictureBoxDoor.Height + 15
                );

            this.pictureBoxBell.Location = new Point(
                this.pictureBoxDoor.Location.X - this.pictureBoxBell.Width - 10,
                300
                );

            this.pictureBoxDecoration.Location = new Point(
                (this.pictureBoxDoor.Width - this.pictureBoxDecoration.Width) /2,
                0
                );

            this.pictureBoxChristmasLights.Location = new Point(0,75);

            this.pictureBoxWindowLeft.Location = new Point(
                    50,
                    this.pictureBoxChristmasLights.Location.Y + this.pictureBoxChristmasLights.Height + 5
                );
            this.pictureBoxWindowRight.Location = new Point(
                this.pictureBoxWall.Width - 50 - this.pictureBoxWindowRight.Width,
                this.pictureBoxWindowLeft.Location.Y
                );

            this.pictureBoxMat.Location = new Point(this.panelHouse.Width / 2, this.pictureBoxWall.Height + this.pictureBoxWall.Location.Y + 5);
            this.pictureBoxElf.Location = new Point(this.pictureBoxFenceLeft.Width - this.pictureBoxElf.Width, -10);
            this.pictureBoxFenceLeft.BringToFront();

            this.pictureBoxLetterBox.Location = new Point(30,30);

            this.pictureBoxReindeer.Location = new Point(
                this.pictureBoxTree.Width - this.pictureBoxReindeer.Width,
                this.pictureBoxTree.Height - this.pictureBoxReindeer.Height
                );

            this.pictureBoxElf.Location = new Point(
                this.pictureBoxFenceLeft.Width - this.pictureBoxElf.Width,
                this.pictureBoxFenceLeft.Height - this.pictureBoxElf.Height
                );
            this.pictureBoxLetterBox.Location = new Point(
                this.pictureBoxElf.Location.X - this.pictureBoxLetterBox.Width,
                this.pictureBoxFenceLeft.Height - this.pictureBoxLetterBox.Height
                );
            #endregion

            this.Icon = Properties.Resources.house;
            
            this.ResumeLayout(false);
            Application.DoEvents();

            this.InitializeCategories();
            this.buttonBorders_Click(this.buttonBorders as object, EventArgs.Empty);
        }

        private void InitializeCategories()
        {
            items = DataController.LoadData();
            foreach (var i in items)
            {
                var tmp = new CategoryItem(i);
                tmp.CategoryClick += OnCategoryClick;
                this.flowLayoutPanelMainControls.Controls.Add(tmp);
            }
        }
        private void InitializeSubCategories(CategoryItem item)
        {
            this.SuspendLayout();

            this.flowLayoutPanelSubControls.Controls.Clear();

            string name = item.Category.Name;
            foreach (var i in item.Category.Images)
            {
                var tmp = new SubCategoryItem(new Item { MainImage = i});
                tmp.CategoryClick += OnCategoryItemClick;
                this.flowLayoutPanelSubControls.Controls.Add(tmp);
            }

            this.ResumeLayout();
        }

        private void OnCategoryClick(object sender, EventArgs e)
            => this.InitializeSubCategories(sender as CategoryItem);
        private void OnCategoryItemClick(object sender, EventArgs e)
        {
            SubCategoryItem source = sender as SubCategoryItem;

            var names = DataController.GetLocation(source);

            SetImage(names.Item1, source.Item.MainImage);
            SetImage(names.Item2, source.Item.MainImage);
        }

        private void SetImage(string name, string source)
        {
            if (name == null || name == string.Empty)
                return;

            PictureBox target = this.panelHouse.Controls.Find(name, true).FirstOrDefault() as PictureBox;
            
            if (target.ImageLocation != null && target.ImageLocation.Equals(source))
            {
                target.Image = null;
                target.ImageLocation = null;
            }
            else
            {
                Bitmap img = new Bitmap(source);
                Bitmap res = null;

                if ( NeedTransparencyChange(target.Name) )
                    res = img.AlterTransparency(10);
                else 
                    res = img;

                target.Image = res;
                target.ImageLocation = source;
            }
        }

        private bool NeedTransparencyChange(string name)
        {
            return (
                name == "pictureBoxRoof" || 
                name.Contains("pictureBoxFence") || 
                name == "pictureBoxWall" || 
                name == "pictureBoxGround" || 
                name == "pictureBoxFenceLeft");
        }

        private void SetBorders(BorderStyle style)
        {
            foreach (PictureBox p in GetAll(this.panelHouse, typeof(PictureBox)))
                p.BorderStyle = style;
        }
        private void RemoveBorders()
            => this.SetBorders(BorderStyle.None);

        private void AddBorders()
            => this.SetBorders(BorderStyle.Fixed3D);

        private void buttonBorders_Click(object sender, EventArgs e)
        {
            if (this.pictureBoxBell.BorderStyle.Equals(BorderStyle.None))
                this.AddBorders();
            else
                this.RemoveBorders();
        }

        private void checkBoxBG_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
                this.panelHouse.BackColor = Color.FromArgb(239, 228, 176);
            else
                this.panelHouse.BackColor = Color.FromName("Control");
        }
    }
}
