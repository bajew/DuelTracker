export function isMobile() {
    try {
        const ua = navigator.userAgent || '';
        const isMobileUA = /Mobi|Android|iPhone|iPad|iPod|Phone/i.test(ua);
        const narrow = window.innerWidth <= 767;
        return isMobileUA || narrow;
    } catch (e) {
        return false;
    }
}

export function registerResizeCallback(dotNetRef) {
    if (!dotNetRef) return;
    function handler() {
        try {
            const mobile = isMobile();
            dotNetRef.invokeMethodAsync('NotifyMobileChanged', mobile).catch(() => { });
        } catch (e) { }
    }
    // store handler so it can be removed later
    window.__deviceDetectorHandler = handler;
    window.addEventListener('resize', handler);
    // call once to report initial state
    handler();
}

export function unregisterResizeCallback() {
    try {
        if (window.__deviceDetectorHandler) {
            window.removeEventListener('resize', window.__deviceDetectorHandler);
            delete window.__deviceDetectorHandler;
        }
    } catch (e) { }
}
