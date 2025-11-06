using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using NeoSoft.UI.Controls;
using NeoSoft.UI.Enums;

namespace NeoSoft.UI.Designers
{
    /// <summary>
    /// Provides smart tags (quick actions) for the SimpleButton in the designer
    /// </summary>
    public class SimpleButtonActionList : DesignerActionList
    {
        private SimpleButton _button;
        private DesignerActionUIService _designerActionService;

        public SimpleButtonActionList(IComponent component) : base(component)
        {
            _button = component as SimpleButton;
            _designerActionService = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        #region Quick Action Properties

        /// <summary>
        /// Gets or sets the button text
        /// </summary>
        [DisplayName("Text")]
        [Description("The text displayed on the button")]
        public string ButtonText
        {
            get => _button.Text;
            set
            {
                GetPropertyByName("Text").SetValue(_button, value);
                _designerActionService?.Refresh(_button);
            }
        }

        /// <summary>
        /// Gets or sets the button style
        /// </summary>
        [DisplayName("Style")]
        [Description("The visual style of the button")]
        public ButtonStyle ButtonStyle
        {
            get => _button.Style;
            set
            {
                GetPropertyByName("Style").SetValue(_button, value);
                _designerActionService?.Refresh(_button);
            }
        }

        /// <summary>
        /// Gets or sets the primary color
        /// </summary>
        [DisplayName("Primary Color")]
        [Description("The main background color")]
        public Color PrimaryColor
        {
            get => _button.PrimaryColor;
            set
            {
                GetPropertyByName("PrimaryColor").SetValue(_button, value);
                _designerActionService?.Refresh(_button);
            }
        }

        /// <summary>
        /// Gets or sets the border radius
        /// </summary>
        [DisplayName("Border Radius")]
        [Description("The radius of rounded corners")]
        public int BorderRadius
        {
            get => _button.BorderRadius;
            set
            {
                GetPropertyByName("BorderRadius").SetValue(_button, value);
                _designerActionService?.Refresh(_button);
            }
        }

        /// <summary>
        /// Gets or sets the predefined icon
        /// </summary>
        [DisplayName("Icon")]
        [Description("Select a predefined icon")]
        public PredefinedIcon IconType
        {
            get => _button.PredefinedIconType;
            set
            {
                GetPropertyByName("PredefinedIconType").SetValue(_button, value);
                _designerActionService?.Refresh(_button);
            }
        }

        /// <summary>
        /// Gets or sets the icon position
        /// </summary>
        [DisplayName("Icon Position")]
        [Description("Position of the icon relative to text")]
        public ImagePosition IconPosition
        {
            get => _button.IconPosition;
            set
            {
                GetPropertyByName("IconPosition").SetValue(_button, value);
                _designerActionService?.Refresh(_button);
            }
        }

        #endregion

        #region Quick Action Methods

        /// <summary>
        /// Open Image Picker dialog to select an icon
        /// </summary>
        public void SelectIcon()
        {
            using (var dialog = new NeoSoft.UI.Dialogs.ImagePickerDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (dialog.SelectedIcon != PredefinedIcon.None)
                    {
                        _button.PredefinedIconType = dialog.SelectedIcon;
                    }
                    else if (dialog.SelectedImage != null)
                    {
                        _button.Icon = dialog.SelectedImage;
                    }

                    _designerActionService?.Refresh(_button);
                }
            }
        }

        /// <summary>
        /// Apply a light theme preset
        /// </summary>
        public void ApplyLightTheme()
        {
            _button.PrimaryColor = Color.FromArgb(240, 240, 240);
            _button.HoverColor = Color.FromArgb(220, 220, 220);
            _button.PressedColor = Color.FromArgb(200, 200, 200);
            _button.BorderColor = Color.FromArgb(180, 180, 180);
            _button.TextColor = Color.FromArgb(50, 50, 50);
            _designerActionService?.Refresh(_button);
        }

        /// <summary>
        /// Apply a dark theme preset
        /// </summary>
        public void ApplyDarkTheme()
        {
            _button.PrimaryColor = Color.FromArgb(45, 45, 48);
            _button.HoverColor = Color.FromArgb(62, 62, 66);
            _button.PressedColor = Color.FromArgb(27, 27, 28);
            _button.BorderColor = Color.FromArgb(63, 63, 70);
            _button.TextColor = Color.White;
            _designerActionService?.Refresh(_button);
        }

        /// <summary>
        /// Apply a blue theme preset
        /// </summary>
        public void ApplyBlueTheme()
        {
            _button.PrimaryColor = Color.FromArgb(0, 122, 204);
            _button.HoverColor = Color.FromArgb(0, 142, 224);
            _button.PressedColor = Color.FromArgb(0, 102, 184);
            _button.BorderColor = Color.FromArgb(0, 122, 204);
            _button.TextColor = Color.White;
            _designerActionService?.Refresh(_button);
        }

