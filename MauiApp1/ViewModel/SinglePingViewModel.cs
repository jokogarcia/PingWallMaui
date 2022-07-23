using CommunityToolkit.Mvvm.ComponentModel;
using PingWall.Helpers;
using PingWall.Model;
using PingWall.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWall.ViewModel
{
    public partial class SinglePingViewModel:BaseViewModel
    {
        public enum SinglePingStatus
        {
            Blank,
            Setup,
            Running
        }

        IPingService _pingService;
        IHostDTORepository _repo;
        public int? Id { get; set; }
        public SinglePingViewModel(IPingService pingService, IHostDTORepository repo, HostDTO dto=null)
        {
            _pingService = pingService;
            ClearCommand = new Command(ClearCommand_Execute);
            StartCommand = new Command(StartCommand_Execute);
            SetupCommand = new Command(SetupCommand_Execute);
            NewCommand = new Command(NewCommand_Execute);
            Status = SinglePingStatus.Blank;
            Title = "Single Ping";
            _repo = repo;

           
            if(dto is null)
            {   //Default Values
                Hostname = "google.com";
                DisplayName = "Google";
                IntervalMiliseconds = 1500;
                Id = null;
            }
            else
            {
                Hostname = dto.Hostname;
                DisplayName = dto.DisplayName;
                IntervalMiliseconds = dto.Interval_Miliseconds;
                Id = dto.Id;
            }
           
        }
       

     

        [ObservableProperty]
        long roundTripMiliseconds;

        [ObservableProperty]
        string hostname;

        [ObservableProperty]
        string displayName;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsStatusRunning))]
        [AlsoNotifyChangeFor(nameof(IsStatusBlank))]
        [AlsoNotifyChangeFor(nameof(IsStatusSetup))]
        SinglePingStatus status;

        [ObservableProperty]
        Command clearCommand;

        [ObservableProperty]
        Command startCommand;

        [ObservableProperty]
        Command setupCommand;
        [ObservableProperty]
        Command newCommand;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotErrorState))]
        bool isErrorState;

        [ObservableProperty]
        int intervalMiliseconds;

        public bool IsNotErrorState { get => !IsErrorState; }

        [ObservableProperty]
        string errorMessage;

        [ObservableProperty]
        bool flashingIndicatorIsVisible;

        public bool IsStatusRunning
        {
            get => Status == SinglePingStatus.Running;
        }
        public bool IsStatusBlank
        {
            get => Status == SinglePingStatus.Blank;
        }
        public bool IsStatusSetup
        {
            get => Status == SinglePingStatus.Setup;
        }

        private void SetupCommand_Execute(object obj)
        {
            Status = SinglePingStatus.Setup;
        }

        private async void StartCommand_Execute(object obj)
        {
            Status = SinglePingStatus.Running;
            HostDTO dto = new() { DisplayName = this.DisplayName, Hostname = this.Hostname, Id = this.Id, Interval_Miliseconds = this.IntervalMiliseconds };
            await _repo.InsertOrUpdateAsync(dto);
            await PingcCycle();

        }

        private void ClearCommand_Execute(object obj)
        {
            Status = SinglePingStatus.Blank;
        }
        private void NewCommand_Execute(object obj)
        {
            MessagingCenter.Send<object>(this, MessagingCenterMsssages.ADD_NEW_CARD);
            SetupCommand_Execute(obj);
        }

        async Task PingcCycle()
        {
            while (Status.Equals(SinglePingStatus.Running)){
                var waitTask = Task.Delay(IntervalMiliseconds);
                var result =await _pingService.Ping(Hostname);
                IsErrorState = result.IsErrorState;
                ErrorMessage = result.ErrorMessage;
                RoundTripMiliseconds = result.RoundTripMilliseconds;
                if (!IsErrorState)
                {
                    FlashIndicator();
                }

                await waitTask;

            }
        }
        async void FlashIndicator()
        {
            FlashingIndicatorIsVisible = true;
            await Task.Delay(100);
            FlashingIndicatorIsVisible = false;
        }
    }
}
