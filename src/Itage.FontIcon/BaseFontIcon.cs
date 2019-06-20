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
    public abstract class BaseFontIcon<T> : Border where T : struct, Enum
    {
        private static readonly SolidColorBrush DefaultBrush;
        static BaseFontIcon()
        {
            DefaultBrush = new SolidColorBrush(Colors.Black);
            DefaultBrush.Freeze();
        }
        public BaseFontIcon()
        {
            FontFamily = DefaultFontFamily;
            Foreground = DefaultBrush;
            Size = 16;
            Width = 16;
            Height = 16;
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
        /// <summary>
        /// Icon property
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(T), typeof(BaseFontIcon<T>), new FrameworkPropertyMetadata((T)(ValueType)0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Font family
        /// </summary>
        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(BaseFontIcon<T>), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));


        /// <summary>
        /// Icon foreground brush
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Foreground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(BaseFontIcon<T>), new FrameworkPropertyMetadata(DefaultBrush,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Icon size.
        /// </summary>
        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(nameof(Size), typeof(double), typeof(BaseFontIcon<T>),
                new FrameworkPropertyMetadata(16d, FrameworkPropertyMetadataOptions.AffectsRender, OnSizePropertyChanged));

        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double size = (double)e.NewValue;
            d.SetValue(WidthProperty, size);
            d.SetValue(HeightProperty, size);
            d.SetValue(MaxWidthProperty, size);
            d.SetValue(MaxHeightProperty, size);
            d.SetValue(MinWidthProperty, size);
            d.SetValue(MinHeightProperty, size);
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var typeface = new Typeface(FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            if (typeface.TryGetGlyphTypeface(out var gtf))
            {
                var iconSymbol = (char)(int)(ValueType)(Icon);
                var glyph = ConvertTextLinesToGlyphRun(gtf, Size, iconSymbol);
                dc.DrawGlyphRun(Foreground, glyph);
            }
        }

        static GlyphRun ConvertTextLinesToGlyphRun(GlyphTypeface glyphTypeface, double fontSize, char symbol)
        {
            var baselineOrigin = new Point(0, -glyphTypeface.Baseline * fontSize);

            var advanceWidths = new double[] { 0 };
            var glyphIndices = new[] { glyphTypeface.CharacterToGlyphMap[symbol] };
            var glyphOffsets = new Point[] { baselineOrigin };

#pragma warning disable CS0618 // Type or member is obsolete
            var run = new GlyphRun(
                glyphTypeface,
                0,
                false,
                fontSize,
                glyphIndices,
                new Point(),
                advanceWidths,
                glyphOffsets,
                null,
                null,
                null,
                null,
                null);
#pragma warning restore CS0618 // Type or member is obsolete
            return run;
        }

    }
}