        /// <summary>
        /// Apply a green theme preset
        /// </summary>
        public void ApplyGreenTheme()
        {
            _button.PrimaryColor = Color.FromArgb(16, 185, 129);
            _button.HoverColor = Color.FromArgb(5, 150, 105);
            _button.PressedColor = Color.FromArgb(4, 120, 87);
            _button.BorderColor = Color.FromArgb(16, 185, 129);
            _button.TextColor = Color.White;
            _designerActionService?.Refresh(_button);
        }

        /// <summary>
        /// Apply a red theme preset
        /// </summary>
        public void ApplyRedTheme()
        {
            _button.PrimaryColor = Color.FromArgb(239, 68, 68);
            _button.HoverColor = Color.FromArgb(220, 38, 38);
            _button.PressedColor = Color.FromArgb(185, 28, 28);
            _button.BorderColor = Color.FromArgb(239, 68, 68);
            _button.TextColor = Color.White;
            _designerActionService?.Refresh(_button);
        }

        /// <summary>
        /// Apply a purple theme preset
        /// </summary>
        public void ApplyPurpleTheme()
        {
            _button.PrimaryColor = Color.FromArgb(139, 92, 246);
            _button.HoverColor = Color.FromArgb(124, 58, 237);
            _button.PressedColor = Color.FromArgb(109, 40, 217);
            _button.BorderColor = Color.FromArgb(139, 92, 246);
            _button.TextColor = Color.White;
            _designerActionService?.Refresh(_button);
        }

        /// <summary>
        /// Reset to default settings
        /// </summary>
        public void ResetToDefault()
        {
            _button.Style = ButtonStyle.Rounded;
            _button.PrimaryColor = Color.FromArgb(0, 122, 204);
            _button.HoverColor = Color.FromArgb(0, 142, 224);
            _button.PressedColor = Color.FromArgb(0, 102, 184);
            _button.BorderColor = Color.FromArgb(0, 122, 204);
            _button.TextColor = Color.White;
            _button.BorderRadius = 8;
            _button.BorderWidth = 2;
            _button.PredefinedIconType = PredefinedIcon.None;
            _button.IconPosition = ImagePosition.Left;
            _designerActionService?.Refresh(_button);
        }

        #endregion

        #region Designer Action Items

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            // Header
            items.Add(new DesignerActionHeaderItem("Quick Settings"));

            // Properties
            items.Add(new DesignerActionPropertyItem("ButtonText",
                "Text", "Quick Settings", "Change the button text"));

            items.Add(new DesignerActionPropertyItem("ButtonStyle",
                "Style", "Quick Settings", "Change the visual style"));

            items.Add(new DesignerActionPropertyItem("PrimaryColor",
                "Primary Color", "Quick Settings", "Change the main color"));

            items.Add(new DesignerActionPropertyItem("BorderRadius",
                "Border Radius", "Quick Settings", "Adjust corner roundness"));

            // Icon section
            items.Add(new DesignerActionHeaderItem("Icon Settings"));

            items.Add(new DesignerActionMethodItem(this, "SelectIcon",
                "Select Icon...", "Icon Settings", "Open image picker to select an icon", true));

            items.Add(new DesignerActionPropertyItem("IconType",
                "Icon", "Icon Settings", "Select a predefined icon"));

            items.Add(new DesignerActionPropertyItem("IconPosition",
                "Icon Position", "Icon Settings", "Position of the icon"));

            // Theme presets
            items.Add(new DesignerActionHeaderItem("Theme Presets"));

            items.Add(new DesignerActionMethodItem(this, "ApplyLightTheme",
                "Light Theme", "Theme Presets", "Apply light color theme", true));

            items.Add(new DesignerActionMethodItem(this, "ApplyDarkTheme",
                "Dark Theme", "Theme Presets", "Apply dark color theme", true));

            items.Add(new DesignerActionMethodItem(this, "ApplyBlueTheme",
                "Blue Theme", "Theme Presets", "Apply blue color theme", true));

            items.Add(new DesignerActionMethodItem(this, "ApplyGreenTheme",
                "Green Theme", "Theme Presets", "Apply green color theme", true));

            items.Add(new DesignerActionMethodItem(this, "ApplyRedTheme",
                "Red Theme", "Theme Presets", "Apply red color theme", true));

            items.Add(new DesignerActionMethodItem(this, "ApplyPurpleTheme",
                "Purple Theme", "Theme Presets", "Apply purple color theme", true));

            // Reset
            items.Add(new DesignerActionHeaderItem("Actions"));

            items.Add(new DesignerActionMethodItem(this, "ResetToDefault",
                "Reset to Default", "Actions", "Reset all settings to default values", true));

            return items;
        }

        #endregion

        #region Helper Methods

        private PropertyDescriptor GetPropertyByName(string propertyName)
        {
            PropertyDescriptor property = TypeDescriptor.GetProperties(_button)[propertyName];
            if (property == null)
            {
                throw new ArgumentException($"Property {propertyName} not found", nameof(propertyName));
            }
            return property;
        }

        #endregion
    }
}