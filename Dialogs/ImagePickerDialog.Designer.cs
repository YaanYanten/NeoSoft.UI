namespace NeoSoft.UI.Dialogs
{
    partial class ImagePickerDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tabControl = new System.Windows.Forms.TabControl();
            tabImagePicker = new System.Windows.Forms.TabPage();
            panelPreview = new System.Windows.Forms.Panel();
            picPreview = new System.Windows.Forms.PictureBox();
            panelLeft = new System.Windows.Forms.Panel();
            grpResourceContext = new System.Windows.Forms.GroupBox();
            panelProjectResource = new System.Windows.Forms.Panel();
            btnImportProject = new System.Windows.Forms.Button();
            txtProjectResource = new System.Windows.Forms.TextBox();
            lblProjectResource = new System.Windows.Forms.Label();
            rbProjectResource = new System.Windows.Forms.RadioButton();
            panelLocalResource = new System.Windows.Forms.Panel();
            btnImportLocal = new System.Windows.Forms.Button();
            btnClearLocal = new System.Windows.Forms.Button();
            lstLocalResources = new System.Windows.Forms.ListBox();
            rbLocalResource = new System.Windows.Forms.RadioButton();
            tabRasterImages = new System.Windows.Forms.TabPage();
            flowRasterIcons = new System.Windows.Forms.FlowLayoutPanel();
            panelRasterFilters = new System.Windows.Forms.Panel();
            lblCategories = new System.Windows.Forms.Label();
            chkListCategories = new System.Windows.Forms.CheckedListBox();
            lblSize = new System.Windows.Forms.Label();
            chkListSize = new System.Windows.Forms.CheckedListBox();
            rbAddToForm = new System.Windows.Forms.RadioButton();
            rbAddToProject = new System.Windows.Forms.RadioButton();
            tabVectorImages = new System.Windows.Forms.TabPage();
            tabFontIcons = new System.Windows.Forms.TabPage();
            lblVersion = new System.Windows.Forms.Label();
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            txtRasterSearch = new System.Windows.Forms.TextBox();
            tabControl.SuspendLayout();
            tabImagePicker.SuspendLayout();
            panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            panelLeft.SuspendLayout();
            grpResourceContext.SuspendLayout();
            panelProjectResource.SuspendLayout();
            panelLocalResource.SuspendLayout();
            tabRasterImages.SuspendLayout();
            panelRasterFilters.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl.Controls.Add(tabImagePicker);
            tabControl.Controls.Add(tabRasterImages);
            tabControl.Controls.Add(tabVectorImages);
            tabControl.Controls.Add(tabFontIcons);
            tabControl.Location = new System.Drawing.Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(660, 520);
            tabControl.TabIndex = 0;
            // 
            // tabImagePicker
            // 
            tabImagePicker.Controls.Add(panelPreview);
            tabImagePicker.Controls.Add(panelLeft);
            tabImagePicker.Location = new System.Drawing.Point(4, 24);
            tabImagePicker.Name = "tabImagePicker";
            tabImagePicker.Padding = new System.Windows.Forms.Padding(3);
            tabImagePicker.Size = new System.Drawing.Size(652, 492);
            tabImagePicker.TabIndex = 0;
            tabImagePicker.Text = "Image Picker";
            tabImagePicker.UseVisualStyleBackColor = true;
            // 
            // panelPreview
            // 
            panelPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelPreview.BackColor = System.Drawing.Color.Gray;
            panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelPreview.Controls.Add(picPreview);
            panelPreview.Location = new System.Drawing.Point(290, 10);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new System.Drawing.Size(350, 470);
            panelPreview.TabIndex = 1;
            // 
            // picPreview
            // 
            picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            picPreview.Location = new System.Drawing.Point(0, 0);
            picPreview.Name = "picPreview";
            picPreview.Size = new System.Drawing.Size(348, 468);
            picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            picPreview.TabIndex = 0;
            picPreview.TabStop = false;
            // 
            // panelLeft
            // 
            panelLeft.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            panelLeft.Controls.Add(grpResourceContext);
            panelLeft.Location = new System.Drawing.Point(10, 10);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new System.Drawing.Size(270, 470);
            panelLeft.TabIndex = 0;
            // 
            // grpResourceContext
            // 
            grpResourceContext.Controls.Add(panelProjectResource);
            grpResourceContext.Controls.Add(rbProjectResource);
            grpResourceContext.Controls.Add(panelLocalResource);
            grpResourceContext.Controls.Add(rbLocalResource);
            grpResourceContext.Dock = System.Windows.Forms.DockStyle.Fill;
            grpResourceContext.Location = new System.Drawing.Point(0, 0);
            grpResourceContext.Name = "grpResourceContext";
            grpResourceContext.Size = new System.Drawing.Size(270, 470);
            grpResourceContext.TabIndex = 0;
            grpResourceContext.TabStop = false;
            grpResourceContext.Text = "Resource context";
            // 
            // panelProjectResource
            // 
            panelProjectResource.Controls.Add(btnImportProject);
            panelProjectResource.Controls.Add(txtProjectResource);
            panelProjectResource.Controls.Add(lblProjectResource);
            panelProjectResource.Enabled = false;
            panelProjectResource.Location = new System.Drawing.Point(15, 310);
            panelProjectResource.Name = "panelProjectResource";
            panelProjectResource.Size = new System.Drawing.Size(240, 150);
            panelProjectResource.TabIndex = 3;
            // 
            // btnImportProject
            // 
            btnImportProject.Location = new System.Drawing.Point(10, 110);
            btnImportProject.Name = "btnImportProject";
            btnImportProject.Size = new System.Drawing.Size(100, 30);
            btnImportProject.TabIndex = 2;
            btnImportProject.Text = "Import...";
            btnImportProject.UseVisualStyleBackColor = true;
            // 
            // txtProjectResource
            // 
            txtProjectResource.Location = new System.Drawing.Point(10, 30);
            txtProjectResource.Multiline = true;
            txtProjectResource.Name = "txtProjectResource";
            txtProjectResource.ReadOnly = true;
            txtProjectResource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtProjectResource.Size = new System.Drawing.Size(220, 70);
            txtProjectResource.TabIndex = 1;
            txtProjectResource.Text = "My.Project.Resources.resx";
            // 
            // lblProjectResource
            // 
            lblProjectResource.AutoSize = true;
            lblProjectResource.ForeColor = System.Drawing.SystemColors.GrayText;
            lblProjectResource.Location = new System.Drawing.Point(10, 10);
            lblProjectResource.Name = "lblProjectResource";
            lblProjectResource.Size = new System.Drawing.Size(0, 15);
            lblProjectResource.TabIndex = 0;
            // 
            // rbProjectResource
            // 
            rbProjectResource.AutoSize = true;
            rbProjectResource.Location = new System.Drawing.Point(15, 285);
            rbProjectResource.Name = "rbProjectResource";
            rbProjectResource.Size = new System.Drawing.Size(110, 19);
            rbProjectResource.TabIndex = 2;
            rbProjectResource.Text = "Project resource";
            rbProjectResource.UseVisualStyleBackColor = true;
            // 
            // panelLocalResource
            // 
            panelLocalResource.Controls.Add(btnImportLocal);
            panelLocalResource.Controls.Add(btnClearLocal);
            panelLocalResource.Controls.Add(lstLocalResources);
            panelLocalResource.Location = new System.Drawing.Point(15, 45);
            panelLocalResource.Name = "panelLocalResource";
            panelLocalResource.Size = new System.Drawing.Size(240, 230);
            panelLocalResource.TabIndex = 1;
            // 
            // btnImportLocal
            // 
            btnImportLocal.Location = new System.Drawing.Point(10, 190);
            btnImportLocal.Name = "btnImportLocal";
            btnImportLocal.Size = new System.Drawing.Size(100, 30);
            btnImportLocal.TabIndex = 2;
            btnImportLocal.Text = "Import...";
            btnImportLocal.UseVisualStyleBackColor = true;
            // 
            // btnClearLocal
            // 
            btnClearLocal.Location = new System.Drawing.Point(120, 190);
            btnClearLocal.Name = "btnClearLocal";
            btnClearLocal.Size = new System.Drawing.Size(100, 30);
            btnClearLocal.TabIndex = 1;
            btnClearLocal.Text = "Clear";
            btnClearLocal.UseVisualStyleBackColor = true;
            // 
            // lstLocalResources
            // 
            lstLocalResources.FormattingEnabled = true;
            lstLocalResources.ItemHeight = 15;
            lstLocalResources.Location = new System.Drawing.Point(10, 10);
            lstLocalResources.Name = "lstLocalResources";
            lstLocalResources.Size = new System.Drawing.Size(220, 169);
            lstLocalResources.TabIndex = 0;
            // 
            // rbLocalResource
            // 
            rbLocalResource.AutoSize = true;
            rbLocalResource.Checked = true;
            rbLocalResource.Location = new System.Drawing.Point(15, 25);
            rbLocalResource.Name = "rbLocalResource";
            rbLocalResource.Size = new System.Drawing.Size(101, 19);
            rbLocalResource.TabIndex = 0;
            rbLocalResource.TabStop = true;
            rbLocalResource.Text = "Local resource";
            rbLocalResource.UseVisualStyleBackColor = true;
            // 
            // tabRasterImages
            // 
            tabRasterImages.Controls.Add(flowRasterIcons);
            tabRasterImages.Controls.Add(panelRasterFilters);
            tabRasterImages.Controls.Add(rbAddToForm);
            tabRasterImages.Controls.Add(rbAddToProject);
            tabRasterImages.Location = new System.Drawing.Point(4, 24);
            tabRasterImages.Name = "tabRasterImages";
            tabRasterImages.Padding = new System.Windows.Forms.Padding(3);
            tabRasterImages.Size = new System.Drawing.Size(652, 492);
            tabRasterImages.TabIndex = 1;
            tabRasterImages.Text = "Raster Images";
            tabRasterImages.UseVisualStyleBackColor = true;
            // 
            // flowRasterIcons
            // 
            flowRasterIcons.AutoScroll = true;
            flowRasterIcons.BackColor = System.Drawing.Color.White;
            flowRasterIcons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowRasterIcons.Location = new System.Drawing.Point(230, 40);
            flowRasterIcons.Name = "flowRasterIcons";
            flowRasterIcons.Size = new System.Drawing.Size(410, 420);
            flowRasterIcons.TabIndex = 2;
            // 
            // panelRasterFilters
            // 
            panelRasterFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelRasterFilters.Controls.Add(lblCategories);
            panelRasterFilters.Controls.Add(chkListCategories);
            panelRasterFilters.Controls.Add(lblSize);
            panelRasterFilters.Controls.Add(chkListSize);
            panelRasterFilters.Location = new System.Drawing.Point(10, 40);
            panelRasterFilters.Name = "panelRasterFilters";
            panelRasterFilters.Size = new System.Drawing.Size(210, 420);
            panelRasterFilters.TabIndex = 0;
            // 
            // lblCategories
            // 
            lblCategories.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblCategories.Location = new System.Drawing.Point(5, 5);
            lblCategories.Name = "lblCategories";
            lblCategories.Size = new System.Drawing.Size(190, 15);
            lblCategories.TabIndex = 0;
            lblCategories.Text = "Categories";
            // 
            // chkListCategories
            // 
            chkListCategories.CheckOnClick = true;
            chkListCategories.Items.AddRange(new object[] { "Select All", "Actions", "Alignment", "Analysis", "Appearance", "Arrange", "Arrows", "Business Objects", "Chart" });
            chkListCategories.Location = new System.Drawing.Point(5, 25);
            chkListCategories.Name = "chkListCategories";
            chkListCategories.Size = new System.Drawing.Size(195, 292);
            chkListCategories.TabIndex = 0;
            // 
            // lblSize
            // 
            lblSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblSize.Location = new System.Drawing.Point(5, 328);
            lblSize.Name = "lblSize";
            lblSize.Size = new System.Drawing.Size(190, 15);
            lblSize.TabIndex = 1;
            lblSize.Text = "Size";
            // 
            // chkListSize
            // 
            chkListSize.CheckOnClick = true;
            chkListSize.Items.AddRange(new object[] { "16x16", "32x32" });
            chkListSize.Location = new System.Drawing.Point(5, 348);
            chkListSize.Name = "chkListSize";
            chkListSize.Size = new System.Drawing.Size(195, 40);
            chkListSize.TabIndex = 1;
            // 
            // rbAddToForm
            // 
            rbAddToForm.Checked = true;
            rbAddToForm.Location = new System.Drawing.Point(230, 467);
            rbAddToForm.Name = "rbAddToForm";
            rbAddToForm.Size = new System.Drawing.Size(160, 20);
            rbAddToForm.TabIndex = 4;
            rbAddToForm.TabStop = true;
            rbAddToForm.Text = "Add to form resources";
            // 
            // rbAddToProject
            // 
            rbAddToProject.Location = new System.Drawing.Point(50, 467);
            rbAddToProject.Name = "rbAddToProject";
            rbAddToProject.Size = new System.Drawing.Size(160, 20);
            rbAddToProject.TabIndex = 3;
            rbAddToProject.Text = "Add to project resources";
            // 
            // tabVectorImages
            // 
            tabVectorImages.Location = new System.Drawing.Point(4, 24);
            tabVectorImages.Name = "tabVectorImages";
            tabVectorImages.Size = new System.Drawing.Size(652, 492);
            tabVectorImages.TabIndex = 2;
            tabVectorImages.Text = "Vector Images";
            tabVectorImages.UseVisualStyleBackColor = true;
            // 
            // tabFontIcons
            // 
            tabFontIcons.Location = new System.Drawing.Point(4, 24);
            tabFontIcons.Name = "tabFontIcons";
            tabFontIcons.Size = new System.Drawing.Size(652, 492);
            tabFontIcons.TabIndex = 3;
            tabFontIcons.Text = "Font Icons";
            tabFontIcons.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblVersion.AutoSize = true;
            lblVersion.ForeColor = System.Drawing.Color.Gray;
            lblVersion.Location = new System.Drawing.Point(12, 548);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new System.Drawing.Size(87, 15);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "Version 24.1.7.0";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(582, 543);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(85, 30);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnOK.Location = new System.Drawing.Point(487, 543);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(85, 30);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += BtnOK_Click;
            // 
            // txtRasterSearch
            // 
            txtRasterSearch.ForeColor = System.Drawing.Color.Gray;
            txtRasterSearch.Location = new System.Drawing.Point(430, 10);
            txtRasterSearch.Name = "txtRasterSearch";
            txtRasterSearch.Size = new System.Drawing.Size(200, 23);
            txtRasterSearch.TabIndex = 1;
            txtRasterSearch.Text = "Enter text to search...";
            // 
            // ImagePickerDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(684, 591);
            Controls.Add(lblVersion);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(tabControl);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ImagePickerDialog";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Image Picker";
            tabControl.ResumeLayout(false);
            tabImagePicker.ResumeLayout(false);
            panelPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            panelLeft.ResumeLayout(false);
            grpResourceContext.ResumeLayout(false);
            grpResourceContext.PerformLayout();
            panelProjectResource.ResumeLayout(false);
            panelProjectResource.PerformLayout();
            panelLocalResource.ResumeLayout(false);
            tabRasterImages.ResumeLayout(false);
            panelRasterFilters.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabImagePicker;
        private System.Windows.Forms.TabPage tabRasterImages;
        private System.Windows.Forms.TabPage tabVectorImages;
        private System.Windows.Forms.TabPage tabFontIcons;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox grpResourceContext;
        private System.Windows.Forms.RadioButton rbLocalResource;
        private System.Windows.Forms.Panel panelLocalResource;
        private System.Windows.Forms.ListBox lstLocalResources;
        private System.Windows.Forms.Button btnClearLocal;
        private System.Windows.Forms.Button btnImportLocal;
        private System.Windows.Forms.RadioButton rbProjectResource;
        private System.Windows.Forms.Panel panelProjectResource;
        private System.Windows.Forms.TextBox txtProjectResource;
        private System.Windows.Forms.Label lblProjectResource;
        private System.Windows.Forms.Button btnImportProject;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblVersion;

        // Raster Images tab controls
        private System.Windows.Forms.Panel panelRasterFilters;
        private System.Windows.Forms.TextBox txtRasterSearch;
        private System.Windows.Forms.CheckedListBox chkListCategories;
        private System.Windows.Forms.CheckedListBox chkListSize;
        private System.Windows.Forms.FlowLayoutPanel flowRasterIcons;
        private System.Windows.Forms.RadioButton rbAddToProject;
        private System.Windows.Forms.RadioButton rbAddToForm;
        private System.Windows.Forms.Label lblCategories;
        private System.Windows.Forms.Label lblSize;
    }
}
