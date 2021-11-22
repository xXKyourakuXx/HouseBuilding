namespace HouseBuilding
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public partial class BuildingForm : Form
    {
        IList<MainCategoryItem> items;

        public BuildingForm()
        {
            InitializeComponent();
            
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
            /// TODO: Add pictureboxes to the form and insert the clicked image to it.
        }
    }
}
