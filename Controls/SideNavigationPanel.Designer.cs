namespace NeoSoft.UI.Controls
{
    partial class SideNavigationPanel
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.btnToggleMenu = new System.Windows.Forms.Button();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pnlTop.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.pnlTop.Controls.Add(this.menuStrip);
            this.pnlTop.Controls.Add(this.btnToggleMenu);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(700, 30);
            this.pnlTop.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip.ForeColor = System.Drawing.Color.White;
            this.menuStrip.Location = new System.Drawing.Point(40, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip.Size = new System.Drawing.Size(660, 30);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // btnToggleMenu
            // 
            this.btnToggleMenu.BackColor = System.Drawing.Color.Transparent;
            this.btnToggleMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnToggleMenu.FlatAppearance.BorderSize = 0;
            this.btnToggleMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleMenu.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnToggleMenu.ForeColor = System.Drawing.Color.White;
            this.btnToggleMenu.Location = new System.Drawing.Point(0, 0);
            this.btnToggleMenu.Name = "btnToggleMenu";
            this.btnToggleMenu.Size = new System.Drawing.Size(40, 30);
            this.btnToggleMenu.TabIndex = 0;
            this.btnToggleMenu.Text = "☰";
            this.btnToggleMenu.UseVisualStyleBackColor = false;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.pnlLeft.Controls.Add(this.pnlNavigation);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 30);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(250, 470);
            this.pnlLeft.TabIndex = 1;
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.AutoScroll = true;
            this.pnlNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.pnlNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavigation.Location = new System.Drawing.Point(0, 0);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(250, 470);
            this.pnlNavigation.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(250, 30);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(10);
            this.pnlContent.Size = new System.Drawing.Size(450, 470);
            this.pnlContent.TabIndex = 2;
            // 
            // contextMenu
            // 
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // SideNavigationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "SideNavigationPanel";
            this.Size = new System.Drawing.Size(700, 500);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Button btnToggleMenu;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
    }
}