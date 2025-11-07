namespace NeoSoft.UI.Dialogs
{
    partial class ImagePickerDialog
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
            tabControl = new System.Windows.Forms.TabControl();
            tabImagePicker = new System.Windows.Forms.TabPage();
            rbProjectResources = new System.Windows.Forms.RadioButton();
            rbFormResources = new System.Windows.Forms.RadioButton();
            flowIcons = new System.Windows.Forms.FlowLayoutPanel();
            lblIcons = new System.Windows.Forms.Label();
            lstCategories = new System.Windows.Forms.ListBox();
            lblCategories = new System.Windows.Forms.Label();
            panelTop = new System.Windows.Forms.Panel();
            cmbSize = new System.Windows.Forms.ComboBox();
            lblSize = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            tabRasterImages = new System.Windows.Forms.TabPage();
            btnClearRaster = new System.Windows.Forms.Button();
            btnImportRaster = new System.Windows.Forms.Button();
            lblRasterInfo = new System.Windows.Forms.Label();
            tabVectorImages = new System.Windows.Forms.TabPage();
            btnImportVector = new System.Windows.Forms.Button();
            lblVectorInfo = new System.Windows.Forms.Label();
            tabFontIcons = new System.Windows.Forms.TabPage();
            lblFontNote = new System.Windows.Forms.Label();
            flowFontIcons = new System.Windows.Forms.FlowLayoutPanel();
            lblFontTitle = new System.Windows.Forms.Label();
            lblVersion = new System.Windows.Forms.Label();
            btnReset = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            tabControl.SuspendLayout();
            tabImagePicker.SuspendLayout();
            panelTop.SuspendLayout();
            tabRasterImages.SuspendLayout();
            tabVectorImages.SuspendLayout();
            tabFontIcons.SuspendLayout();
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
            tabControl.Size = new System.Drawing.Size(680, 530);
            tabControl.TabIndex = 0;
            // 
            // tabImagePicker
            // 
            tabImagePicker.Controls.Add(rbProjectResources);
            tabImagePicker.Controls.Add(rbFormResources);
            tabImagePicker.Controls.Add(flowIcons);
            tabImagePicker.Controls.Add(lblIcons);
            tabImagePicker.Controls.Add(lstCategories);
            tabImagePicker.Controls.Add(lblCategories);
            tabImagePicker.Controls.Add(panelTop);
            tabImagePicker.Location = new System.Drawing.Point(4, 24);
            tabImagePicker.Name = "tabImagePicker";
            tabImagePicker.Padding = new System.Windows.Forms.Padding(3);
            tabImagePicker.Size = new System.Drawing.Size(672, 502);
            tabImagePicker.TabIndex = 0;
            tabImagePicker.Text = "Image Picker";
            tabImagePicker.UseVisualStyleBackColor = true;
            // 
            // rbProjectResources
            // 
            rbProjectResources.AutoSize = true;
            rbProjectResources.Location = new System.Drawing.Point(440, 460);
            rbProjectResources.Name = "rbProjectResources";
            rbProjectResources.Size = new System.Drawing.Size(154, 19);
            rbProjectResources.TabIndex = 6;
            rbProjectResources.Text = "Add to project resources";
            rbProjectResources.UseVisualStyleBackColor = true;
            // 
            // rbFormResources
            // 
            rbFormResources.AutoSize = true;
            rbFormResources.Checked = true;
            rbFormResources.Location = new System.Drawing.Point(220, 460);
            rbFormResources.Name = "rbFormResources";
            rbFormResources.Size = new System.Drawing.Size(143, 19);
            rbFormResources.TabIndex = 5;
            rbFormResources.TabStop = true;
            rbFormResources.Text = "Add to form resources";
            rbFormResources.UseVisualStyleBackColor = true;
            // 
            // flowIcons
            // 
            flowIcons.AutoScroll = true;
            flowIcons.BackColor = System.Drawing.Color.White;
            flowIcons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowIcons.Location = new System.Drawing.Point(220, 70);
            flowIcons.Name = "flowIcons";
            flowIcons.Size = new System.Drawing.Size(440, 380);
            flowIcons.TabIndex = 4;
            // 
            // lblIcons
            // 
            lblIcons.AutoSize = true;
            lblIcons.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblIcons.Location = new System.Drawing.Point(220, 45);
            lblIcons.Name = "lblIcons";
            lblIcons.Size = new System.Drawing.Size(36, 15);
            lblIcons.TabIndex = 3;
            lblIcons.Text = "Icons";
            // 
            // lstCategories
            // 
            lstCategories.FormattingEnabled = true;
            lstCategories.ItemHeight = 15;
            lstCategories.Location = new System.Drawing.Point(10, 70);
            lstCategories.Name = "lstCategories";
            lstCategories.Size = new System.Drawing.Size(200, 379);
            lstCategories.TabIndex = 2;
            // 
            // lblCategories
            // 
            lblCategories.AutoSize = true;
            lblCategories.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblCategories.Location = new System.Drawing.Point(10, 45);
            lblCategories.Name = "lblCategories";
            lblCategories.Size = new System.Drawing.Size(66, 15);
            lblCategories.TabIndex = 1;
            lblCategories.Text = "Categories";
            // 
            // panelTop
            // 
            panelTop.Controls.Add(cmbSize);
            panelTop.Controls.Add(lblSize);
            panelTop.Controls.Add(txtSearch);
            panelTop.Location = new System.Drawing.Point(10, 7);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(440, 35);
            panelTop.TabIndex = 0;
            // 
            // cmbSize
            // 
            cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSize.FormattingEnabled = true;
            cmbSize.Items.AddRange(new object[] { "All", "16x16", "32x32", "48x48" });
            cmbSize.Location = new System.Drawing.Point(310, 5);
            cmbSize.Name = "cmbSize";
            cmbSize.Size = new System.Drawing.Size(115, 23);
            cmbSize.TabIndex = 2;
            // 
            // lblSize
            // 
            lblSize.Location = new System.Drawing.Point(270, 8);
            lblSize.Name = "lblSize";
            lblSize.Size = new System.Drawing.Size(35, 20);
            lblSize.TabIndex = 1;
            lblSize.Text = "Size:";
            lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSearch
            // 
            txtSearch.ForeColor = System.Drawing.Color.Gray;
            txtSearch.Location = new System.Drawing.Point(3, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(250, 23);
            txtSearch.TabIndex = 0;
            txtSearch.Text = "Search icons...";
            // 
            // tabRasterImages
            // 
            tabRasterImages.Controls.Add(btnClearRaster);
            tabRasterImages.Controls.Add(btnImportRaster);
            tabRasterImages.Controls.Add(lblRasterInfo);
            tabRasterImages.Location = new System.Drawing.Point(4, 24);
            tabRasterImages.Name = "tabRasterImages";
            tabRasterImages.Padding = new System.Windows.Forms.Padding(3);
            tabRasterImages.Size = new System.Drawing.Size(672, 502);
            tabRasterImages.TabIndex = 1;
            tabRasterImages.Text = "Raster Images";
            tabRasterImages.UseVisualStyleBackColor = true;
            // 
            // btnClearRaster
            // 
            btnClearRaster.Location = new System.Drawing.Point(150, 50);
            btnClearRaster.Name = "btnClearRaster";
            btnClearRaster.Size = new System.Drawing.Size(120, 35);
            btnClearRaster.TabIndex = 2;
            btnClearRaster.Text = "Clear Selection";
            btnClearRaster.UseVisualStyleBackColor = true;
            // 
            // btnImportRaster
            // 
            btnImportRaster.Location = new System.Drawing.Point(20, 50);
            btnImportRaster.Name = "btnImportRaster";
            btnImportRaster.Size = new System.Drawing.Size(120, 35);
            btnImportRaster.TabIndex = 1;
            btnImportRaster.Text = "Import Image...";
            btnImportRaster.UseVisualStyleBackColor = true;
            // 
            // lblRasterInfo
            // 
            lblRasterInfo.AutoSize = true;
            lblRasterInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblRasterInfo.Location = new System.Drawing.Point(20, 20);
            lblRasterInfo.Name = "lblRasterInfo";
            lblRasterInfo.Size = new System.Drawing.Size(219, 15);
            lblRasterInfo.TabIndex = 0;
            lblRasterInfo.Text = "Import raster images (PNG, JPG, BMP)";
            // 
            // tabVectorImages
            // 
            tabVectorImages.Controls.Add(btnImportVector);
            tabVectorImages.Controls.Add(lblVectorInfo);
            tabVectorImages.Location = new System.Drawing.Point(4, 24);
            tabVectorImages.Name = "tabVectorImages";
            tabVectorImages.Size = new System.Drawing.Size(672, 502);
            tabVectorImages.TabIndex = 2;
            tabVectorImages.Text = "Vector Images";
            tabVectorImages.UseVisualStyleBackColor = true;
            // 
            // btnImportVector
            // 
            btnImportVector.Enabled = false;
            btnImportVector.Location = new System.Drawing.Point(20, 50);
            btnImportVector.Name = "btnImportVector";
            btnImportVector.Size = new System.Drawing.Size(120, 35);
            btnImportVector.TabIndex = 1;
            btnImportVector.Text = "Import SVG...";
            btnImportVector.UseVisualStyleBackColor = true;
            // 
            // lblVectorInfo
            // 
            lblVectorInfo.AutoSize = true;
            lblVectorInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            lblVectorInfo.ForeColor = System.Drawing.Color.Gray;
            lblVectorInfo.Location = new System.Drawing.Point(20, 20);
            lblVectorInfo.Name = "lblVectorInfo";
            lblVectorInfo.Size = new System.Drawing.Size(228, 15);
            lblVectorInfo.TabIndex = 0;
            lblVectorInfo.Text = "Import vector images (SVG) - Coming soon";
            // 
            // tabFontIcons
            // 
            tabFontIcons.Controls.Add(lblFontNote);
            tabFontIcons.Controls.Add(flowFontIcons);
            tabFontIcons.Controls.Add(lblFontTitle);
            tabFontIcons.Location = new System.Drawing.Point(4, 24);
            tabFontIcons.Name = "tabFontIcons";
            tabFontIcons.Size = new System.Drawing.Size(672, 502);
            tabFontIcons.TabIndex = 3;
            tabFontIcons.Text = "Font Icons";
            tabFontIcons.UseVisualStyleBackColor = true;
            // 
            // lblFontNote
            // 
            lblFontNote.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            lblFontNote.ForeColor = System.Drawing.Color.Gray;
            lblFontNote.Location = new System.Drawing.Point(20, 450);
            lblFontNote.Name = "lblFontNote";
            lblFontNote.Size = new System.Drawing.Size(600, 30);
            lblFontNote.TabIndex = 2;
            lblFontNote.Text = "NOTE: Font icons are system-dependent and may not display correctly on all systems.";
            // 
            // flowFontIcons
            // 
            flowFontIcons.AutoScroll = true;
            flowFontIcons.BackColor = System.Drawing.Color.White;
            flowFontIcons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowFontIcons.Location = new System.Drawing.Point(20, 55);
            flowFontIcons.Name = "flowFontIcons";
            flowFontIcons.Size = new System.Drawing.Size(620, 380);
            flowFontIcons.TabIndex = 1;
            // 
            // lblFontTitle
            // 
            lblFontTitle.AutoSize = true;
            lblFontTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblFontTitle.Location = new System.Drawing.Point(20, 20);
            lblFontTitle.Name = "lblFontTitle";
            lblFontTitle.Size = new System.Drawing.Size(220, 19);
            lblFontTitle.TabIndex = 0;
            lblFontTitle.Text = "Font Icons (Unicode Characters)";
            // 
            // lblVersion
            // 
            lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblVersion.AutoSize = true;
            lblVersion.ForeColor = System.Drawing.Color.Gray;
            lblVersion.Location = new System.Drawing.Point(12, 565);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new System.Drawing.Size(72, 15);
            lblVersion.TabIndex = 4;
            lblVersion.Text = "Version 1.1.0";
            // 
            // btnReset
            // 
            btnReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnReset.Location = new System.Drawing.Point(608, 555);
            btnReset.Name = "btnReset";
            btnReset.Size = new System.Drawing.Size(80, 30);
            btnReset.TabIndex = 3;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(518, 555);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(80, 30);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOK.Location = new System.Drawing.Point(428, 555);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(80, 30);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // ImagePickerDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(704, 601);
            Controls.Add(lblVersion);
            Controls.Add(btnReset);
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
            tabImagePicker.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            tabRasterImages.ResumeLayout(false);
            tabRasterImages.PerformLayout();
            tabVectorImages.ResumeLayout(false);
            tabVectorImages.PerformLayout();
            tabFontIcons.ResumeLayout(false);
            tabFontIcons.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabImagePicker;
        private System.Windows.Forms.TabPage tabRasterImages;
        private System.Windows.Forms.TabPage tabVectorImages;
        private System.Windows.Forms.TabPage tabFontIcons;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Label lblCategories;
        private System.Windows.Forms.ListBox lstCategories;
        private System.Windows.Forms.Label lblIcons;
        private System.Windows.Forms.FlowLayoutPanel flowIcons;
        private System.Windows.Forms.RadioButton rbFormResources;
        private System.Windows.Forms.RadioButton rbProjectResources;
        private System.Windows.Forms.Label lblRasterInfo;
        private System.Windows.Forms.Button btnImportRaster;
        private System.Windows.Forms.Button btnClearRaster;
        private System.Windows.Forms.Label lblVectorInfo;
        private System.Windows.Forms.Button btnImportVector;
        private System.Windows.Forms.Label lblFontTitle;
        private System.Windows.Forms.FlowLayoutPanel flowFontIcons;
        private System.Windows.Forms.Label lblFontNote;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblVersion;
    }
}