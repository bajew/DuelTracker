using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DuelTracker.Services
{
    public class AppState : IAsyncDisposable, INotifyPropertyChanged
    {
        private readonly DeviceDetectorService _deviceDetector;
        private bool isMobile;

        public bool IsMobile { get => isMobile; set 
            {
                isMobile = value;
                OnPropertyChanged();
            }
        }

        public event Action<bool>? OnMobileChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public AppState(DeviceDetectorService deviceDetector)
        {
            _deviceDetector = deviceDetector;
            _deviceDetector.OnIsMobileChanged += HandleMobileChanged;
        }

        private void HandleMobileChanged(bool value)
        {
            IsMobile = value;
            OnMobileChanged?.Invoke(value);
        }

        public async Task InitializeAsync()
        {
            await _deviceDetector.InitializeAsync();
            IsMobile = await _deviceDetector.GetIsMobileAsync();
            OnMobileChanged?.Invoke(IsMobile);
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        public async ValueTask DisposeAsync()
        {
            _deviceDetector.OnIsMobileChanged -= HandleMobileChanged;
            await _deviceDetector.DisposeAsync();
        }
    }
}
