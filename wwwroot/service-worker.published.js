// Published service worker — cache-first strategy for offline-first PWA.
// Blazor build pipeline replaces this file at publish time and injects
// the asset manifest URL into the importScripts call.

const CACHE_NAME = 'nihongo-study-v2';
const OFFLINE_URL = 'offline.html';

// Populated by Blazor publish pipeline via service-worker-assets.js
self.importScripts('./service-worker-assets.js');

const assetsToCache = self.assetsManifest.assets
    .filter(a => a.assetType !== 'pdb')
    .map(a => a.url);

// ── Install: pre-cache all app assets ──────────────────────────────────────
self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(CACHE_NAME).then(cache =>
            cache.addAll([OFFLINE_URL, ...assetsToCache])
        )
    );
    self.skipWaiting();
});

// ── Activate: evict old caches, take control immediately ───────────────────
self.addEventListener('activate', event => {
    event.waitUntil(
        caches.keys().then(keys =>
            Promise.all(
                keys.filter(k => k !== CACHE_NAME).map(k => caches.delete(k))
            )
        )
    );
    self.clients.claim();
});

// ── Fetch: cache-first, fall back to network, then offline.html ───────────
self.addEventListener('fetch', event => {
    if (event.request.method !== 'GET') return;

    // Don't intercept external requests (e.g. Google Fonts)
    const url = new URL(event.request.url);
    if (url.origin !== self.location.origin) return;

    event.respondWith(
        caches.match(event.request).then(cached => {
            if (cached) return cached;

            return fetch(event.request).then(response => {
                if (response.ok) {
                    const clone = response.clone();
                    caches.open(CACHE_NAME).then(cache => cache.put(event.request, clone));
                }
                return response;
            }).catch(() => {
                if (event.request.mode === 'navigate') {
                    return caches.match(OFFLINE_URL);
                }
                return new Response('', { status: 503, statusText: 'Service Unavailable' });
            });
        })
    );
});
