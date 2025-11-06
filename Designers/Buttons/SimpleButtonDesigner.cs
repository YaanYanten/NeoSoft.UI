using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Collections;

namespace NeoSoft.UI.Designers
{
    /// <summary>
    /// Custom designer for SimpleButton that provides smart tags and design-time support
    /// </summary>
    public class SimpleButtonDesigner : ControlDesigner
    {
        private DesignerActionListCollection _actionLists;

        /// <summary>
        /// Gets the design-time action lists supported by the component
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.Add(new SimpleButtonActionList(Component));
                }
                return _actionLists;
            }
        }

        /// <summary>
        /// Initializes the designer
        /// </summary>
        /// <param name="component">The component being designed</param>
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

            // Enable design-time hit testing
            EnableDesignMode(Control, "SimpleButton");
        }

        /// <summary>
        /// Filters properties to hide or customize in the property grid
        /// </summary>
        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);

            // Hide properties that are not relevant for this control
            string[] propertiesToHide = new string[]
            {
                "BackgroundImage",
                "BackgroundImageLayout",
                "RightToLeft",
                "ImeMode"
            };

            foreach (string propName in propertiesToHide)
            {
                if (properties.Contains(propName))
                {
                    properties.Remove(propName);
                }
            }
        }

        /// <summary>
        /// Gets the selection rules for the control
        /// </summary>
        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.AllSizeable | SelectionRules.Moveable | SelectionRules.Visible;
            }
        }
    }
}