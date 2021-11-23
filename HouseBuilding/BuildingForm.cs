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
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
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

        public BuildingForm()
        {
            InitializeComponent();

            foreach (PictureBox b in this.panelHouse.Controls)
            {
                b.MouseMove += pictureBox_MouseMove;
                b.MouseDown += pictureBox_MouseDown;
            }
            
            #region Set parents for transparency issues
            this.pictureBoxChimney.Parent = this.pictureBoxRoof;
            this.pictureBoxWindowLeft.Parent = this.pictureBoxWall;
            this.pictureBoxWindowRight.Parent = this.pictureBoxWall;
            this.pictureBoxDoor.Parent = this.pictureBoxWall;
            this.pictureBoxBell.Parent = this.pictureBoxWall;
            this.pictureBoxDecoration.Parent = this.pictureBoxDoor;
            this.pictureBoxChristmasLights.Parent = this.pictureBoxWall;
            this.pictureBoxMat.Parent = this.pictureBoxGround;
            this.pictureBoxElf.Parent = this.pictureBoxFenceLeft;
            this.pictureBoxLetterBox.Parent = this.pictureBoxFenceRight;
            #endregion


            #region Set locations inside new parent controls
            this.pictureBoxChimney.Location = new Point(
                this.pictureBoxRoof.Width - this.pictureBoxChimney.Width - 20,
                30);

            this.pictureBoxWindowLeft.Location = new Point(
                this.pictureBoxWall.Width/2 - this.pictureBoxWindowLeft.Width - 50,
                80
                );
            this.pictureBoxWindowRight.Location = new Point(
                this.pictureBoxWall.Width - this.pictureBoxWindowLeft.Width - 20,
                this.pictureBoxWindowLeft.Location.Y
                );

            this.pictureBoxDoor.Location = new Point(
                this.pictureBoxWall.Width - this.pictureBoxDoor.Width - 30,
                this.pictureBoxWall.Height - this.pictureBoxDoor.Height + 15
                );

            this.pictureBoxBell.Location = new Point(
                this.pictureBoxDoor.Location.X - this.pictureBoxBell.Width - 10,
                this.pictureBoxWindowLeft.Location.Y + this.pictureBoxWindowLeft.Height + 10
                );

            this.pictureBoxDecoration.Location = new Point(
                this.pictureBoxDoor.Width /2,
                0
                );

            this.pictureBoxChristmasLights.Location = new Point(0);

            this.pictureBoxMat.Location = new Point(this.pictureBoxGround.Width / 2, 0);
            this.pictureBoxElf.Location = new Point(this.pictureBoxFenceLeft.Width - this.pictureBoxElf.Width, -10);
            this.pictureBoxFenceLeft.BringToFront() ;

            this.pictureBoxLetterBox.Location = new Point(30,30);
            #endregion


            this.Icon = Properties.Resources.house;
            
            this.InitializeCategories();            
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

                if (target.Name == "pictureBoxRoof" || target.Name.Contains("pictureBoxFence") || target.Name == "pictureBoxWall" || target.Name == "pictureBoxGround")
                    res = img.AlterTransparency(10);
                else 
                    res = img;

                target.Image = res;
                target.ImageLocation = source;
            }
        }
    }
}
