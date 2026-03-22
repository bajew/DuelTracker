using Microsoft.Extensions.Hosting;
using System;

namespace DuelTracker.Services
{
    public class AppActionsService
    {
        public event Action? ResetRequested;
        public event Action? MobileToggled;

        public void RequestReset() => ResetRequested?.Invoke();

        public void ToggleMobile() => MobileToggled?.Invoke();
    }
}
