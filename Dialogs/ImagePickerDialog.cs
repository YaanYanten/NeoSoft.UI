using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NeoSoft.UI.Enums;
using NeoSoft.UI.Helpers;

namespace NeoSoft.UI.Dialogs
{
    /// <summary>
    /// Image Picker dialog for selecting icons
    /// SEPARATED DESIGNER VERSION - UI in Designer.cs file
    /// </summary>
    public partial class ImagePickerDialog : Form
    {
        #region Private Fields

        private Image _selectedImage;
        private string _selectedIconName;
        private int _selectedIconSize = 16;
        private PredefinedIcon _selectedPredefinedIcon = PredefinedIcon.None;
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
        /// Gets the selected predefined icon
        /// </summary>
        public PredefinedIcon SelectedIcon
        {
            get
            {
                if (_selectedPredefinedIcon != PredefinedIcon.None)
                    return _selectedPredefinedIcon;

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
            InitializeEvents();
            LoadCategories();
            LoadIcons();
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize event handlers
        /// </summary>
        private void InitializeEvents()
        {
            // Search box events
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // ComboBox events
            cmbSize.SelectedIndexChanged += CmbSize_SelectedIndexChanged;

            // ListBox events
            lstCategories.SelectedIndexChanged += LstCategories_SelectedIndexChanged;

            // Button events
            btnReset.Click += BtnReset_Click;
            btnImportRaster.Click += BtnImportRaster_Click;
            btnClearRaster.Click += BtnClearRaster_Click;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the initial icon to display as selected when the dialog opens
        /// </summary>
        public void SetInitialIcon(PredefinedIcon icon)
        {
            _selectedPredefinedIcon = icon;

            if (icon != PredefinedIcon.None)
            {
                _selectedIconName = icon.ToString();
                SelectIconByName(_selectedIconName);
            }
        }

        /// <summary>
        /// Sets the initial image to display as selected when the dialog opens
        /// </summary>
        public void SetInitialImage(Image image, string name = null)
        {
            if (image != null)
            {
                _selectedImage = image;
                _selectedIconName = name ?? "Custom";
                _selectedPredefinedIcon = PredefinedIcon.None;
            }
        }

        #endregion

        #region Data Loading

        private void LoadCategories()
        {
            try
            {
                lstCategories.Items.Clear();

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
                    // Fallback categories
                    lstCategories.Items.AddRange(new object[]
                    {
                        "All", "Actions", "Arrows", "Edit",
                        "Navigation", "Files", "Media", "System"
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

                var icons = IconResourceLoader.GetIcons(selectedCategory, filterSize);

                if (icons != null && icons.Count > 0)
                {
                    foreach (var iconResource in icons)
                    {
                        // IMPORTANTE: Limpiar el nombre aquí
                        string cleanName = CleanIconName(iconResource.Name);

                        Panel iconPanel = CreateIconPanel(
                            iconResource.Image,
                            cleanName,  // ← Usar nombre limpio
                            iconResource.Name, // ← Guardar nombre original para búsqueda
                            iconResource.Size
                        );
                        flowIcons.Controls.Add(iconPanel);
                    }
                }
                else
                {
                    ShowNoIconsMessage();
                }

                flowIcons.ResumeLayout();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading icons: {ex.Message}");
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Cleans up icon names by removing resource path prefixes
        /// </summary>
        private string CleanIconName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "Unknown";

            string cleanName = name;

            // 1. Remove common prefixes
            string[] prefixesToRemove = new[]
            {
                "NeoSoft.UI.Resources.Images.",
                "Resources.Images.",
                "Resources.",
                "Images."
            };

            foreach (var prefix in prefixesToRemove)
            {
                if (cleanName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    cleanName = cleanName.Substring(prefix.Length);
                }
            }

            // 2. Remove file extensions
            string[] extensionsToRemove = new[]
            {
                ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".ico"
            };

            foreach (var ext in extensionsToRemove)
            {
                if (cleanName.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    cleanName = cleanName.Substring(0, cleanName.Length - ext.Length);
                    break;
                }
            }

            // 3. Replace underscores and dots with spaces
            cleanName = cleanName.Replace("_", " ").Replace(".", " ");

            // 4. Remove extra spaces
            while (cleanName.Contains("  "))
            {
                cleanName = cleanName.Replace("  ", " ");
            }
            cleanName = cleanName.Trim();

            // 5. Limit length
            if (cleanName.Length > 18)
            {
                cleanName = cleanName.Substring(0, 15) + "...";
            }

            return cleanName;
        }

        private void ShowNoIconsMessage()
        {
            Label lblNoIcons = new Label
            {
                Text = "No icons found in this category.\n\n" +
                       "To add icons:\n" +
                       "1. Add images to: Resources/Images/{Category}/\n" +
                       "2. Set Build Action: Embedded Resource",
                Location = new Point(10, 10),
                Size = new Size(400, 120),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F)
            };
            flowIcons.Controls.Add(lblNoIcons);
        }

        private void ShowErrorMessage(string error)
        {
            Label lblError = new Label
            {
                Text = $"Error loading icons:\n{error}",
                Location = new Point(10, 10),
                Size = new Size(400, 100),
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 9F)
            };
            flowIcons.Controls.Add(lblError);
        }

        private int GetSelectedSize()
        {
            if (cmbSize.SelectedItem == null)
                return 0;

            string selected = cmbSize.SelectedItem.ToString();

            switch (selected)
            {
                case "16x16": return 16;
                case "32x32": return 32;
                case "48x48": return 48;
                default: return 0; // All
            }
        }

        #endregion

        #region UI Creation

        /// <summary>
        /// Creates an icon panel for the flow layout
        /// </summary>
        private Panel CreateIconPanel(Image icon, string displayName, string originalName, int size)
        {
            int panelHeight = size + 55;

            Panel panel = new Panel
            {
                Size = new Size(75, panelHeight),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Margin = new Padding(5),
                BackColor = Color.White,
                Tag = new IconInfo
                {
                    DisplayName = displayName,    // Nombre para mostrar
                    OriginalName = originalName,  // Nombre original para guardar
                    Size = size,
                    Image = icon
                }
            };

            // Icon PictureBox
            PictureBox picIcon = new PictureBox
            {
                Size = new Size(size, size),
                Location = new Point((75 - size) / 2, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = icon
            };

            // Display name label
            Label lblName = new Label
            {
                Text = displayName,  // ← Usar nombre limpio para mostrar
                Location = new Point(2, size + 15),
                Size = new Size(71, 30),
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI", 7.5F),
                AutoSize = false
            };

            // Size label
            Label lblSize = new Label
            {
                Text = $"{size}×{size}",
                Location = new Point(2, size + 40),
                Size = new Size(71, 15),
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI", 6.5F),
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
            panel.BackColor = Color.FromArgb(220, 240, 255);

            // Store selection
            if (panel.Tag is IconInfo info)
            {
                _selectedImage = info.Image;
                _selectedIconName = info.OriginalName;  // Guardar nombre original
                _selectedIconSize = info.Size;

                // Try to parse as predefined icon
                string nameForParsing = info.OriginalName.Replace(" ", "");
                if (Enum.TryParse(nameForParsing, true, out PredefinedIcon icon))
                {
                    _selectedPredefinedIcon = icon;
                }
                else
                {
                    _selectedPredefinedIcon = PredefinedIcon.None;
                }
            }
        }

        private void SelectIconByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            foreach (Control ctrl in flowIcons.Controls)
            {
                if (ctrl is Panel panel && panel.Tag is IconInfo info)
                {
                    if (info.OriginalName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                        info.DisplayName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        SelectIconPanel(panel);
                        flowIcons.ScrollControlIntoView(panel);
                        break;
                    }
                }
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
                txtSearch.Text = "Search icons...";
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
                    // Buscar en ambos nombres
                    ctrl.Visible = string.IsNullOrEmpty(searchText) ||
                                   info.DisplayName.ToLower().Contains(searchText) ||
                                   info.OriginalName.ToLower().Contains(searchText);
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
            _selectedPredefinedIcon = PredefinedIcon.None;

            txtSearch.Text = "Search icons...";
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
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif|" +
                            "PNG Files|*.png|" +
                            "JPEG Files|*.jpg;*.jpeg|" +
                            "All Files|*.*";
                ofd.Title = "Select Image";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _selectedImage = Image.FromFile(ofd.FileName);
                        _selectedIconName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                        _selectedPredefinedIcon = PredefinedIcon.None;

                        MessageBox.Show(
                            $"Image loaded successfully!\n\n" +
                            $"File: {_selectedIconName}\n" +
                            $"Size: {_selectedImage.Width}×{_selectedImage.Height}",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Error loading image:\n\n{ex.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnClearRaster_Click(object sender, EventArgs e)
        {
            _selectedImage = null;
            _selectedIconName = null;
            _selectedPredefinedIcon = PredefinedIcon.None;
            MessageBox.Show("Selection cleared.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Helper Classes

        private class IconInfo
        {
            public string DisplayName { get; set; }   // Nombre limpio para mostrar
            public string OriginalName { get; set; }  // Nombre original completo
            public int Size { get; set; }
            public Image Image { get; set; }
        }

        #endregion
    }
}