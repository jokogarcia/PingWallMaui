using MauiApp1.Controls;
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
        MainFlexLayout.Children.Add(new PingCard_Empty());

    }

    void AddNewCard(SinglePingViewModel viewModel)
    {
		var newIndex = MainFlexLayout.Children.Count > 0 ? MainFlexLayout.Children.Count - 1 : 0;
        MainFlexLayout.Children.Insert(
			newIndex 
			,new PingCard(viewModel));

    }

	

	
}

