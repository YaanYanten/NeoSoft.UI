using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NeoSoft.UI.Enums;
using NeoSoft.UI.Helpers;

namespace NeoSoft.UI.Dialogs
{
    /// <summary>
    /// Image Picker dialog for selecting icons
    /// </summary>
    public class ImagePickerDialog : Form
    {
        #region Private Fields

        private Image _selectedImage;
        private string _selectedIconName;
        private int _selectedIconSize = 16;

        private TabControl tabControl;
        private TabPage tabImagePicker;
        private TabPage tabRasterImages;
        private TabPage tabVectorImages;
        private TabPage tabFontIcons;

        private ListBox lstCategories;
        private FlowLayoutPanel flowIcons;
        private TextBox txtSearch;
        private ComboBox cmbSize;
        private Button btnOK;
        private Button btnCancel;
        private Button btnReset;
        private RadioButton rbFormResources;
        private RadioButton rbProjectResources;

        private bool _isSearchPlaceholder = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected image
        /// </summary>
        public Image SelectedImage => _selectedImage;

        /// <summary>
        /// Gets the selected icon name
        /// </summary>
        public string SelectedIconName => _selectedIconName;

        /// <summary>
        /// Gets the selected icon size
        /// </summary>
        public int SelectedIconSize => _selectedIconSize;

        /// <summary>
        /// Gets the selected predefined icon (for compatibility)
        /// </summary>
        public PredefinedIcon SelectedIcon
        {
            get
            {
                // Try to match the selected icon name to a predefined icon
                if (string.IsNullOrEmpty(_selectedIconName))
                    return PredefinedIcon.None;

                if (Enum.TryParse(_selectedIconName, true, out PredefinedIcon icon))
                    return icon;

                return PredefinedIcon.None;
            }
        }

        #endregion

        #region Constructor

        public ImagePickerDialog()
        {
            InitializeComponent();
            LoadCategories();
            LoadIcons();
        }

        #endregion

        #region Initialization

