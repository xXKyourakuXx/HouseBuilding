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

        public BuildingForm()
        {
            InitializeComponent();
            this.pictureBoxChimney.Parent = this.pictureBoxRoof;
            this.pictureBoxChimney.Location = new Point(
                this.pictureBoxRoof.Width - this.pictureBoxChimney.Width - 20,
                30);
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
                target.Image = Image.FromFile(source);
                target.ImageLocation = source;
            }
        }
    }
}
