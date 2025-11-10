using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NeoSoft.UI.Controls
{
    /// <summary>
    /// Control de navegación lateral con menú superior
    /// </summary>
    [ToolboxBitmap(typeof(SideNavigationPanel), "SideNavigationPanel.bmp")]
    [Description("Side navigation panel with menu and content area")]
    public partial class SideNavigationPanel : UserControl
    {
        #region Private Fields

        private List<Navigation.NavigationItem> _items;
        private Navigation.NavigationItem _selectedItem;
        private bool _isCollapsed = false;
        private int _expandedWidth = 250;
        private int _collapsedWidth = 50;

        #endregion

        #region Properties

        [Category("Navigation")]
        [Description("Items de navegación")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Navigation.NavigationItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                RefreshNavigation();
            }
        }

        [Category("Navigation")]
        [Description("Item seleccionado")]
        [Browsable(false)]
        public Navigation.NavigationItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnItemSelected(new NavigationItemEventArgs(value));
                RefreshNavigation();
            }
        }

        [Category("Navigation")]
        [Description("Ancho cuando está expandido")]
        [DefaultValue(250)]
        public int ExpandedWidth
        {
            get => _expandedWidth;
            set
            {
                _expandedWidth = value;
                if (!_isCollapsed)
                    pnlLeft.Width = value;
            }
        }

        [Category("Navigation")]
        [Description("Ancho cuando está colapsado")]
        [DefaultValue(50)]
        public int CollapsedWidth
        {
            get => _collapsedWidth;
            set
            {
                _collapsedWidth = value;
                if (_isCollapsed)
                    pnlLeft.Width = value;
            }
        }

        [Category("Navigation")]
        [Description("Indica si el panel está colapsado")]
        [DefaultValue(false)]
        public bool IsCollapsed
        {
            get => _isCollapsed;
            set
            {
                _isCollapsed = value;
                pnlLeft.Width = value ? _collapsedWidth : _expandedWidth;
            }
        }

        [Category("Appearance")]
        [Description("Color de fondo del panel de navegación")]
        public Color NavigationBackColor
        {
            get => pnlLeft.BackColor;
            set => pnlLeft.BackColor = value;
        }

        [Category("Appearance")]
        [Description("Color de fondo del panel de contenido")]
        public Color ContentBackColor
        {
            get => pnlContent.BackColor;
            set => pnlContent.BackColor = value;
        }

        #endregion

        #region Events

        [Category("Navigation")]
        [Description("Ocurre cuando se selecciona un item")]
        public event EventHandler<NavigationItemEventArgs> ItemSelected;

        [Category("Navigation")]
        [Description("Ocurre cuando se expande/colapsa un grupo")]
        public event EventHandler<NavigationItemEventArgs> ItemToggled;

        #endregion

        #region Constructor

        public SideNavigationPanel()
        {
            _items = new List<Navigation.NavigationItem>();
            InitializeComponent();
            InitializeEvents();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
        }

        #endregion

        #region Initialization

        private void InitializeEvents()
        {
            // Vincular eventos del botón toggle
            btnToggleMenu.Click += BtnToggleMenu_Click;

            // Configurar context menu
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            var addGroup = new ToolStripMenuItem("Add Group");
            addGroup.Click += (s, e) => AddGroup();

            var addChildGroup = new ToolStripMenuItem("Add Child Group");
            addChildGroup.Click += (s, e) => AddChildGroup();

            var removeGroup = new ToolStripMenuItem("Remove Group");
            removeGroup.Click += (s, e) => RemoveGroup();

            var addItem = new ToolStripMenuItem("Add Item");
            addItem.Click += (s, e) => AddItem();

            var addSeparator = new ToolStripMenuItem("Add Separator");
            addSeparator.Click += (s, e) => AddSeparator();

            contextMenu.Items.AddRange(new ToolStripItem[]
            {
                addGroup,
                addChildGroup,
                removeGroup,
                new ToolStripSeparator(),
                addItem,
                addSeparator
            });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Agregar un item al menú superior
        /// </summary>
        public ToolStripMenuItem AddMenuItem(string text)
        {
            var item = new ToolStripMenuItem(text);
            menuStrip.Items.Add(item);
            return item;
        }

        /// <summary>
        /// Agregar un control al área de contenido
        /// </summary>
        public void SetContentControl(Control control)
        {
            pnlContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);
        }

        /// <summary>
        /// Refrescar la navegación
        /// </summary>
        public void RefreshNavigation()
        {
            pnlNavigation.SuspendLayout();
            pnlNavigation.Controls.Clear();

            int y = 10;
            foreach (var item in _items)
            {
                y = AddNavigationItemControl(item, y);
            }

            pnlNavigation.ResumeLayout();
        }

        /// <summary>
        /// Toggle collapse/expand
        /// </summary>
        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }

        #endregion

        #region Private Methods

        private int AddNavigationItemControl(Navigation.NavigationItem item, int yPosition)
        {
            if (item.IsSeparator)
            {
                var separator = new Panel
                {
                    Height = 1,
                    Width = pnlNavigation.Width - 20,
                    Left = 10,
                    Top = yPosition + 5,
                    BackColor = Color.FromArgb(100, 255, 255, 255)
                };
                pnlNavigation.Controls.Add(separator);
                return yPosition + 11;
            }

            var btn = new Button
            {
                Width = pnlNavigation.Width - 20,
                Height = 40,
                Left = 10 + (item.Level * 15),
                Top = yPosition,
                Text = item.Text,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                BackColor = item == _selectedItem ? Color.FromArgb(38, 50, 56) : Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                Tag = item,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(69, 90, 100);

            // Icono o flecha
            if (item.IsGroup)
            {
                string arrow = item.IsExpanded ? "▼" : "▶";
                btn.Text = $"{arrow} {item.Text}";
            }

            btn.Click += NavButton_Click;
            btn.MouseDown += NavButton_MouseDown;

            pnlNavigation.Controls.Add(btn);
            yPosition += 45;

            // Agregar hijos si está expandido
            if (item.IsGroup && item.IsExpanded)
            {
                foreach (var child in item.Children)
                {
                    yPosition = AddNavigationItemControl(child, yPosition);
                }
            }

            return yPosition;
        }

        #endregion

        #region Event Handlers

        private void BtnToggleMenu_Click(object sender, EventArgs e)
        {
            ToggleCollapse();
        }

        private void NavButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Navigation.NavigationItem item)
            {
                if (item.IsGroup)
                {
                    item.IsExpanded = !item.IsExpanded;
                    OnItemToggled(new NavigationItemEventArgs(item));
                    RefreshNavigation();
                }
                else
                {
                    SelectedItem = item;
                }
            }
        }

        private void NavButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && sender is Button btn)
            {
                contextMenu.Tag = btn.Tag;
                contextMenu.Show(btn, e.Location);
            }
        }

        #endregion

        #region Context Menu Actions

        private void AddGroup()
        {
            var item = Navigation.NavigationItem.CreateGroup("New Group");
            _items.Add(item);
            RefreshNavigation();
        }

        private void AddChildGroup()
        {
            if (contextMenu.Tag is Navigation.NavigationItem parent)
            {
                var item = Navigation.NavigationItem.CreateGroup("New Child Group");
                parent.AddChild(item);
                parent.IsExpanded = true;
                RefreshNavigation();
            }
        }

        private void RemoveGroup()
        {
            if (contextMenu.Tag is Navigation.NavigationItem item)
            {
                if (item.Parent != null)
                {
                    item.Parent.RemoveChild(item);
                }
                else
                {
                    _items.Remove(item);
                }
                RefreshNavigation();
            }
        }

        private void AddItem()
        {
            if (contextMenu.Tag is Navigation.NavigationItem parent && parent.IsGroup)
            {
                var item = new Navigation.NavigationItem("New Item");
                parent.AddChild(item);
                parent.IsExpanded = true;
                RefreshNavigation();
            }
        }

        private void AddSeparator()
        {
            if (contextMenu.Tag is Navigation.NavigationItem parent && parent.IsGroup)
            {
                var separator = Navigation.NavigationItem.CreateSeparator();
                parent.AddChild(separator);
                parent.IsExpanded = true;
                RefreshNavigation();
            }
        }

        #endregion

        #region Event Raisers

        protected virtual void OnItemSelected(NavigationItemEventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }

        protected virtual void OnItemToggled(NavigationItemEventArgs e)
        {
            ItemToggled?.Invoke(this, e);
        }

        #endregion
    }

    #region Event Args

    public class NavigationItemEventArgs : EventArgs
    {
        public Navigation.NavigationItem Item { get; }

        public NavigationItemEventArgs(Navigation.NavigationItem item)
        {
            Item = item;
        }
    }

    #endregion
}