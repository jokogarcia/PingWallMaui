using PingWall.Controls;
using PingWall.Helpers;
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
        MessagingCenter.Subscribe<object>(this, MessagingCenterMsssages.HELP, OpenHelp);

        MainFlexLayout.Children.Add(new PingCard_Empty());

    }

	private async void OpenHelp(object obj)
	{
		await Navigation.PushAsync(new HelpPage());
	}

	void AddNewCard(SinglePingViewModel viewModel)
    {
		var newIndex = MainFlexLayout.Children.Count > 0 ? MainFlexLayout.Children.Count - 1 : 0;
        MainFlexLayout.Children.Insert(
			newIndex 
			,new PingCard(viewModel));

    }

	

	
}

