using System;
using System.Drawing;
using System.Windows.Forms;

namespace HouseBuilding
{
    public partial class SubCategoryItem : UserControl
    {
        /// <summary>
        /// 'baseColor' stores the initial BackColor of the user control.
        /// After the mouse leaves the control, it resets its color to this.
        /// </summary>
        protected Color baseColor;

        public SubCategoryItem(Item item)
        {
            InitializeComponent();
            this.pictureBoxImage.Image = Image.FromFile(item.MainImage);
        }

        #region BackColorManagement
        protected void SetColorOnMouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGreen;
        }
        protected void ResetColorOnMouseLeave(object sender, EventArgs e)
        {
            this.BackColor = this.baseColor;
        }
        #endregion BackColorManagement

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

        #region Call parent's OnClick when clicked
        protected void pictureBoxImage_Click(object sender, EventArgs e)
            => this.OnClick(this, e);
        protected void labelName_Click(object sender, EventArgs e)
            => this.OnClick(this, e);
        #endregion Call parent's OnClick when clicked
    }
}
