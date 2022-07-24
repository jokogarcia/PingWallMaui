namespace PingWall.Controls;

public partial class CustomProgressBarVertical : ContentView
{
	public CustomProgressBarVertical()
	{
		InitializeComponent();
	}
	private double _progress;
	public double Progress { get=>_progress; set
		{
			_progress=value;
            MainGrid.RowDefinitions[0].Height = new GridLength(100 - _progress, GridUnitType.Star);
            MainGrid.RowDefinitions[1].Height = new GridLength(_progress, GridUnitType.Star);
        } }
	public static BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(double), typeof(CustomProgressBarVertical), 0d, propertyChanged:OnProgressPropertyChanged);
	public Color ProgressColor { get => BottomFrame.BackgroundColor; set =>BottomFrame.BackgroundColor=BottomFrame.BorderColor = value; }
    public Color RestColor { get => TopFrame.BackgroundColor; set => TopFrame.BackgroundColor = TopFrame.BorderColor = value; }
    public static BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CustomProgressBarVertical), Colors.Transparent, propertyChanged: OnProgressColorPropertyChanged);
    public static BindableProperty RestColorProperty = BindableProperty.Create(nameof(RestColor), typeof(Color), typeof(CustomProgressBarVertical), Colors.Transparent, propertyChanged: OnRestColorPropertyChanged);
    static void OnProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue == newValue)
            return;
        CustomProgressBarVertical control = bindable as CustomProgressBarVertical;

        control.Progress = (double)newValue;
    }
    static void OnProgressColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue == newValue)
            return;
        CustomProgressBarVertical control = bindable as CustomProgressBarVertical;

        control.ProgressColor = (Color)newValue;

    }
    static void OnRestColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue == newValue)
            return;
        CustomProgressBarVertical control = bindable as CustomProgressBarVertical;

        control.RestColor = (Color)newValue;

    }
}