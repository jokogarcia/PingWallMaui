using PingWall.Controls;
using PingWall.Helpers;
using PingWall.Model;
using PingWall.Services;
using PingWall.ViewModel;

namespace PingWall;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel, IHostDTORepository repo)
	{
		InitializeComponent();
		BindingContext = viewModel;
		
		MessagingCenter.Subscribe<SinglePingViewModel> (this, MessagingCenterMsssages.ADD_NEW_CARD, AddNewCard);

	}

    void AddNewCard(SinglePingViewModel viewModel)
    {
        MainFlexLayout.Children.Add(new PingCard(viewModel));
    }
	
	

	
}

