using System;

namespace NeoSoft.UI.Enums
{
    /// <summary>
    /// Defines the visual style of the button
    /// </summary>
    public enum ButtonStyle
    {
        /// <summary>
        /// Default flat style with solid colors
        /// </summary>
        Flat,

        /// <summary>
        /// Rounded corners with smooth edges
        /// </summary>
        Rounded,

        /// <summary>
        /// Outlined style with transparent background
        /// </summary>
        Outline,

        /// <summary>
        /// Gradient background from top to bottom
        /// </summary>
        Gradient,

        /// <summary>
        /// Material Design style with elevation shadow
        /// </summary>
        Material,

        /// <summary>
        /// Glass effect with transparency
        /// </summary>
        Glass
    }

    /// <summary>
    /// Defines the position of the icon relative to text
    /// </summary>
    public enum ImagePosition
    {
        /// <summary>
        /// Icon positioned to the left of text
        /// </summary>
        Left,

        /// <summary>
        /// Icon positioned to the right of text
        /// </summary>
        Right,

        /// <summary>
        /// Icon positioned above text
        /// </summary>
        Top,

        /// <summary>
        /// Icon positioned below text
        /// </summary>
        Bottom,

        /// <summary>
        /// Icon centered, no text visible
        /// </summary>
        Center
    }

    /// <summary>
    /// Predefined icon types for quick selection
    /// </summary>
    public enum PredefinedIcon
    {
        None,
        Save,
        Delete,
        Edit,
        Add,
        Remove,
        Refresh,
        Search,
        Settings,
        Close,
        Check,
        Cancel,
        Upload,
        Download,
        Print,
        Copy,
        Paste,
        Cut,
        Undo,
        Redo,
        Home,
        Back,
        Forward,
        Info,
        Warning,
        Error,
        Success
    }

    /// <summary>
    /// Available theme presets
    /// </summary>
    public enum ThemePreset
    {
        Light,
        Dark,
        Blue,
        Green,
        Purple,
        Orange,
        Custom
    }
}