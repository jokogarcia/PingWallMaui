namespace PingWall;

public partial class ProgressBarTest : ContentPage
{
	public ProgressBarTest()
	{
		InitializeComponent();
		BindingContext = new ViewModel.ProgressBarTestViewModel();
	}
}