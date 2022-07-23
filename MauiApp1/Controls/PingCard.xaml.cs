using PingWall.ViewModel;

namespace PingWall.Controls;

public partial class PingCard : ContentView
{
	
	public PingCard(SinglePingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}


}