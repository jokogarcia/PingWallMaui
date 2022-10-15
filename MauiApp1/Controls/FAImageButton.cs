
using System;

namespace PingWall.Controls
{
    public class FAImageButton : ImageButton
    {
        public string Glyph
        {
            get => (string)GetValue(GlyphProperty); set
            {
                SetValue(GlyphProperty, value);
            }
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty); set
            {
                SetValue(ColorProperty, value);
            }
        }
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty); 
            set
            {
                SetValue(FontFamilyProperty, value);
            }
        }
        public static readonly BindableProperty GlyphProperty = BindableProperty.Create(
                nameof(Glyph),
                typeof(string),
                typeof(FAImageButton),
                string.Empty,
                propertyChanged:GlyphPropertyChanged
            );

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
                       nameof(Size),
                       typeof(double),
                       typeof(FAImageButton),
                       0.0,
                       propertyChanged: SizePropertyChanged
                   );

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
                       nameof(Color),
                       typeof(Color),
                       typeof(FAImageButton),
                       null,
                       propertyChanged:ColorPropertyChanged
                   );
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
                       nameof(FontFamily),
                       typeof(string),
                       typeof(FAImageButton),
                       string.Empty,
                       propertyChanged:FontFamilyPropertyChanged
                   );



        private static void GlyphPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FAImageButton target = (FAImageButton)bindable;
            target.Glyph = newValue.ToString();
            target.Source ??= new FontImageSource();
            ((FontImageSource)target.Source).Glyph = newValue.ToString();
        }
        private static void SizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FAImageButton target = (FAImageButton)bindable;
            target.Size = (double)newValue;
            target.Source ??= new FontImageSource();
            ((FontImageSource)target.Source).Size = target.Size;
        }
        private static void ColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FAImageButton target = (FAImageButton)bindable;
            target.Color = (Color)newValue;
            target.Source ??= new FontImageSource();
            ((FontImageSource)target.Source).Color = target.Color;
        }
        private static void FontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FAImageButton target = (FAImageButton)bindable;
            target.FontFamily = newValue.ToString();
            target.Source ??= new FontImageSource();
            ((FontImageSource)target.Source).FontFamily = newValue.ToString();
        }
    }
}
