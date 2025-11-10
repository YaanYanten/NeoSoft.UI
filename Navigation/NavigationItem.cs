using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NeoSoft.UI.Navigation
{
    /// <summary>
    /// Representa un elemento en el menú de navegación
    /// </summary>
    public class NavigationItem
    {
        #region Properties

        /// <summary>
        /// ID único del elemento
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Texto a mostrar
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Icono del elemento
        /// </summary>
        public Image Icon { get; set; }

        /// <summary>
        /// Tooltip
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// Tag para datos adicionales
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Elementos hijos
        /// </summary>
        public List<NavigationItem> Children { get; set; }

        /// <summary>
        /// Elemento padre
        /// </summary>
        public NavigationItem Parent { get; set; }

        /// <summary>
        /// Indica si es un separador
        /// </summary>
        public bool IsSeparator { get; set; }

        /// <summary>
        /// Indica si es un grupo
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// Indica si el grupo está expandido
        /// </summary>
        public bool IsExpanded { get; set; }

        /// <summary>
        /// Nivel de profundidad (0 = raíz)
        /// </summary>
        public int Level
        {
            get
            {
                int level = 0;
                NavigationItem current = Parent;
                while (current != null)
                {
                    level++;
                    current = current.Parent;
                }
                return level;
            }
        }

        #endregion

        #region Constructor

        public NavigationItem()
        {
            Id = Guid.NewGuid().ToString();
            Children = new List<NavigationItem>();
            IsExpanded = false;
        }

        public NavigationItem(string text) : this()
        {
            Text = text;
        }

        public NavigationItem(string text, Image icon) : this(text)
        {
            Icon = icon;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Agregar un hijo
        /// </summary>
        public void AddChild(NavigationItem item)
        {
            item.Parent = this;
            Children.Add(item);
            IsGroup = true;
        }

        /// <summary>
        /// Remover un hijo
        /// </summary>
        public void RemoveChild(NavigationItem item)
        {
            item.Parent = null;
            Children.Remove(item);

            if (Children.Count == 0)
                IsGroup = false;
        }

        /// <summary>
        /// Obtener todos los descendientes visibles
        /// </summary>
        public List<NavigationItem> GetVisibleDescendants()
        {
            var result = new List<NavigationItem>();

            if (!IsExpanded)
                return result;

            foreach (var child in Children)
            {
                result.Add(child);
                if (child.IsGroup && child.IsExpanded)
                {
                    result.AddRange(child.GetVisibleDescendants());
                }
            }

            return result;
        }

        /// <summary>
        /// Crear un separador
        /// </summary>
        public static NavigationItem CreateSeparator()
        {
            return new NavigationItem
            {
                IsSeparator = true,
                Text = "-"
            };
        }

        /// <summary>
        /// Crear un grupo
        /// </summary>
        public static NavigationItem CreateGroup(string text, Image icon = null)
        {
            return new NavigationItem
            {
                Text = text,
                Icon = icon,
                IsGroup = true
            };
        }

        #endregion

        #region Override

        public override string ToString()
        {
            return Text ?? "(Sin texto)";
        }

        #endregion
    }
}