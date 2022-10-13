
using System;

namespace PingWall.Controls
{
    public class FAImageButton : ImageButton
    {
       
        private FontImageSource fis = new();

        public string Glyph
        {
            get => (string)GetValue(GlyphProperty); set
            {
                SetValue(GlyphProperty, value);
                PropertiesUpdated();
            }
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty); set
            {
                SetValue(ColorProperty, value);
                PropertiesUpdated();
            }
        }
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty); set
            {
                SetValue(FontFamilyProperty, value);
                PropertiesUpdated();
            }
        }
        public static readonly BindableProperty GlyphProperty = BindableProperty.Create(
                nameof(Glyph),
                typeof(string),
                typeof(FAImageButton),
                string.Empty
            );

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
                       nameof(Color),
                       typeof(Color),
                       typeof(FAImageButton),
                       Colors.Transparent
                   );
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
                       nameof(FontFamily),
                       typeof(string),
                       typeof(FAImageButton),
                       "FAS"
                   );


        private void PropertiesUpdated()
        {
            fis.Glyph = Glyph;
            fis.Color = Color;
            fis.FontFamily = FontFamily;
            this.Source = fis;
        }
    }
}
