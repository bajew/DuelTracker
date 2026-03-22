using System;

namespace DuelTracker.Services
{
    public class AppActionsService
    {
        public event Action? ResetRequested;

        public void RequestReset() => ResetRequested?.Invoke();
    }
}
