using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NeoSoft.UI.Helpers
{
    /// <summary>
    /// Helper class to load icons from embedded resources
    /// </summary>
    public static class IconResourceLoader
    {
        private static Dictionary<string, List<IconResource>> _iconCache;
        private static Assembly _assembly;

        /// <summary>
        /// Icon resource information
        /// </summary>
        public class IconResource
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public int Size { get; set; }
            public string ResourceName { get; set; }
            public Image Image { get; set; }
        }

        static IconResourceLoader()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _iconCache = new Dictionary<string, List<IconResource>>();
            LoadAllIcons();
        }

        /// <summary>
        /// Loads all icons from embedded resources
        /// </summary>
        private static void LoadAllIcons()
        {
            try
            {
                // Obtener todos los recursos embebidos
                string[] resourceNames = _assembly.GetManifestResourceNames();

                // Filtrar solo imágenes
                var imageResources = resourceNames.Where(r =>
                    r.Contains(".Images.") &&
                    (r.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                     r.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                     r.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                     r.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                ).ToList();

                foreach (var resourceName in imageResources)
                {
                    try
                    {
                        // Parsear el nombre del recurso
                        var parts = resourceName.Split('.');

                        if (parts.Length < 6) continue;

                        // Extraer información
                        string category = ExtractCategory(resourceName);
                        string iconName = ExtractIconName(resourceName);
                        int size = ExtractSize(resourceName);

                        // Cargar la imagen
                        using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
                        {
                            if (stream != null)
                            {
                                // CRÍTICO: Crear Bitmap independiente del stream
                                // NO usar Image.FromStream directamente porque se invalida al cerrar el stream
                                Image tempImage = Image.FromStream(stream);
                                Image independentImage = new Bitmap(tempImage);
                                tempImage.Dispose();

                                IconResource iconResource = new IconResource
                                {
                                    Name = iconName,
                                    Category = category,
                                    Size = size,
                                    ResourceName = resourceName,
                                    Image = independentImage  // ← Imagen independiente
                                };

                                // Agregar al caché por categoría
                                if (!_iconCache.ContainsKey(category))
                                {
                                    _iconCache[category] = new List<IconResource>();
                                }

                                _iconCache[category].Add(iconResource);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error pero continuar con otros iconos
                        System.Diagnostics.Debug.WriteLine($"Error loading icon {resourceName}: {ex.Message}");
                    }
                }

                System.Diagnostics.Debug.WriteLine($"✓ IconResourceLoader: {_iconCache.Sum(c => c.Value.Count)} iconos cargados");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading icons: {ex.Message}");
            }
        }

        /// <summary>
        /// Extracts category from resource name
        /// Example: NeoSoft.UI.Resources.Images.Actions.ICONS_16.Add_x_16.png → Actions
        /// </summary>
        private static string ExtractCategory(string resourceName)
        {
            try
            {
                var parts = resourceName.Split('.');

                // Buscar "Images" y tomar la siguiente parte
                int imagesIndex = Array.FindIndex(parts, p => p.Equals("Images", StringComparison.OrdinalIgnoreCase));

                if (imagesIndex >= 0 && imagesIndex + 1 < parts.Length)
                {
                    return parts[imagesIndex + 1];
                }
            }
            catch { }

            return "Other";
        }

        /// <summary>
        /// Extracts icon name from resource name
        /// Example: Add_x_16.png → Add
        /// </summary>
        private static string ExtractIconName(string resourceName)
        {
            try
            {
                // Obtener el nombre del archivo sin extensión
                string fileName = Path.GetFileNameWithoutExtension(resourceName);

                // Remover el sufijo de tamaño (_x_16, _x_32, etc.)
                if (fileName.Contains("_x_"))
                {
                    int index = fileName.LastIndexOf("_x_");
                    return fileName.Substring(0, index);
                }

                return fileName;
            }
            catch
            {
                return Path.GetFileNameWithoutExtension(resourceName);
            }
        }

        /// <summary>
        /// Extracts size from resource name
        /// Example: Add_x_16.png → 16
        /// </summary>
        private static int ExtractSize(string resourceName)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(resourceName);

                if (fileName.Contains("_x_"))
                {
                    string[] parts = fileName.Split(new[] { "_x_" }, StringSplitOptions.None);
                    if (parts.Length > 1)
                    {
                        if (int.TryParse(parts[1], out int size))
                        {
                            return size;
                        }
                    }
                }

                // Si no se puede extraer, buscar en el path si dice ICONS_16 o ICONS_32
                if (resourceName.Contains("ICONS_16"))
                    return 16;
                if (resourceName.Contains("ICONS_32"))
                    return 32;
            }
            catch { }

            return 16; // Default
        }

        /// <summary>
        /// Gets all available categories
        /// </summary>
        public static List<string> GetCategories()
        {
            var categories = _iconCache.Keys.OrderBy(k => k).ToList();
            categories.Insert(0, "All"); // Agregar "All" al inicio
            return categories;
        }

        /// <summary>
        /// Gets icons by category and size
        /// </summary>
        public static List<IconResource> GetIcons(string category = "All", int size = 0)
        {
            List<IconResource> result = new List<IconResource>();

            if (category == "All")
            {
                // Retornar todos los iconos
                foreach (var cat in _iconCache.Values)
                {
                    result.AddRange(cat);
                }
            }
            else if (_iconCache.ContainsKey(category))
            {
                result.AddRange(_iconCache[category]);
            }

            // Filtrar por tamaño si se especificó
            if (size > 0)
            {
                result = result.Where(i => i.Size == size).ToList();
            }

            // Ordenar por nombre
            return result.OrderBy(i => i.Name).ToList();
        }

        /// <summary>
        /// Gets available sizes for a category
        /// </summary>
        public static List<int> GetAvailableSizes(string category = "All")
        {
            var icons = GetIcons(category);
            return icons.Select(i => i.Size).Distinct().OrderBy(s => s).ToList();
        }

        /// <summary>
        /// Gets an icon by name and size
        /// </summary>
        public static Image GetIcon(string name, int size = 16)
        {
            foreach (var category in _iconCache.Values)
            {
                var icon = category.FirstOrDefault(i =>
                    i.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                    i.Size == size);

                if (icon != null)
                {
                    // CRÍTICO: Retornar copia independiente
                    return new Bitmap(icon.Image);
                }
            }

            return null;
        }

        /// <summary>
        /// Clears the icon cache
        /// </summary>
        public static void ClearCache()
        {
            foreach (var category in _iconCache.Values)
            {
                foreach (var icon in category)
                {
                    icon.Image?.Dispose();
                }
            }
            _iconCache.Clear();
            LoadAllIcons();
        }
    }
}