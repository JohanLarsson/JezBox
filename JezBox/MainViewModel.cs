using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;

namespace JezBox
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IAssetSyncServiceClient service;
        private bool isAlive;

        public MainViewModel(IAssetSyncServiceClient service)
        {
            this.service = service;
            PingCommand = new RelayCommand(_ => Ping(), _ => true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand PingCommand { get; }

        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                if (value == isAlive) return;
                isAlive = value;
                OnPropertyChanged();
            }
        }

        private async void Ping()
        {
            IsAlive = await this.service.PingServiceAsync().ConfigureAwait(false);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}