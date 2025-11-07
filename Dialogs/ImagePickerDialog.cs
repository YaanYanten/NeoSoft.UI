using System;
using System.Drawing;
using System.Windows.Forms;
using NeoSoft.UI.Enums;

namespace NeoSoft.UI.Dialogs
{
    /// <summary>
    /// Image Picker dialog - CORRECT LAYOUT
    /// Matches Visual Studio's standard image picker
    /// </summary>
    public partial class ImagePickerDialog : Form
    {
        #region Private Fields

        private Image _selectedImage;
        private string _selectedIconName;
        private PredefinedIcon _selectedPredefinedIcon = PredefinedIcon.None;
        private bool _useLocalResource = true;

        #endregion

        #region Properties

        public Image SelectedImage => _selectedImage;
        public string SelectedIconName => _selectedIconName;
        public bool UseLocalResource => _useLocalResource;

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
            LoadLocalResources();
        }

        #endregion

        #region Initialization

        private void InitializeEvents()
        {
            rbLocalResource.CheckedChanged += RbResource_CheckedChanged;
            rbProjectResource.CheckedChanged += RbResource_CheckedChanged;
            btnImportLocal.Click += BtnImportLocal_Click;
            btnClearLocal.Click += BtnClearLocal_Click;
            btnImportProject.Click += BtnImportProject_Click;

            // Raster Images tab events
            chkListCategories.ItemCheck += ChkListCategories_ItemCheck;
            chkListSize.ItemCheck += ChkListSize_ItemCheck;
            txtRasterSearch.TextChanged += TxtRasterSearch_TextChanged;

            // Cargar iconos al cambiar de tab
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                if (tabControl.SelectedTab == tabRasterImages)
                {
                    LoadRasterIcons();
                }
            };
        }

        #endregion

        #region Public Methods

        public void SetInitialIcon(PredefinedIcon icon)
        {
            _selectedPredefinedIcon = icon;

            if (icon != PredefinedIcon.None)
            {
                _selectedIconName = icon.ToString();
            }
        }

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

        private void LoadLocalResources()
        {
            try
            {
                lstLocalResources.Items.Clear();

                // TODO: Cargar recursos locales del proyecto actual
                // Por ahora, mostrar mensaje
                lstLocalResources.Items.Add("(No local resources)");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading local resources: {ex.Message}");
            }
        }

        private void LoadProjectResources()
        {
            try
            {
                // TODO: Cargar desde Project.resx
                MessageBox.Show("Project resource loading not implemented yet.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading project resources: {ex.Message}");
            }
        }

        private void LoadRasterIcons()
        {
            try
            {
                flowRasterIcons.SuspendLayout();
                flowRasterIcons.Controls.Clear();

                // Obtener categorías seleccionadas
                var selectedCategories = new System.Collections.Generic.List<string>();
                foreach (var item in chkListCategories.CheckedItems)
                {
                    selectedCategories.Add(item.ToString());
                }

                // Obtener tamaños seleccionados
                var selectedSizes = new System.Collections.Generic.List<int>();
                foreach (var item in chkListSize.CheckedItems)
                {
                    string sizeStr = item.ToString();
                    if (sizeStr == "16x16") selectedSizes.Add(16);
                    else if (sizeStr == "32x32") selectedSizes.Add(32);
                }

                // Si no hay nada seleccionado, cargar todo
                bool loadAll = selectedCategories.Count == 0 && selectedSizes.Count == 0;

                // TODO: Cargar iconos desde recursos embebidos
                // Por ahora, crear iconos de ejemplo
                CreateSampleIcons(selectedCategories, selectedSizes, loadAll);

                flowRasterIcons.ResumeLayout();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading raster icons: {ex.Message}");
                flowRasterIcons.ResumeLayout();
            }
        }

        private void CreateSampleIcons(System.Collections.Generic.List<string> categories,
            System.Collections.Generic.List<int> sizes, bool loadAll)
        {
            // Iconos de ejemplo para demostración
            string[] sampleCategories = { "Actions", "Arrows", "Business Objects", "Chart" };
            int[] sampleSizes = { 16, 32 };

            for (int i = 0; i < 24; i++)
            {
                string category = sampleCategories[i % sampleCategories.Length];
                int size = sampleSizes[i % sampleSizes.Length];

                // Filtrar por categoría y tamaño si está seleccionado
                if (!loadAll)
                {
                    if (categories.Count > 0 && !categories.Contains(category))
                        continue;
                    if (sizes.Count > 0 && !sizes.Contains(size))
                        continue;
                }

                // Crear panel de icono de ejemplo
                Panel iconPanel = CreateRasterIconPanel($"Icon{i + 1}", category, size);
                flowRasterIcons.Controls.Add(iconPanel);
            }

            if (flowRasterIcons.Controls.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "No icons found with selected filters",
                    AutoSize = false,
                    Size = new Size(300, 50),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray
                };
                flowRasterIcons.Controls.Add(lblEmpty);
            }
        }

        private Panel CreateRasterIconPanel(string name, string category, int size)
        {
            Panel panel = new Panel
            {
                Size = new Size(48, 48),
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                BackColor = Color.White,
                Tag = new RasterIconInfo { Name = name, Category = category, Size = size }
            };

            // Crear un icono simple de ejemplo (cuadrado con color)
            PictureBox pic = new PictureBox
            {
                Size = new Size(size, size),
                Location = new Point((48 - size) / 2, (48 - size) / 2),
                BackColor = Color.FromArgb(100 + (name.GetHashCode() % 155),
                                          100 + (category.GetHashCode() % 155),
                                          100 + (size * 3 % 155))
            };

            panel.Controls.Add(pic);

            // Eventos
            panel.Click += (s, e) => SelectRasterIcon(panel);
            pic.Click += (s, e) => SelectRasterIcon(panel);

            return panel;
        }

        private void SelectRasterIcon(Panel panel)
        {
            // Deseleccionar todos
            foreach (Control ctrl in flowRasterIcons.Controls)
            {
                if (ctrl is Panel p)
                    p.BackColor = Color.White;
            }

            // Seleccionar este
            panel.BackColor = Color.LightBlue;

            // Asignar imagen seleccionada
            if (panel.Tag is RasterIconInfo info)
            {
                _selectedIconName = info.Name;
            }
        }

        #endregion

        #region Event Handlers

        private void RbResource_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLocalResource.Checked)
            {
                _useLocalResource = true;
                panelLocalResource.Enabled = true;
                panelProjectResource.Enabled = false;
                LoadLocalResources();
            }
            else if (rbProjectResource.Checked)
            {
                _useLocalResource = false;
                panelLocalResource.Enabled = false;
                panelProjectResource.Enabled = true;
                LoadProjectResources();
            }
        }

        private void BtnImportLocal_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif|PNG Files|*.png|JPEG Files|*.jpg;*.jpeg|All Files|*.*";
                ofd.Title = "Select Image";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _selectedImage = Image.FromFile(ofd.FileName);
                        _selectedIconName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                        _selectedPredefinedIcon = PredefinedIcon.None;

                        // Mostrar preview
                        if (picPreview.Image != null)
                            picPreview.Image.Dispose();

                        picPreview.Image = new Bitmap(_selectedImage, picPreview.Size);

                        MessageBox.Show($"Image loaded!\n\nFile: {_selectedIconName}\nSize: {_selectedImage.Width}×{_selectedImage.Height}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image:\n\n{ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnClearLocal_Click(object sender, EventArgs e)
        {
            _selectedImage = null;
            _selectedIconName = null;
            _selectedPredefinedIcon = PredefinedIcon.None;

            if (picPreview.Image != null)
            {
                picPreview.Image.Dispose();
                picPreview.Image = null;
            }

            lstLocalResources.SelectedIndex = -1;
        }

        private void BtnImportProject_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Project resource import not implemented yet.",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (_selectedImage == null)
            {
                MessageBox.Show("Please select an image first.",
                    "No Image Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ChkListCategories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Usar BeginInvoke para que el cambio se aplique antes de recargar
            this.BeginInvoke(new Action(() => LoadRasterIcons()));
        }

        private void ChkListSize_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() => LoadRasterIcons()));
        }

        private void TxtRasterSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtRasterSearch.Text.ToLower();

            foreach (Control ctrl in flowRasterIcons.Controls)
            {
                if (ctrl is Panel panel && panel.Tag is RasterIconInfo info)
                {
                    string name = info.Name.ToLower();
                    ctrl.Visible = string.IsNullOrEmpty(searchText) || name.Contains(searchText);
                }
            }
        }

        #endregion

        #region Helper Classes

        private class RasterIconInfo
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public int Size { get; set; }
        }

        #endregion

    }
}