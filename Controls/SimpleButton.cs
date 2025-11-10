using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NeoSoft.UI.Enums;

namespace NeoSoft.UI.Controls
{
    /// <summary>
    /// Advanced button control with customizable styles, icons, and themes
    /// </summary>
    [ToolboxBitmap(typeof(Button))]
    [DefaultEvent("Click")]
    [Designer(typeof(NeoSoft.UI.Designers.SimpleButtonDesigner))]
    public class SimpleButton : Control
    {
        #region Private Fields

        private ButtonStyle _buttonStyle = ButtonStyle.Rounded;
        private Color _primaryColor = Color.FromArgb(0, 122, 204);
        private Color _hoverColor = Color.FromArgb(0, 142, 224);
        private Color _pressedColor = Color.FromArgb(0, 102, 184);
        private Color _borderColor = Color.FromArgb(0, 122, 204);
        private Color _textColor = Color.White;
        private int _borderRadius = 8;
        private int _borderWidth = 2;
        private Image _icon = null;
        private ImagePosition _iconPosition = ImagePosition.Left;
        private int _iconSize = 16;
        private int _iconSpacing = 8;
        private PredefinedIcon _predefinedIcon = PredefinedIcon.None;
        private bool _isHovered = false;
        private bool _isPressed = false;
        private bool _enableAnimation = true;
        private float _animationProgress = 0f;
        private Timer _animationTimer;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the visual style of the button
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The visual style of the button")]
        [DefaultValue(typeof(ButtonStyle), "Rounded")]
        public ButtonStyle Style
        {
            get => _buttonStyle;
            set
            {
                _buttonStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the primary color of the button
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The primary background color")]
        public Color PrimaryColor
        {
            get => _primaryColor;
            set
            {
                _primaryColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color when the mouse hovers over the button
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The color when hovering")]
        public Color HoverColor
        {
            get => _hoverColor;
            set
            {
                _hoverColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color when the button is pressed
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The color when pressed")]
        public Color PressedColor
        {
            get => _pressedColor;
            set
            {
                _pressedColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border color
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The color of the border")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text color
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The color of the text")]
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the radius of rounded corners
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The radius of rounded corners (0-50)")]
        [DefaultValue(8)]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = Math.Max(0, Math.Min(50, value));
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the border
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The width of the border (0-10)")]
        [DefaultValue(2)]
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = Math.Max(0, Math.Min(10, value));
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the icon image
        /// NOTE: This property can be set either directly or through PredefinedIconType
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The icon to display on the button")]
        [Editor(typeof(NeoSoft.UI.Editors.ImageIconUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Image Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                // If setting icon directly, clear predefined icon
                if (value != null)
                {
                    _predefinedIcon = PredefinedIcon.None;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the position of the icon relative to text
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The position of the icon relative to text")]
        [DefaultValue(typeof(ImagePosition), "Left")]
        public ImagePosition IconPosition
        {
            get => _iconPosition;
            set
            {
                _iconPosition = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the size of the icon
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The size of the icon in pixels")]
        [DefaultValue(16)]
        public int IconSize
        {
            get => _iconSize;
            set
            {
                _iconSize = Math.Max(8, Math.Min(64, value));
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the spacing between icon and text
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("The spacing between icon and text")]
        [DefaultValue(8)]
        public int IconSpacing
        {
            get => _iconSpacing;
            set
            {
                _iconSpacing = Math.Max(0, Math.Min(32, value));
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a predefined icon
        /// When set, this automatically updates the Icon property with the corresponding image
        /// </summary>
        [Category("NeoSoft Appearance")]
        [Description("Select a predefined icon")]
        [DefaultValue(typeof(PredefinedIcon), "None")]
        [Editor(typeof(NeoSoft.UI.Editors.PredefinedIconUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public PredefinedIcon PredefinedIconType
        {
            get => _predefinedIcon;
            set
            {
                _predefinedIcon = value;
                if (value != PredefinedIcon.None)
                {
                    _icon = GetPredefinedIcon(value);
                }
                else
                {
                    _icon = null;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets whether animations are enabled
        /// </summary>
        [Category("NeoSoft Behavior")]
        [Description("Enable or disable animations")]
        [DefaultValue(true)]
        public bool EnableAnimation
        {
            get => _enableAnimation;
            set => _enableAnimation = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the SimpleButton class
        /// </summary>
        public SimpleButton()
        {
            // Double buffering for smooth rendering
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor, true);

            // Default settings
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Size = new Size(150, 40);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            Cursor = Cursors.Hand;

            // Animation timer
            _animationTimer = new Timer();
            _animationTimer.Interval = 16; // ~60 FPS
            _animationTimer.Tick += AnimationTimer_Tick;
        }

        #endregion

        #region Painting Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Get current color based on state
            Color currentColor = GetCurrentColor();

            // Draw based on style
            switch (_buttonStyle)
            {
                case ButtonStyle.Flat:
                    DrawFlatStyle(graphics, currentColor);
                    break;
                case ButtonStyle.Rounded:
                    DrawRoundedStyle(graphics, currentColor);
                    break;
                case ButtonStyle.Outline:
                    DrawOutlineStyle(graphics, currentColor);
                    break;
                case ButtonStyle.Gradient:
                    DrawGradientStyle(graphics, currentColor);
                    break;
                case ButtonStyle.Material:
                    DrawMaterialStyle(graphics, currentColor);
                    break;
                case ButtonStyle.Glass:
                    DrawGlassStyle(graphics, currentColor);
                    break;
            }

            // Draw content (icon and text)
            DrawContent(graphics);
        }

        private void DrawFlatStyle(Graphics g, Color color)
        {
            Rectangle rect = ClientRectangle;
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, rect);
            }
        }

        private void DrawRoundedStyle(Graphics g, Color color)
        {
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = GetRoundedRectangle(rect, _borderRadius))
            {
                // Fill background
                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillPath(brush, path);
                }

                // Draw border
                if (_borderWidth > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderWidth))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        private void DrawOutlineStyle(Graphics g, Color color)
        {
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = GetRoundedRectangle(rect, _borderRadius))
            {
                // Transparent background
                using (SolidBrush brush = new SolidBrush(Color.Transparent))
                {
                    g.FillPath(brush, path);
                }

                // Draw border
                using (Pen pen = new Pen(color, _borderWidth))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        private void DrawGradientStyle(Graphics g, Color color)
        {
            Rectangle rect = ClientRectangle;
            Color lightColor = ControlPaint.Light(color);

            using (GraphicsPath path = GetRoundedRectangle(rect, _borderRadius))
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, lightColor, color, LinearGradientMode.Vertical))
            {
                g.FillPath(brush, path);

                // Draw border
                if (_borderWidth > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderWidth))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        private void DrawMaterialStyle(Graphics g, Color color)
        {
            Rectangle rect = new Rectangle(2, 2, Width - 5, Height - 5);

            // Draw shadow
            if (!_isPressed)
            {
                using (GraphicsPath shadowPath = GetRoundedRectangle(
                    new Rectangle(4, 4, Width - 5, Height - 5), _borderRadius))
                using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                {
                    shadowBrush.CenterColor = Color.FromArgb(50, 0, 0, 0);
                    shadowBrush.SurroundColors = new[] { Color.Transparent };
                    g.FillPath(shadowBrush, shadowPath);
                }
            }

            // Draw button
            using (GraphicsPath path = GetRoundedRectangle(rect, _borderRadius))
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillPath(brush, path);
            }
        }

        private void DrawGlassStyle(Graphics g, Color color)
        {
            Rectangle rect = ClientRectangle;
            using (GraphicsPath path = GetRoundedRectangle(rect, _borderRadius))
            {
                // Semi-transparent background
                Color glassColor = Color.FromArgb(180, color.R, color.G, color.B);
                using (SolidBrush brush = new SolidBrush(glassColor))
                {
                    g.FillPath(brush, path);
                }

                // Glass reflection
                Rectangle topHalf = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height / 2);
                using (LinearGradientBrush glassBrush = new LinearGradientBrush(
                    topHalf, Color.FromArgb(80, 255, 255, 255), Color.Transparent,
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(glassBrush, topHalf);
                }

                // Border
                using (Pen pen = new Pen(_borderColor, _borderWidth))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        private void DrawContent(Graphics g)
        {
            Rectangle contentRect = ClientRectangle;

            // Calculate text and icon sizes
            SizeF textSize = string.IsNullOrEmpty(Text) ? SizeF.Empty :
                g.MeasureString(Text, Font);

            bool hasIcon = _icon != null;
            bool hasText = !string.IsNullOrEmpty(Text);

            if (_iconPosition == ImagePosition.Center || !hasText)
            {
                // Draw icon centered
                if (hasIcon)
                {
                    DrawIconCentered(g, contentRect);
                }
                else if (hasText)
                {
                    DrawTextCentered(g, contentRect, textSize);
                }
            }
            else
            {
                // Draw icon and text based on position
                switch (_iconPosition)
                {
                    case ImagePosition.Left:
                        DrawIconAndTextHorizontal(g, contentRect, textSize, true);
                        break;
                    case ImagePosition.Right:
                        DrawIconAndTextHorizontal(g, contentRect, textSize, false);
                        break;
                    case ImagePosition.Top:
                        DrawIconAndTextVertical(g, contentRect, textSize, true);
                        break;
                    case ImagePosition.Bottom:
                        DrawIconAndTextVertical(g, contentRect, textSize, false);
                        break;
                }
            }
        }

        private void DrawIconCentered(Graphics g, Rectangle rect)
        {
            int x = (rect.Width - _iconSize) / 2;
            int y = (rect.Height - _iconSize) / 2;
            DrawIcon(g, new Point(x, y));
        }

        private void DrawTextCentered(Graphics g, Rectangle rect, SizeF textSize)
        {
            float x = (rect.Width - textSize.Width) / 2;
            float y = (rect.Height - textSize.Height) / 2;

            using (SolidBrush brush = new SolidBrush(_textColor))
            {
                g.DrawString(Text, Font, brush, x, y);
            }
        }

        private void DrawIconAndTextHorizontal(Graphics g, Rectangle rect, SizeF textSize, bool iconLeft)
        {
            bool hasIcon = _icon != null;
            int totalWidth = (hasIcon ? _iconSize + _iconSpacing : 0) + (int)textSize.Width;
            int startX = (rect.Width - totalWidth) / 2;
            int centerY = rect.Height / 2;

            if (iconLeft && hasIcon)
            {
                // Draw icon then text
                DrawIcon(g, new Point(startX, centerY - _iconSize / 2));

                float textX = startX + _iconSize + _iconSpacing;
                float textY = centerY - textSize.Height / 2;
                using (SolidBrush brush = new SolidBrush(_textColor))
                {
                    g.DrawString(Text, Font, brush, textX, textY);
                }
            }
            else
            {
                // Draw text then icon
                float textX = startX;
                float textY = centerY - textSize.Height / 2;
                using (SolidBrush brush = new SolidBrush(_textColor))
                {
                    g.DrawString(Text, Font, brush, textX, textY);
                }

                if (hasIcon)
                {
                    int iconX = startX + (int)textSize.Width + _iconSpacing;
                    DrawIcon(g, new Point(iconX, centerY - _iconSize / 2));
                }
            }
        }

        private void DrawIconAndTextVertical(Graphics g, Rectangle rect, SizeF textSize, bool iconTop)
        {
            bool hasIcon = _icon != null;
            int totalHeight = (hasIcon ? _iconSize + _iconSpacing : 0) + (int)textSize.Height;
            int startY = (rect.Height - totalHeight) / 2;
            int centerX = rect.Width / 2;

            if (iconTop && hasIcon)
            {
                // Draw icon then text
                DrawIcon(g, new Point(centerX - _iconSize / 2, startY));

                float textX = centerX - textSize.Width / 2;
                float textY = startY + _iconSize + _iconSpacing;
                using (SolidBrush brush = new SolidBrush(_textColor))
                {
                    g.DrawString(Text, Font, brush, textX, textY);
                }
            }
            else
            {
                // Draw text then icon
                float textX = centerX - textSize.Width / 2;
                float textY = startY;
                using (SolidBrush brush = new SolidBrush(_textColor))
                {
                    g.DrawString(Text, Font, brush, textX, textY);
                }

                if (hasIcon)
                {
                    int iconY = startY + (int)textSize.Height + _iconSpacing;
                    DrawIcon(g, new Point(centerX - _iconSize / 2, iconY));
                }
            }
        }

        private void DrawIcon(Graphics g, Point location)
        {
            if (_icon != null)
            {
                g.DrawImage(_icon, location.X, location.Y, _iconSize, _iconSize);
            }
        }

        #endregion

        #region Helper Methods

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (radius == 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private Color GetCurrentColor()
        {
            if (_isPressed)
                return _pressedColor;
            if (_isHovered)
                return InterpolateColor(_primaryColor, _hoverColor, _animationProgress);
            return _primaryColor;
        }

        private Color InterpolateColor(Color from, Color to, float progress)
        {
            int r = (int)(from.R + (to.R - from.R) * progress);
            int g = (int)(from.G + (to.G - from.G) * progress);
            int b = (int)(from.B + (to.B - from.B) * progress);
            return Color.FromArgb(r, g, b);
        }

        // REEMPLAZAR CON:
        /// <summary>
        /// Gets a predefined icon as an Image from IconResourceLoader
        /// </summary>
        public Image GetPredefinedIcon(PredefinedIcon iconType)
        {
            if (iconType == PredefinedIcon.None)
                return null;

            try
            {
                // Cargar desde IconResourceLoader
                string iconName = iconType.ToString();
                Image icon = Helpers.IconResourceLoader.GetIcon(iconName, _iconSize);

                if (icon != null)
                {
                    System.Diagnostics.Debug.WriteLine($"✓ GetPredefinedIcon: {iconName} loaded ({icon.Width}x{icon.Height})");
                    return icon;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"⚠ GetPredefinedIcon: {iconName} not found in resources");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ GetPredefinedIcon error: {ex.Message}");
            }

            return null;
        }

        #endregion

        #region Mouse Events

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;

            if (_enableAnimation)
            {
                _animationTimer.Start();
            }
            else
            {
                _animationProgress = 1f;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _isPressed = false;

            if (_enableAnimation)
            {
                _animationTimer.Start();
            }
            else
            {
                _animationProgress = 0f;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isPressed = false;
            Invalidate();
        }

        #endregion

        #region Animation

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (_isHovered && _animationProgress < 1f)
            {
                _animationProgress += 0.15f;
                if (_animationProgress >= 1f)
                {
                    _animationProgress = 1f;
                    _animationTimer.Stop();
                }
            }
            else if (!_isHovered && _animationProgress > 0f)
            {
                _animationProgress -= 0.15f;
                if (_animationProgress <= 0f)
                {
                    _animationProgress = 0f;
                    _animationTimer.Stop();
                }
            }

            Invalidate();
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}