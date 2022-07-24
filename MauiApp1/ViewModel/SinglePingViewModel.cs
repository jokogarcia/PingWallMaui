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
        IPingHistoryRepository _historyRepository;
        public int? Id { get; set; }
        public SinglePingViewModel(IPingService pingService, IHostDTORepository repo, IPingHistoryRepository historyRepo, HostDTO dto=null)
        {
            _pingService = pingService;
            ClearCommand = new Command(ClearCommand_Execute);
            StartCommand = new Command(StartCommand_Execute);
            SetupCommand = new Command(SetupCommand_Execute);
            NewCommand = new Command(NewCommand_Execute);
            DeleteCommand = new Command(async ()=>await DeleteCommand_Execute());
            Status = SinglePingStatus.Blank;
            Title = "Single Ping";
            _repo = repo;
            IsVisible = true;
            _historyRepository = historyRepo;
           
            if(dto is null)
            {   //Default Values
                Hostname = "";
                DisplayName = "";
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
        double successRate;

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
        bool isVisible;

        [ObservableProperty]
        Command setupCommand;
        [ObservableProperty]
        Command newCommand;

        [ObservableProperty]
        Command deleteCommand;

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
            int dummy = 0;
            MessagingCenter.Send<object>(dummy, MessagingCenterMsssages.ADD_NEW_BLANK_CARD);
            SetupCommand_Execute(obj);
        }
        private async Task DeleteCommand_Execute()
        {
            if(this.Id is not null)
            {
                await _repo.DeleteAsync((int)this.Id);
                this.IsVisible = false;
            }
        }

        async Task PingcCycle()
        {
            while (Status.Equals(SinglePingStatus.Running)) {
                var waitTask = Task.Delay(IntervalMiliseconds);
                var result = await _pingService.Ping(Hostname);
                var t1 = _historyRepository.AddAsync(result);
                IsErrorState = result.IsErrorState;
                ErrorMessage = result.ErrorMessage;
                RoundTripMiliseconds = result.RoundTripMilliseconds;
                if (!IsErrorState)
                {
                    FlashIndicator();
                }
                await t1;
                SuccessRate = await _historyRepository.GetSuccessRate((int)this.Id, DateTime.UtcNow - TimeSpan.FromMinutes(60), DateTime.UtcNow);
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
