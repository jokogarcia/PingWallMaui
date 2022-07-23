using PingWall.Controls;
using PingWall.Helpers;
using PingWall.Model;
using PingWall.Services;
using PingWall.ViewModel;

namespace PingWall;

public partial class MainPage : ContentPage
{
	IHostDTORepository _repo;
	Task loadFromDiskTask;
	public MainPage(MainPageViewModel viewModel, IHostDTORepository repo)
	{
		InitializeComponent();
		BindingContext = viewModel;
		
		MessagingCenter.Subscribe<object> (this, MessagingCenterMsssages.ADD_NEW_CARD, AddNewCard);
		_repo = repo;
        //loadFromDiskTask = LoadFromDisk();
        LoadFromDisk();
	}
	void AddNewCard(object sender = null)
	{
        MainFlexLayout.Children.Add(new PingCard(new SinglePingViewModel(new PingService(),_repo)));
    }
	
	async Task LoadFromDisk() {
        foreach (var ping in await _repo.GetAll())
        {
            var viewModel = new SinglePingViewModel(new PingService(), _repo)
            {
                DisplayName = ping.DisplayName,
                Hostname = ping.Hostname,
                IntervalMiliseconds = ping.Interval_Miliseconds,
                Id = ping.Id,
                Status = SinglePingViewModel.SinglePingStatus.Running
            };
            viewModel.StartCommand.Execute(null);
            MainFlexLayout.Children.Add(new PingCard(viewModel));
        }
        AddNewCard();

    }

	
}

