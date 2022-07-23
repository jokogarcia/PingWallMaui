using PingWall.ViewModel;

namespace PingWall;

public partial class SinglePing : ContentPage
{
	public SinglePing(SinglePingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}