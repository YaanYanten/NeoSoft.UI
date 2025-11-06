using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NeoSoft.UI.Controls
{
    /// <summary>
    /// TextBox with placeholder text support for .NET Framework 4.8
    /// </summary>
    public class PlaceholderTextBox : TextBox
    {
        private string _placeholderText = "";
        private Color _placeholderColor = Color.Gray;
        private Color _textColor = Color.Black;
        private bool _isPlaceholder = false;

        /// <summary>
        /// Gets or sets the placeholder text
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The text to display when the textbox is empty")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value;
                if (string.IsNullOrEmpty(Text))
                {
                    ShowPlaceholder();
                }
            }
        }

        /// <summary>
        /// Gets or sets the placeholder text color
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The color of the placeholder text")]
        public Color PlaceholderColor
        {
            get => _placeholderColor;
            set
            {
                _placeholderColor = value;
                if (_isPlaceholder)
                {
                    ForeColor = _placeholderColor;
                }
            }
        }

        /// <summary>
        /// Gets or sets the normal text color
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The color of the normal text")]
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                if (!_isPlaceholder)
                {
                    ForeColor = _textColor;
                }
            }
        }

        /// <summary>
        /// Gets the actual text value (excluding placeholder)
        /// </summary>
        [Browsable(false)]
        public string RealText
        {
            get => _isPlaceholder ? "" : Text;
        }

        public PlaceholderTextBox()
        {
            // Subscribe to events
            this.Enter += PlaceholderTextBox_Enter;
            this.Leave += PlaceholderTextBox_Leave;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(_placeholderText))
            {
                ShowPlaceholder();
            }
        }

        private void PlaceholderTextBox_Enter(object sender, EventArgs e)
        {
            if (_isPlaceholder)
            {
                HidePlaceholder();
            }
        }

        private void PlaceholderTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                ShowPlaceholder();
            }
        }

        private void ShowPlaceholder()
        {
            _isPlaceholder = true;
            Text = _placeholderText;
            ForeColor = _placeholderColor;
        }

        private void HidePlaceholder()
        {
            _isPlaceholder = false;
            Text = "";
            ForeColor = _textColor;
        }

        /// <summary>
        /// Sets the text value programmatically
        /// </summary>
        public void SetText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ShowPlaceholder();
            }
            else
            {
                _isPlaceholder = false;
                Text = text;
                ForeColor = _textColor;
            }
        }

        /// <summary>
        /// Clears the textbox
        /// </summary>
        public new void Clear()
        {
            base.Clear();
            ShowPlaceholder();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Enter -= PlaceholderTextBox_Enter;
                this.Leave -= PlaceholderTextBox_Leave;
            }
            base.Dispose(disposing);
        }
    }
}