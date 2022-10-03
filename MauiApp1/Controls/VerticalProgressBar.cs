using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.Controls
{
    internal class VerticalProgressBar : VerticalStackLayout
    {
        public BoxView ProgressView { get; set; }
        public double Progress { 
            get => (double)GetValue(ProgressProperty);
            set
            {
                SetValue(ProgressProperty, value);
            }
        }
        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
                nameof(Progress),
                typeof(double),
                typeof(VerticalProgressBar),
                1d,
                propertyChanged:OnProgressPropertyChanged
            );
        public Color ProgressColor { 
            get => (Color)GetValue(ProgressColorProperty); 
            set {
                this.BackgroundColor = value; 
                SetValue(ProgressColorProperty, value);
            } 
        }
        public Color RestColor { get => (Color)GetValue(RestColorProperty); set { ProgressView.BackgroundColor = value; SetValue(RestColorProperty, value); } }
        public static BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CustomProgressBarVertical), Colors.Transparent);
        public static BindableProperty RestColorProperty = BindableProperty.Create(nameof(RestColor), typeof(Color), typeof(CustomProgressBarVertical), Colors.Transparent);
        private static void OnProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            VerticalProgressBar vpb = bindable as VerticalProgressBar;
            vpb.UpdateProgressBarSize();
        }

        public VerticalProgressBar()
        {
            this.BackgroundColor = ProgressColor;
            ProgressView = new()
            {
                CornerRadius = 0,
                Margin = this.Margin,
                HeightRequest = this.HeightRequest,
                WidthRequest = this.WidthRequest,
                HorizontalOptions=LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End,
                Color=this.RestColor

            };
            this.Add(ProgressView);
           
            this.SizeChanged+=OnSizeChanged;
        }
        void UpdateProgressBarSize()
        {
            var progress = Progress > 1 ?  0: 1-Progress;
            ProgressView.HeightRequest = this.Height * progress;
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            UpdateProgressBarSize();
            this.BackgroundColor = ProgressColor;
            ProgressView.Color = RestColor;
        }
        ~VerticalProgressBar()
        {
            this.SizeChanged -= OnSizeChanged;
        }
    }
}
