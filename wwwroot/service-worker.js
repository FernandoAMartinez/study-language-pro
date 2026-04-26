// Development service worker — passthrough, no caching.
// Prevents interference with Blazor hot reload in development.
self.addEventListener('fetch', () => { });
