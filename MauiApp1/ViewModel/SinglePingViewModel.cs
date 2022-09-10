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
           
            Setup,
            Running
        }

        IPingService _pingService;
        IHostDTORepository _repo;
        IPingHistoryRepository _historyRepository;
        public int Id { get; set; }
        public SinglePingViewModel(IPingService pingService, IHostDTORepository repo, IPingHistoryRepository historyRepo, HostDTO dto)
        {
            _pingService = pingService;
            StartCommand = new Command(StartCommand_Execute);
            SetupCommand = new Command(SetupCommand_Execute);
            DeleteCommand = new Command(async ()=>await DeleteCommand_Execute());
            Status = SinglePingStatus.Setup;
            Title = "Single Ping";
            _repo = repo;
            IsVisible = true;
            _historyRepository = historyRepo;

            Hostname = dto.Hostname;
            DisplayName = dto.DisplayName;
            IntervalMiliseconds = dto.Interval_Miliseconds;
            Id = (int)dto.Id;


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
        [AlsoNotifyChangeFor(nameof(IsStatusSetup))]
        SinglePingStatus status;

        [ObservableProperty]
        Command startCommand;

        [ObservableProperty]
        bool isVisible;

        [ObservableProperty]
        Command setupCommand;

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
            if(dto.Id is null)
            {
                this.Id = await _repo.AddAsync(dto);
            }
            else
            {
                await _repo.UpdateAsync(dto);
            }
            
            await PingCycle();

        }

        
        
        private async Task DeleteCommand_Execute()
        {
            await _repo.DeleteAsync((int)this.Id);
            this.IsVisible = false;
        }

        async Task PingCycle()
        {
            while (Status.Equals(SinglePingStatus.Running)) {
                var waitTask = Task.Delay(IntervalMiliseconds);
                var result = await _pingService.Ping(Hostname);
                result.PingId = this.Id;
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
