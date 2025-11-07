using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using NeoSoft.UI.Controls;
using NeoSoft.UI.Enums;
using NeoSoft.UI.Dialogs;

namespace NeoSoft.UI.Designers
{
    /// <summary>
    /// Provides smart tags (designer actions) for SimpleButton
    /// </summary>
    public class SimpleButtonActionList : DesignerActionList
    {
        private SimpleButton _button;
        private DesignerActionUIService _designerActionUIService;

        /// <summary>
        /// Constructor
        /// </summary>
        public SimpleButtonActionList(IComponent component) : base(component)
        {
            _button = component as SimpleButton;
            _designerActionUIService = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        #region Properties for Smart Tags

        /// <summary>
        /// Gets or sets the button text
        /// </summary>
        [Category("Quick Settings")]
        [Description("The text displayed on the button")]
        public string Text
        {
            get => _button.Text;
            set
            {
                SetProperty("Text", value);
            }
        }

        /// <summary>
        /// Gets or sets the button style
        /// </summary>
        [Category("Quick Settings")]
        [Description("The visual style of the button")]
        public ButtonStyle Style
        {
            get => _button.Style;
            set
            {
                SetProperty("Style", value);
            }
        }

        /// <summary>
        /// Gets or sets the primary color
        /// </summary>
        [Category("Quick Settings")]
        [Description("The primary background color")]
        public Color PrimaryColor
        {
            get => _button.PrimaryColor;
            set
            {
                SetProperty("PrimaryColor", value);
            }
        }

        /// <summary>
        /// Gets or sets the border radius
        /// </summary>
        [Category("Quick Settings")]
        [Description("The radius of rounded corners")]
        public int BorderRadius
        {
            get => _button.BorderRadius;
            set
            {
                SetProperty("BorderRadius", value);
            }
        }

        /// <summary>
        /// Gets or sets the predefined icon type
        /// </summary>
        [Category("Icon Settings")]
        [Description("Select a predefined icon")]
        public PredefinedIcon Icon
        {
            get => _button.PredefinedIconType;
            set
            {
                SetProperty("PredefinedIconType", value);
            }
        }

        /// <summary>
        /// Gets or sets the icon position
        /// </summary>
        [Category("Icon Settings")]
        [Description("The position of the icon relative to text")]
        public ImagePosition IconPosition
        {
            get => _button.IconPosition;
            set
            {
                SetProperty("IconPosition", value);
            }
        }

        #endregion

        #region Methods (Actions)

        /// <summary>
        /// Opens the Image Picker dialog to select an icon
        /// </summary>
        public void SelectIcon()
        {
            try
            {
                using (var dialog = new ImagePickerDialog())
                {
                    // Set current icon if exists (will be implemented in ImagePickerDialog)
                    dialog.SetInitialIcon(_button.PredefinedIconType);

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // If a predefined icon was selected
                        if (dialog.SelectedIcon != PredefinedIcon.None)
                        {
                            SetProperty("PredefinedIconType", dialog.SelectedIcon);
                        }
                        // If a custom image was selected
                        else if (dialog.SelectedImage != null)
                        {
                            SetProperty("Icon", dialog.SelectedImage);
                            // Reset predefined icon
                            SetProperty("PredefinedIconType", PredefinedIcon.None);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening icon selector: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Applies the Light theme preset
        /// </summary>
        public void LightTheme()
        {
            SetProperty("PrimaryColor", Color.White);
            SetProperty("TextColor", Color.FromArgb(33, 33, 33));
            SetProperty("BorderColor", Color.FromArgb(200, 200, 200));
            SetProperty("HoverColor", Color.FromArgb(245, 245, 245));
            SetProperty("PressedColor", Color.FromArgb(230, 230, 230));
        }

        /// <summary>
        /// Applies the Dark theme preset
        /// </summary>
        public void DarkTheme()
        {
            SetProperty("PrimaryColor", Color.FromArgb(45, 45, 48));
            SetProperty("TextColor", Color.White);
            SetProperty("BorderColor", Color.FromArgb(63, 63, 70));
            SetProperty("HoverColor", Color.FromArgb(62, 62, 64));
            SetProperty("PressedColor", Color.FromArgb(30, 30, 32));
        }

        /// <summary>
        /// Applies the Blue theme preset
        /// </summary>
        public void BlueTheme()
        {
            SetProperty("PrimaryColor", Color.FromArgb(0, 122, 204));
            SetProperty("TextColor", Color.White);
            SetProperty("BorderColor", Color.FromArgb(0, 122, 204));
            SetProperty("HoverColor", Color.FromArgb(0, 142, 224));
            SetProperty("PressedColor", Color.FromArgb(0, 102, 184));
        }

        /// <summary>
        /// Applies the Green theme preset
        /// </summary>
        public void GreenTheme()
        {
            SetProperty("PrimaryColor", Color.FromArgb(76, 175, 80));
            SetProperty("TextColor", Color.White);
            SetProperty("BorderColor", Color.FromArgb(76, 175, 80));
            SetProperty("HoverColor", Color.FromArgb(102, 187, 106));
            SetProperty("PressedColor", Color.FromArgb(56, 142, 60));
        }

        /// <summary>
        /// Applies the Red theme preset
        /// </summary>
        public void RedTheme()
        {
            SetProperty("PrimaryColor", Color.FromArgb(244, 67, 54));
            SetProperty("TextColor", Color.White);
            SetProperty("BorderColor", Color.FromArgb(244, 67, 54));
            SetProperty("HoverColor", Color.FromArgb(239, 83, 80));
            SetProperty("PressedColor", Color.FromArgb(211, 47, 47));
        }

        /// <summary>
        /// Applies the Purple theme preset
        /// </summary>
        public void PurpleTheme()
        {
            SetProperty("PrimaryColor", Color.FromArgb(156, 39, 176));
            SetProperty("TextColor", Color.White);
            SetProperty("BorderColor", Color.FromArgb(156, 39, 176));
            SetProperty("HoverColor", Color.FromArgb(171, 71, 188));
            SetProperty("PressedColor", Color.FromArgb(123, 31, 162));
        }

        /// <summary>
        /// Resets the button to default settings
        /// </summary>
        public void ResetToDefault()
        {
            SetProperty("Text", "SimpleButton");
            SetProperty("Style", ButtonStyle.Rounded);
            SetProperty("PrimaryColor", Color.FromArgb(0, 122, 204));
            SetProperty("TextColor", Color.White);
            SetProperty("BorderColor", Color.FromArgb(0, 122, 204));
            SetProperty("HoverColor", Color.FromArgb(0, 142, 224));
            SetProperty("PressedColor", Color.FromArgb(0, 102, 184));
            SetProperty("BorderRadius", 8);
            SetProperty("BorderWidth", 2);
            SetProperty("PredefinedIconType", PredefinedIcon.None);
            SetProperty("Icon", null);
            SetProperty("IconPosition", ImagePosition.Left);
        }

        #endregion

        #region GetSortedActionItems

        /// <summary>
        /// Returns the list of designer action items
        /// </summary>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            // Quick Settings Section
            items.Add(new DesignerActionHeaderItem("Quick Settings"));
            items.Add(new DesignerActionPropertyItem("Text", "Text", "Quick Settings", "The text displayed on the button"));
            items.Add(new DesignerActionPropertyItem("Style", "Style", "Quick Settings", "The visual style of the button"));
            items.Add(new DesignerActionPropertyItem("PrimaryColor", "Primary Color", "Quick Settings", "The primary background color"));
            items.Add(new DesignerActionPropertyItem("BorderRadius", "Border Radius", "Quick Settings", "The radius of rounded corners"));

            // Icon Settings Section
            items.Add(new DesignerActionHeaderItem("Icon Settings"));
            items.Add(new DesignerActionMethodItem(this, "SelectIcon", "Select Icon...", "Icon Settings", "Open the icon picker dialog", true));
            items.Add(new DesignerActionPropertyItem("Icon", "Icon", "Icon Settings", "Select a predefined icon"));
            items.Add(new DesignerActionPropertyItem("IconPosition", "Icon Position", "Icon Settings", "The position of the icon"));

            // Theme Presets Section
            items.Add(new DesignerActionHeaderItem("Theme Presets"));
            items.Add(new DesignerActionMethodItem(this, "LightTheme", "Light Theme", "Theme Presets", "Apply light theme colors", true));
            items.Add(new DesignerActionMethodItem(this, "DarkTheme", "Dark Theme", "Theme Presets", "Apply dark theme colors", true));
            items.Add(new DesignerActionMethodItem(this, "BlueTheme", "Blue Theme", "Theme Presets", "Apply blue theme colors", true));
            items.Add(new DesignerActionMethodItem(this, "GreenTheme", "Green Theme", "Theme Presets", "Apply green theme colors", true));
            items.Add(new DesignerActionMethodItem(this, "RedTheme", "Red Theme", "Theme Presets", "Apply red theme colors", true));
            items.Add(new DesignerActionMethodItem(this, "PurpleTheme", "Purple Theme", "Theme Presets", "Apply purple theme colors", true));

            // Actions Section
            items.Add(new DesignerActionHeaderItem("Actions"));
            items.Add(new DesignerActionMethodItem(this, "ResetToDefault", "Reset to Default", "Actions", "Reset all settings to default values", true));

            return items;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Helper method to set a property and notify the designer
        /// </summary>
        private void SetProperty(string propertyName, object value)
        {
            PropertyDescriptor property = TypeDescriptor.GetProperties(_button)[propertyName];
            if (property != null)
            {
                property.SetValue(_button, value);
            }

            // Refresh the smart tag panel
            _designerActionUIService?.Refresh(_button);
        }

        #endregion
    }
}