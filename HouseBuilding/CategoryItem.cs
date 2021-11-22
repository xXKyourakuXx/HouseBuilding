namespace HouseBuilding
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class CategoryItem : UserControl
    {
        /// <summary>
        /// 'baseColor' stores the initial BackColor of the user control.
        /// After the mouse leaves the control, it resets its color to this.
        /// </summary>
        protected Color baseColor;

        /// <summary>
        /// 'Category' property stores the 'Item' object assigned to this control.
        /// It represents the Images and the name of the category.
        /// </summary>
        public MainCategoryItem Category { get; set; }

        /// <summary>
        /// There is an eventhandler which shows the prototype of the methods 
        /// which are assignable to this control's click event.
        /// When this control or its subcontrols are clicked, the OnClick method calles the event
        /// which is assigned where this object's instances are created and initialized.
        /// </summary>
        #region Click event management
        public delegate void ClickEventHandler(object sender, EventArgs e);
        public event ClickEventHandler CategoryClick;
        protected virtual void OnClick(object sender, EventArgs e)
            => CategoryClick?.Invoke(sender, e);
        #endregion Click event management

        /// <summary>
        /// The constructor initializes the Category property, the label text and the image.
        /// </summary>
        /// <param name="item">The Item object represents the list of the images and the name of the current category. 
        /// E.g. Door
        /// </param>
        public CategoryItem(MainCategoryItem item)
        {
            InitializeComponent();
            this.Category = item;
            this.labelName.Text = this.Category.Name;
            
            if (item.MainImage != null)
                this.pictureBoxImage.Image = Image.FromFile(item.MainImage);
            
            this.baseColor = this.BackColor;
        }

        #region BackColorManagement
        protected void SetColorOnMouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGreen;
            this.labelName.BackColor = this.BackColor;
        }
        protected void ResetColorOnMouseLeave(object sender, EventArgs e)
        {
            this.BackColor = this.baseColor;
            this.labelName.BackColor = this.BackColor;
        }
        #endregion BackColorManagement

        #region Call parent's OnClick when clicked
        protected void pictureBoxImage_Click(object sender, EventArgs e)
            => this.OnClick(this, e);
        protected void labelName_Click(object sender, EventArgs e)
            => this.OnClick(this, e);
        #endregion Call parent's OnClick when clicked
    }
}
