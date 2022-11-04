using PingWall.ViewModel;
using System.Text.RegularExpressions;

namespace PingWall.Controls;

public partial class PingCard : ContentView
{
	
	public PingCard(SinglePingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

	}


}