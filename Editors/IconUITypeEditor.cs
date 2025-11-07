using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using NeoSoft.UI.Enums;

namespace NeoSoft.UI.Editors
{
    /// <summary>
    /// Custom UI Type Editor for PredefinedIconType property with ellipsis button
    /// Opens ImagePickerDialog and returns the selected PredefinedIcon enum value
    /// </summary>
    public class PredefinedIconUITypeEditor : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style - Modal dialog
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Edits the value by showing the Image Picker dialog
        /// </summary>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService editorService =
                    provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

                if (editorService != null)
                {
                    using (var dialog = new NeoSoft.UI.Dialogs.ImagePickerDialog())
                    {
                        // Set current value if it's a PredefinedIcon
                        if (value is PredefinedIcon currentIcon)
                        {
                            dialog.SetInitialIcon(currentIcon);
                        }

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            // Return the selected predefined icon
                            if (dialog.SelectedIcon != PredefinedIcon.None)
                            {
                                return dialog.SelectedIcon;
                            }
                        }
                    }
                }
            }

            return value;
        }
    }

    /// <summary>
    /// Custom UI Type Editor for Icon (Image) property with ellipsis button
    /// Opens ImagePickerDialog and returns the selected Image
    /// </summary>
    public class ImageIconUITypeEditor : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style - Modal dialog
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Edits the value by showing the Image Picker dialog
        /// </summary>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService editorService =
                    provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

                if (editorService != null)
                {
                    using (var dialog = new NeoSoft.UI.Dialogs.ImagePickerDialog())
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            // Priority 1: Custom image
                            if (dialog.SelectedImage != null)
                            {
                                return dialog.SelectedImage;
                            }
                            // Priority 2: Predefined icon converted to Image
                            else if (dialog.SelectedIcon != PredefinedIcon.None)
                            {
                                // Get the control instance to call GetPredefinedIcon
                                if (context?.Instance is NeoSoft.UI.Controls.SimpleButton button)
                                {
                                    // Trigger the PredefinedIconType property to update Icon
                                    button.PredefinedIconType = dialog.SelectedIcon;
                                    return button.Icon;
                                }
                            }
                        }
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Indicates we can paint a representation of the value
        /// </summary>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Paints a representation of the value
        /// </summary>
        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value is Image img)
            {
                // Draw the actual image
                e.Graphics.DrawImage(img, e.Bounds);
            }
            else
            {
                // Draw a placeholder
                using (Brush brush = new SolidBrush(Color.LightGray))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                using (Pen pen = new Pen(Color.DarkGray))
                {
                    e.Graphics.DrawRectangle(pen, e.Bounds);

                // Draw an "X" to indicate no image
                pen.Width = 2;
                e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom);
                e.Graphics.DrawLine(pen, e.Bounds.Right, e.Bounds.Top, e.Bounds.Left, e.Bounds.Bottom);
                }
            }
        }
    }
}