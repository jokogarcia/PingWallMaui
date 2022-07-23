using CommunityToolkit.Mvvm.ComponentModel;
using PingWall.Controls;
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
        Task initTask;
        public MainPageViewModel(IHostDTORepository repo)
        {
            Title = "Ping Wall";
            _repo = repo;
            initTask = Init();
            
        }
        async Task Init()
        {
            await LoadFromDisk();
            AddBlankCard();
        }
        async Task LoadFromDisk()
        {
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
                AddNewCard(viewModel);
            }
        }
        void AddBlankCard()
        {
            var blankViewModel = new SinglePingViewModel(new PingService(), _repo)
            {

                IntervalMiliseconds = 1500,
                Id = null,
                Status = SinglePingViewModel.SinglePingStatus.Blank
            };
            AddNewCard(blankViewModel);
        }

        private void AddNewCard(SinglePingViewModel viewModel)
        {
            MessagingCenter.Send<SinglePingViewModel>(viewModel, Helpers.MessagingCenterMsssages.ADD_NEW_CARD);
        }
    }
}
