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
    /// Custom UI Type Editor for PredefinedIcon property with ellipsis button
    /// </summary>
    public class IconUITypeEditor : UITypeEditor
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
    /// Custom UI Type Editor for Image property with ellipsis button
    /// </summary>
    public class ImageUITypeEditor : UITypeEditor
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
                            if (dialog.SelectedImage != null)
                            {
                                return dialog.SelectedImage;
                            }
                            else if (dialog.SelectedIcon != PredefinedIcon.None)
                            {
                                // Aquí podrías convertir el icono predefinido a Image
                                // Por ahora retornamos el valor actual
                                return value;
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
                e.Graphics.DrawImage(img, e.Bounds);
            }
            else
            {
                // Dibujar un placeholder
                using (Brush brush = new SolidBrush(Color.LightGray))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                using (Pen pen = new Pen(Color.Gray))
                {
                    e.Graphics.DrawRectangle(pen, e.Bounds);
                }
            }
        }
    }
}