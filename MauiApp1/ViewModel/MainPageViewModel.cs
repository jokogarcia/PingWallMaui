using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using PingWall.Controls;
using PingWall.Helpers;
using PingWall.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        IHostDTORepository _repo;
        IPingHistoryRepository _historyRepo;
        Task initTask;
        IFullScreenService _fullScreenService;
        IServiceProvider _serviceProvider;
        [ObservableProperty]
        Command toggleFullScreenCommand;
        bool _isFullScreen = false;
        public MainPageViewModel(IHostDTORepository repo, IPingHistoryRepository historyRepo, IServiceProvider serviceProvider, IFullScreenService fullScreenService)
        {
            Title = "Ping Wall";
            _repo = repo;
            _historyRepo = historyRepo;
            initTask = Init();
            _fullScreenService = fullScreenService;
            _serviceProvider = serviceProvider;
            MessagingCenter.Subscribe<object>(this, MessagingCenterMsssages.ADD_NEW_BLANK_CARD, async (_) => await AddNewBlankCard());
            ToggleFullScreenCommand = new Command(ToggleFullScreenCommand_Execute);
        }

        async Task Init()
        {
            await LoadFromDisk();
            
        }
        async Task LoadFromDisk()
        {
            foreach (var ping in await _repo.GetAll())
            {
                bool isBlank = string.IsNullOrEmpty(ping.Hostname);
                var viewModel = new SinglePingViewModel(_serviceProvider.GetService<IPingService>(), _repo, _historyRepo, ping)
                {
                    Status = isBlank ? SinglePingViewModel.SinglePingStatus.Setup : SinglePingViewModel.SinglePingStatus.Running
                };
                
                if(!isBlank) viewModel.StartCommand.Execute(null);
                AddNewCard(viewModel);
            }
        }
        

        private void AddNewCard(SinglePingViewModel viewModel)
        {
            MessagingCenter.Send<SinglePingViewModel>(viewModel, Helpers.MessagingCenterMsssages.ADD_NEW_CARD);
        }
        private async Task AddNewBlankCard()
        {
            var pingId = await _repo.AddAsync(new Model.HostDTO { Interval_Miliseconds = 1500 });
            var ping = await _repo.GetAsync(pingId);
            var viewModel = new SinglePingViewModel(_serviceProvider.GetService<IPingService>(), _repo, _historyRepo, ping)
            {
                Status = SinglePingViewModel.SinglePingStatus.Setup
            };
            AddNewCard(viewModel);
        }
        private void ToggleFullScreenCommand_Execute(object obj)
        {
            _isFullScreen=!_isFullScreen;
            if (_isFullScreen)
            {
                _fullScreenService.EnterFullScreenMode();
            }
            else
            {
                _fullScreenService.ExitFullScreenMode();
            }
        }

    }
}
