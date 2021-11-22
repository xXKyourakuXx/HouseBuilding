
namespace HouseBuilding
{
    partial class BuildingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHouse = new System.Windows.Forms.Panel();
            this.panelControls = new System.Windows.Forms.Panel();
            this.flowLayoutPanelSubControls = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelMainControls = new System.Windows.Forms.FlowLayoutPanel();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHouse
            // 
            this.panelHouse.BackColor = System.Drawing.SystemColors.Control;
            this.panelHouse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelHouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHouse.Location = new System.Drawing.Point(0, 0);
            this.panelHouse.Name = "panelHouse";
            this.panelHouse.Size = new System.Drawing.Size(1350, 729);
            this.panelHouse.TabIndex = 0;
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.Green;
            this.panelControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelControls.Controls.Add(this.flowLayoutPanelSubControls);
            this.panelControls.Controls.Add(this.flowLayoutPanelMainControls);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Margin = new System.Windows.Forms.Padding(0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(598, 729);
            this.panelControls.TabIndex = 1;
            // 
            // flowLayoutPanelSubControls
            // 
            this.flowLayoutPanelSubControls.AutoScroll = true;
            this.flowLayoutPanelSubControls.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelSubControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSubControls.Location = new System.Drawing.Point(0, 631);
            this.flowLayoutPanelSubControls.Name = "flowLayoutPanelSubControls";
            this.flowLayoutPanelSubControls.Size = new System.Drawing.Size(594, 94);
            this.flowLayoutPanelSubControls.TabIndex = 1;
            // 
            // flowLayoutPanelMainControls
            // 
            this.flowLayoutPanelMainControls.AutoScroll = true;
            this.flowLayoutPanelMainControls.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelMainControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanelMainControls.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelMainControls.Name = "flowLayoutPanelMainControls";
            this.flowLayoutPanelMainControls.Size = new System.Drawing.Size(594, 631);
            this.flowLayoutPanelMainControls.TabIndex = 0;
            // 
            // BuildingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.panelHouse);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1366, 768);
            this.Name = "BuildingForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Házépítés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHouse;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSubControls;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMainControls;
    }
}