        private void InitializeComponent()
        {
            // Form configuration
            this.Text = "Image Picker";
            this.Size = new Size(680, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new Font("Segoe UI", 9F);

            // TabControl
            tabControl = new TabControl
            {
                Location = new Point(12, 12),
                Size = new Size(640, 530),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            // Tab Pages
            CreateImagePickerTab();
            CreateRasterImagesTab();
            CreateVectorImagesTab();
            CreateFontIconsTab();

            // Buttons
            CreateButtons();

            // Version label
            Label lblVersion = new Label
            {
                Text = "Version 1.0.0",
                Location = new Point(12, 565),
                Size = new Size(150, 20),
                ForeColor = Color.Gray,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            // Add controls
            this.Controls.Add(tabControl);
            this.Controls.Add(lblVersion);
        }

        private void CreateImagePickerTab()
        {
            tabImagePicker = new TabPage("Image Picker");
            tabControl.TabPages.Add(tabImagePicker);

            // Search box
            txtSearch = new TextBox
            {
                Location = new Point(320, 10),
                Size = new Size(200, 25),
                Text = "Enter text to search...",
                ForeColor = Color.Gray
            };
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // Size filter label
            Label lblSize = new Label
            {
                Text = "Size:",
                Location = new Point(530, 13),
                Size = new Size(35, 20)
            };

            // Size filter ComboBox
            cmbSize = new ComboBox
            {
                Location = new Point(570, 10),
                Size = new Size(70, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSize.Items.AddRange(new object[] { "All", "16x16", "32x32" });
            cmbSize.SelectedIndex = 0;
            cmbSize.SelectedIndexChanged += CmbSize_SelectedIndexChanged;

            // Categories label
            Label lblCategories = new Label
            {
                Text = "Categories",
                Location = new Point(10, 45),
                Size = new Size(200, 20)
            };

            // Categories ListBox
            lstCategories = new ListBox
            {
                Location = new Point(10, 70),
                Size = new Size(200, 380),
                SelectionMode = SelectionMode.One
            };
            lstCategories.SelectedIndexChanged += LstCategories_SelectedIndexChanged;

            // Icons FlowLayoutPanel
            flowIcons = new FlowLayoutPanel
            {
                Location = new Point(220, 70),
                Size = new Size(400, 380),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Resource options
            rbFormResources = new RadioButton
            {
                Text = "Add to form resources",
                Location = new Point(220, 460),
                Size = new Size(180, 20),
                Checked = true
            };

            rbProjectResources = new RadioButton
            {
                Text = "Add to project resources",
                Location = new Point(410, 460),
                Size = new Size(180, 20)
            };

            // Add controls to tab
            tabImagePicker.Controls.Add(txtSearch);
            tabImagePicker.Controls.Add(lblSize);
            tabImagePicker.Controls.Add(cmbSize);
            tabImagePicker.Controls.Add(lblCategories);
            tabImagePicker.Controls.Add(lstCategories);
            tabImagePicker.Controls.Add(flowIcons);
            tabImagePicker.Controls.Add(rbFormResources);
            tabImagePicker.Controls.Add(rbProjectResources);
        }

        private void CreateRasterImagesTab()
        {
            tabRasterImages = new TabPage("Raster Images");
            tabControl.TabPages.Add(tabRasterImages);

            Label lblInfo = new Label
            {
                Text = "Import raster images (PNG, JPG, BMP)",
                Location = new Point(20, 20),
                Size = new Size(300, 20)
            };

            Button btnImport = new Button
            {
                Text = "Import...",
                Location = new Point(20, 50),
                Size = new Size(100, 30)
            };
            btnImport.Click += BtnImportRaster_Click;

            Button btnClear = new Button
            {
                Text = "Clear",
                Location = new Point(130, 50),
                Size = new Size(100, 30)
            };
            btnClear.Click += (s, e) => _selectedImage = null;

            tabRasterImages.Controls.Add(lblInfo);
            tabRasterImages.Controls.Add(btnImport);
            tabRasterImages.Controls.Add(btnClear);
        }

        private void CreateVectorImagesTab()
        {
            tabVectorImages = new TabPage("Vector Images");
            tabControl.TabPages.Add(tabVectorImages);

            Label lblInfo = new Label
            {
                Text = "Import vector images (SVG) - Coming soon",
                Location = new Point(20, 20),
                Size = new Size(300, 20)
            };

            Button btnImport = new Button
            {
                Text = "Import...",
                Location = new Point(20, 50),
                Size = new Size(100, 30),
                Enabled = false
            };

            tabVectorImages.Controls.Add(lblInfo);
            tabVectorImages.Controls.Add(btnImport);
        }

        private void CreateFontIconsTab()
        {
            tabFontIcons = new TabPage("Font Icons");
            tabControl.TabPages.Add(tabFontIcons);

            Label lblTitle = new Label
            {
                Text = "Font Icons",
                Location = new Point(20, 20),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            FlowLayoutPanel flowFontIcons = new FlowLayoutPanel
            {
                Location = new Point(20, 55),
                Size = new Size(580, 350),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Add some font icons
            string[] fontIcons = new[]
            {
                "☰", "📶", "🔗", "📱", "📡", "🛡️", "☀️", "👤", "🌙", "✈️",
                "🖥️", "🔖", "⭐", "✓", "✕", "⚙️", "📹", "✉️", "👥", "📞"
            };

            foreach (var icon in fontIcons)
            {
                Button btnIcon = new Button
                {
                    Text = icon,
                    Size = new Size(45, 45),
                    Font = new Font("Segoe UI Emoji", 18F),
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(3),
                    Cursor = Cursors.Hand
                };
                btnIcon.FlatAppearance.BorderColor = Color.LightGray;
                flowFontIcons.Controls.Add(btnIcon);
            }

            Label lblNote = new Label
            {
                Text = "NOTE: Font icons are system-dependent",
                Location = new Point(20, 420),
                Size = new Size(300, 20),
                ForeColor = Color.Gray
            };

            tabFontIcons.Controls.Add(lblTitle);
            tabFontIcons.Controls.Add(flowFontIcons);
            tabFontIcons.Controls.Add(lblNote);
        }

        private void CreateButtons()
        {
            btnOK = new Button
            {
                Text = "OK",
                Location = new Point(388, 555),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(478, 555),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            btnReset = new Button
            {
                Text = "Reset",
                Location = new Point(568, 555),
                Size = new Size(80, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnReset.Click += BtnReset_Click;

            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnReset);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        #endregion

        #region Data Loading

        private void LoadCategories()
        {
            try
            {
                lstCategories.Items.Clear();

                // Try to load from IconResourceLoader
                var categories = IconResourceLoader.GetCategories();

                if (categories != null && categories.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        lstCategories.Items.Add(category);
                    }
                }
                else
                {
                    // Fallback to default categories
                    lstCategories.Items.AddRange(new object[]
                    {
                        "All",
                        "Actions",
                        "Arrows",
                        "Edit",
                        "Navigation",
                        "Files"
                    });
                }

                if (lstCategories.Items.Count > 0)
                {
                    lstCategories.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");

                // Fallback
                lstCategories.Items.AddRange(new object[] { "All", "Actions", "Arrows" });
                lstCategories.SelectedIndex = 0;
            }
        }

        private void LoadIcons()
        {
            try
            {
                flowIcons.SuspendLayout();
                flowIcons.Controls.Clear();

                string selectedCategory = lstCategories.SelectedItem?.ToString() ?? "All";
                int filterSize = GetSelectedSize();

                // Get icons from loader
                var icons = IconResourceLoader.GetIcons(selectedCategory, filterSize);

                if (icons != null && icons.Count > 0)
                {
                    foreach (var iconResource in icons)
                    {
                        Panel iconPanel = CreateIconPanel(
                            iconResource.Image,
                            iconResource.Name,
                            iconResource.Size
                        );
                        flowIcons.Controls.Add(iconPanel);
                    }
                }
                else
                {
                    // Show "No icons" message
                    Label lblNoIcons = new Label
                    {
                        Text = "No icons found in this category.\n\nAdd your icons to:\nResources/Images/{Category}/ICONS_{Size}/",
                        Location = new Point(10, 10),
                        Size = new Size(350, 100),
                        ForeColor = Color.Gray,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    flowIcons.Controls.Add(lblNoIcons);
                }

                flowIcons.ResumeLayout();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading icons: {ex.Message}");

                Label lblError = new Label
                {
                    Text = $"Error loading icons:\n{ex.Message}",
                    Location = new Point(10, 10),
                    Size = new Size(350, 100),
                    ForeColor = Color.Red
                };
                flowIcons.Controls.Add(lblError);
            }
        }

        private int GetSelectedSize()
        {
            if (cmbSize.SelectedItem == null)
                return 0;

            string selected = cmbSize.SelectedItem.ToString();

            if (selected == "16x16")
                return 16;
            else if (selected == "32x32")
                return 32;

            return 0; // All sizes
        }

        #endregion

        #region UI Creation

        private Panel CreateIconPanel(Image icon, string name, int size)
        {
            Panel panel = new Panel
            {
                Size = new Size(70, 85),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Margin = new Padding(5),
                BackColor = Color.White,
                Tag = new IconInfo { Name = name, Size = size, Image = icon }
            };

            // Icon PictureBox
            PictureBox picIcon = new PictureBox
            {
                Size = new Size(size, size),
                Location = new Point((70 - size) / 2, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = icon
            };

            // Icon name label
            Label lblName = new Label
            {
                Text = name,
                Location = new Point(0, size + 15),
                Size = new Size(70, 30),
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI", 7F)
            };

            // Size label
            Label lblSize = new Label
            {
                Text = $"{size}x{size}",
                Location = new Point(0, size + 35),
                Size = new Size(70, 20),
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI", 6F),
                ForeColor = Color.Gray
            };

            panel.Controls.Add(picIcon);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblSize);

            // Events
            panel.Click += (s, e) => SelectIconPanel(panel);
            picIcon.Click += (s, e) => SelectIconPanel(panel);
            lblName.Click += (s, e) => SelectIconPanel(panel);
            lblSize.Click += (s, e) => SelectIconPanel(panel);

            return panel;
        }

        private void SelectIconPanel(Panel panel)
        {
            // Deselect all
            foreach (Control ctrl in flowIcons.Controls)
            {
                if (ctrl is Panel p)
                    p.BackColor = Color.White;
            }

            // Select this one
            panel.BackColor = Color.FromArgb(200, 230, 255);

            // Store selection
            if (panel.Tag is IconInfo info)
            {
                _selectedImage = info.Image;
                _selectedIconName = info.Name;
                _selectedIconSize = info.Size;
            }
        }

        #endregion

        #region Event Handlers

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (_isSearchPlaceholder)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
                _isSearchPlaceholder = false;
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Enter text to search...";
                txtSearch.ForeColor = Color.Gray;
                _isSearchPlaceholder = true;
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_isSearchPlaceholder)
                return;

            string searchText = txtSearch.Text.ToLower();

            foreach (Control ctrl in flowIcons.Controls)
            {
                if (ctrl is Panel panel && panel.Tag is IconInfo info)
                {
                    ctrl.Visible = string.IsNullOrEmpty(searchText) ||
                                   info.Name.ToLower().Contains(searchText);
                }
            }
        }

        private void LstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadIcons();
        }

        private void CmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadIcons();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            _selectedImage = null;
            _selectedIconName = null;
            _selectedIconSize = 16;

            txtSearch.Text = "Enter text to search...";
            txtSearch.ForeColor = Color.Gray;
            _isSearchPlaceholder = true;

            if (lstCategories.Items.Count > 0)
                lstCategories.SelectedIndex = 0;

            if (cmbSize.Items.Count > 0)
                cmbSize.SelectedIndex = 0;

            foreach (Control ctrl in flowIcons.Controls)
            {
                if (ctrl is Panel p)
                    p.BackColor = Color.White;
            }
        }

        private void BtnImportRaster_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp|All Files|*.*";
                ofd.Title = "Select Image";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _selectedImage = Image.FromFile(ofd.FileName);
                        _selectedIconName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                        MessageBox.Show("Image loaded successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Helper Classes

        private class IconInfo
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public Image Image { get; set; }
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose controls
                tabControl?.Dispose();
                btnOK?.Dispose();
                btnCancel?.Dispose();
                btnReset?.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}