using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace DuelTracker.Services
{
    public class DeviceDetectorService : IAsyncDisposable
    {
        private readonly IJSRuntime _js;
        private DotNetObjectReference<DeviceDetectorService>? _dotNetRef;
        private IJSObjectReference? _module;

        public event Action<bool>? OnIsMobileChanged;

        public DeviceDetectorService(IJSRuntime js)
        {
            _js = js;
        }

        public async ValueTask InitializeAsync()
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            // Import the module and register the resize callback, passing a DotNet reference
            _module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/deviceDetector.js");
            if (_module is not null)
            {
                await _module.InvokeVoidAsync("registerResizeCallback", _dotNetRef);
            }
        }

        public async ValueTask<bool> GetIsMobileAsync()
        {
            try
            {
                if (_module is null)
                {
                    _module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/deviceDetector.js");
                }

                if (_module is not null)
                {
                    return await _module.InvokeAsync<bool>("isMobile");
                }
            }
            catch { }

            return false;
        }

        [JSInvokable]
        public Task NotifyMobileChanged(bool isMobile)
        {
            OnIsMobileChanged?.Invoke(isMobile);
            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_module is not null)
                {
                    await _module.InvokeVoidAsync("unregisterResizeCallback");
                    await _module.DisposeAsync();
                }
            }
            catch { }

            _dotNetRef?.Dispose();
        }
    }
}
