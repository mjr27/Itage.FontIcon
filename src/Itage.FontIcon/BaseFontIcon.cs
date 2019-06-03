using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Itage.FontIcon
{
    /// <summary>
    /// Base class for font iconx. This should be redefined in new assembly
    /// </summary>
    /// <typeparam name="T">Enum with font glyphs</typeparam>
    public abstract class BaseFontIcon<T> : TextBlock where T : struct, Enum
    {
        public BaseFontIcon()
        {
            SetValue(FontSizeProperty, (double)IconSize.Small);
        }
        /// <summary>
        /// Default font family for font icons
        /// </summary>
        protected static FontFamily DefaultFontFamily { get; set; }

        /// <summary>
        /// Icon glyph
        /// </summary>
        public T Icon
        {
            get { return (T)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(T), typeof(BaseFontIcon<T>), new PropertyMetadata((T)(ValueType)0, OnIconPropertyChanged));

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextAlignmentProperty, TextAlignment.Center);
            d.SetValue(TextProperty, char.ConvertFromUtf32((int)e.NewValue));
        }


        /// <summary>
        /// Icon size.
        /// </summary>
        public IconSize IconSize
        {
            get { return (IconSize)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(IconSize), typeof(BaseFontIcon<T>), new PropertyMetadata(IconSize.Small, OnIconSizePropertyChanged));

        private static void OnIconSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(FontSizeProperty, (double)(IconSize)e.NewValue);
        }

    }
}
