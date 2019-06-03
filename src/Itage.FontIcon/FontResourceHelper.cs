using System;
using System.Reflection;
using System.Windows.Media;

namespace Itage.FontIcon
{
    /// <summary>
    /// Helper class for making a font family for embedded fonts
    /// </summary>
    public static class FontResourceHelper
    {

        /// <summary>
        /// Builds a <see cref="FontFamily" /> for font, embedded as resource.
        /// </summary>
        /// <param name="assembly">Assembly, containing font</param>
        /// <param name="resourcePath">Path to fonts folder</param>
        /// <param name="fontName">Font name</param>
        /// <returns>Font family</returns>
        public static FontFamily MakeFontFamily(Assembly assembly, string resourcePath, string fontName)
        {
            string assemblyName = assembly.GetName().Name;
            string assemblyUri = $"pack://application:,,,/{assemblyName};component/{resourcePath.Trim('/')}/";
            return new FontFamily(new Uri(assemblyUri), "./#" + fontName);
        }
    }
}
