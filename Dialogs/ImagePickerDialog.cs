using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using NeoSoft.UI.Enums;
using NeoSoft.UI.Helpers;

namespace NeoSoft.UI.Dialogs
{
    /// <summary>
    /// Image Picker dialog que carga iconos desde recursos embebidos
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
            InitializeIconSystem();
            InitializeEvents();
            LoadLocalResources();
        }

        #endregion

        #region Initialization

        private void InitializeIconSystem()
        {
            try
            {
                // El IconResourceLoader se inicializa automáticamente con el constructor estático
                // No necesita llamada a Initialize()

                // Cargar categorías desde recursos
                LoadCategoriesFromResources();

                // Seleccionar "Select All" por defecto
                if (chkListCategories.Items.Count > 0)
                {
                    chkListCategories.SetItemChecked(0, true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing icon system: {ex.Message}");
                MessageBox.Show(
                    "Error al inicializar el sistema de iconos.\n\n" +
                    "Verifica que los iconos estén como Embedded Resources.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void LoadCategoriesFromResources()
        {
            try
            {
                chkListCategories.Items.Clear();

                // Obtener categorías reales desde los recursos
                var categories = IconResourceLoader.GetCategories();

                // Agregar "Select All" primero
                chkListCategories.Items.Add("Select All");

                // Agregar categorías reales (excepto "All" que ya viene del loader)
                foreach (var category in categories.Where(c => c != "All"))
                {
                    chkListCategories.Items.Add(category);
                }

                // Debug
                System.Diagnostics.Debug.WriteLine($"Categorías cargadas: {chkListCategories.Items.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");

                // Fallback a categorías por defecto
                chkListCategories.Items.Clear();
                chkListCategories.Items.Add("Select All");
                chkListCategories.Items.Add("Actions");
                chkListCategories.Items.Add("Icons");
            }
        }

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
                var selectedCategories = new List<string>();
                bool selectAllChecked = false;

                for (int i = 0; i < chkListCategories.Items.Count; i++)
                {
                    if (chkListCategories.GetItemChecked(i))
                    {
                        string category = chkListCategories.Items[i].ToString();

                        if (category == "Select All")
                        {
                            selectAllChecked = true;
                            break;
                        }
                        else
                        {
                            selectedCategories.Add(category);
                        }
                    }
                }

                // Obtener tamaños seleccionados
                var selectedSizes = new List<int>();
                for (int i = 0; i < chkListSize.Items.Count; i++)
                {
                    if (chkListSize.GetItemChecked(i))
                    {
                        string sizeStr = chkListSize.Items[i].ToString();
                        if (sizeStr == "16x16") selectedSizes.Add(16);
                        else if (sizeStr == "32x32") selectedSizes.Add(32);
                    }
                }

                // Cargar iconos desde recursos embebidos
                List<IconResourceLoader.IconResource> icons;

                if (selectAllChecked || selectedCategories.Count == 0)
                {
                    // Cargar todos los iconos
                    icons = IconResourceLoader.GetIcons("All", 0);
                }
                else
                {
                    // Cargar solo categorías seleccionadas
                    icons = new List<IconResourceLoader.IconResource>();
                    foreach (var category in selectedCategories)
                    {
                        icons.AddRange(IconResourceLoader.GetIcons(category, 0));
                    }
                }

                // Filtrar por tamaño si hay seleccionados
                if (selectedSizes.Count > 0)
                {
                    icons = icons.Where(i => selectedSizes.Contains(i.Size)).ToList();
                }

                // Crear paneles para los iconos
                if (icons != null && icons.Count > 0)
                {
                    foreach (var iconResource in icons)
                    {
                        Panel iconPanel = CreateRasterIconPanel(iconResource);
                        flowRasterIcons.Controls.Add(iconPanel);
                    }

                    System.Diagnostics.Debug.WriteLine($"Iconos cargados: {icons.Count}");
                }
                else
                {
                    ShowNoIconsMessage();
                }

                flowRasterIcons.ResumeLayout();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading raster icons: {ex.Message}");
                MessageBox.Show($"Error loading icons: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                flowRasterIcons.ResumeLayout();
            }
        }

        private Panel CreateRasterIconPanel(IconResourceLoader.IconResource iconResource)
        {
            int panelSize = 80;

            Panel panel = new Panel
            {
                Size = new Size(panelSize, panelSize + 35),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Margin = new Padding(3),
                BackColor = Color.White,
                Tag = iconResource
            };

            // PictureBox para el icono
            PictureBox picIcon = new PictureBox
            {
                Size = new Size(iconResource.Size, iconResource.Size),
                Location = new Point((panelSize - iconResource.Size) / 2, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = iconResource.Image
            };

            // Label para el nombre
            Label lblName = new Label
            {
                Text = CleanIconName(iconResource.ResourceName),  // ← FIX: Usar nombre limpio
                Location = new Point(2, iconResource.Size + 15),
                Size = new Size(panelSize - 4, 18),
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI", 7.5F),
                AutoSize = false
            };

            // Label para el tamaño
            Label lblSize = new Label
            {
                Text = $"{iconResource.Size}×{iconResource.Size}",
                Location = new Point(2, iconResource.Size + 33),
                Size = new Size(panelSize - 4, 15),
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI", 6.5F),
                ForeColor = Color.Gray
            };

            panel.Controls.Add(picIcon);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblSize);

            // Eventos
            panel.Click += (s, e) => SelectRasterIcon(panel);
            picIcon.Click += (s, e) => SelectRasterIcon(panel);
            lblName.Click += (s, e) => SelectRasterIcon(panel);
            lblSize.Click += (s, e) => SelectRasterIcon(panel);

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
            panel.BackColor = Color.FromArgb(220, 240, 255);

            // Guardar selección
            if (panel.Tag is IconResourceLoader.IconResource iconResource)
            {
                try
                {
                    // Verificar que la imagen existe
                    if (iconResource.Image == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ ERROR: iconResource.Image es NULL");
                        MessageBox.Show("Error: La imagen del icono es NULL", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Disponer imagen anterior si existe
                    if (_selectedImage != null)
                    {
                        _selectedImage.Dispose();
                        _selectedImage = null;
                    }

                    // CRÍTICO: Crear copia INDEPENDIENTE usando Bitmap
                    _selectedImage = new Bitmap(iconResource.Image);

                    // Limpiar nombre usando ResourceName
                    _selectedIconName = CleanIconName(iconResource.ResourceName);

                    // Intentar parsear a PredefinedIcon
                    string cleanName = CleanIconName(iconResource.ResourceName);
                    if (Enum.TryParse(cleanName.Replace(" ", ""), true, out PredefinedIcon icon))
                    {
                        _selectedPredefinedIcon = icon;
                    }
                    else
                    {
                        _selectedPredefinedIcon = PredefinedIcon.None;
                    }

                    System.Diagnostics.Debug.WriteLine(
                        $"✓ Icono seleccionado: {_selectedIconName}\n" +
                        $"  Imagen: {_selectedImage.Width}x{_selectedImage.Height}\n" +
                        $"  Format: {_selectedImage.PixelFormat}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ ERROR en SelectRasterIcon: {ex.Message}");
                    MessageBox.Show($"Error al seleccionar icono:\n\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowNoIconsMessage()
        {
            Label lblEmpty = new Label
            {
                Text = "No se encontraron iconos.\n\n" +
                       "Verifica que:\n" +
                       "1. Los iconos estén en Resources/Images/{Categoría}/\n" +
                       "2. Build Action = Embedded Resource\n" +
                       "3. Nombres sigan: Nombre_x_Tamaño.png",
                AutoSize = false,
                Size = new Size(400, 150),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F)
            };
            flowRasterIcons.Controls.Add(lblEmpty);
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

            // DEBUG: Verificar que la imagen es válida
            try
            {
                int width = _selectedImage.Width;
                int height = _selectedImage.Height;
                System.Diagnostics.Debug.WriteLine($"✓ OK clicked - Image valid: {width}x{height}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ ERROR: Image invalid on OK: {ex.Message}");
                MessageBox.Show("Error: La imagen seleccionada es inválida.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ChkListCategories_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Si se está chequeando "Select All"
            if (e.Index == 0 && e.NewValue == CheckState.Checked)
            {
                // Desmarcar todos los demás
                this.BeginInvoke(new Action(() =>
                {
                    for (int i = 1; i < chkListCategories.Items.Count; i++)
                    {
                        chkListCategories.SetItemChecked(i, false);
                    }
                    LoadRasterIcons();
                }));
            }
            // Si se está chequeando cualquier otra categoría
            else if (e.Index > 0 && e.NewValue == CheckState.Checked)
            {
                // Desmarcar "Select All"
                this.BeginInvoke(new Action(() =>
                {
                    chkListCategories.SetItemChecked(0, false);
                    LoadRasterIcons();
                }));
            }
            else
            {
                this.BeginInvoke(new Action(() => LoadRasterIcons()));
            }
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
                if (ctrl is Panel panel && panel.Tag is IconResourceLoader.IconResource info)
                {
                    string name = info.Name.ToLower();
                    ctrl.Visible = string.IsNullOrEmpty(searchText) || name.Contains(searchText);
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Limpia el nombre del icono para mostrarlo correctamente
        /// </summary>
        private string CleanIconName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "Icon";

            // Si contiene path completo (puntos), tomar solo el nombre del archivo
            if (name.Contains("."))
            {
                string[] parts = name.Split('.');
                // Tomar penúltima parte (antes de la extensión)
                name = parts.Length > 1 ? parts[parts.Length - 2] : parts[parts.Length - 1];
            }

            // Remover sufijo de tamaño (_x_16, _x_32)
            if (name.Contains("_x_"))
            {
                int index = name.LastIndexOf("_x_");
                name = name.Substring(0, index);
            }

            // Reemplazar guiones bajos con espacios
            name = name.Replace("_", " ");

            // Limitar longitud para que quepa en el label
            if (name.Length > 12)
            {
                name = name.Substring(0, 9) + "...";
            }

            return name;
        }

        #endregion
    }
}