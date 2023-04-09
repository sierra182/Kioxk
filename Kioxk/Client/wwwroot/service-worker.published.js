// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

self.importScripts('./service-worker-assets.js');
self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
const offlineAssetsInclude = [ /\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/ ];
const offlineAssetsExclude = [ /^service-worker\.js$/ ];

async function onInstall(event) {
    console.info('Service worker: Install');

    // Fetch and cache all matching items from the assets manifest
    const assetsRequests = self.assetsManifest.assets
        .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
        .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
        .map(asset => new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' }));
    await caches.open(cacheName).then(cache => cache.addAll(assetsRequests));

    self.skipWaiting();
}

async function onActivate(event) {
    console.info('Service worker: Activate');

    // Delete unused caches
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key))); 
}

//async function onFetch(event) {
//    let cachedResponse = null;
//    if (event.request.method === 'GET') {
//        // For all navigation requests, try to serve index.html from cache
//        // If you need some URLs to be server-rendered, edit the following check to exclude those URLs
//        const shouldServeIndexHtml = event.request.mode === 'navigate';

//        const request = shouldServeIndexHtml ? 'index.html' : event.request;
//        const cache = await caches.open(cacheName);
//        cachedResponse = await cache.match(request);
//    }

//    return cachedResponse || fetch(event.request);
//}

//async function onFetch(event) {
//    const { request } = event;

//    if (request.method === "GET") {
//        const shouldServeIndexHtml = event.request.mode === "navigate";
//        const cacheRequest = shouldServeIndexHtml ? "index.html" : request;
//        const cache = await caches.open(cacheName);

//        try {
//            const cachedResponse = await cache.match(cacheRequest);

//            if (cachedResponse) {
//                return cachedResponse;
//            } else {
//                const networkResponse = await fetch(request);

//                if (networkResponse.ok) {
//                    cache.put(cacheRequest, networkResponse.clone());
//                }

//                return networkResponse;
//            }
//        } catch (error) {
//            const networkResponse = await fetch(request);

//            if (networkResponse.ok) {
//                cache.put(cacheRequest, networkResponse.clone());
//            }

//            return networkResponse;
//        }
//    }
//async function onFetch(event) {
//    const { request } = event;

//    if (request.method === "GET") {
//        const shouldServeIndexHtml = event.request.mode === "navigate";
//        const cacheRequest = shouldServeIndexHtml ? "index.html" : request;
//        const cache = await caches.open(cacheName);

//        const cachedResponse = await cache.match(cacheRequest);

//        const fetchPromise = fetch(request)
//            .then(async (networkResponse) => {
//                if (networkResponse.ok) {
//                    cache.put(cacheRequest, networkResponse.clone());
//                }
//                return networkResponse;
//            })
//            .catch(() => cachedResponse);

//        event.waitUntil(fetchPromise);
//        return cachedResponse || fetchPromise;
//    }
//*****
//self.addEventListener('fetch', (event) => {
//    // Check if this is a navigation request
//    if (event.request.mode === 'navigate') {
//        // Open the cache
//        event.respondWith(caches.open(cacheName).then((cache) => {
//            // Go to the network first
//            return fetch(event.request.url).then((fetchedResponse) => {
//                cache.put(event.request, fetchedResponse.clone());

//                return fetchedResponse;
//            }).catch(() => {
//                // If the network is unavailable, get
//                return cache.match(event.request.url);
//            });
//        }));
//    } else {
//        return;
//    }
//});
//async function onFetch(event) {
//    if (event.request.mode === 'navigate') {
//        event.respondWith(caches.open(cacheName).then((cache) => {
//            return fetch(event.request).then((fetchedResponse) => {
//                cache.put(event.request, fetchedResponse.clone());
//                return fetchedResponse;
//            }).catch(() => {
//                return cache.match(event.request);
//            });
//        }));
//    } else {
//        return;
//    }
//}


async function onFetch(event) {
    if (event.request.method === 'GET') {
        try {
            const networkResponse = await fetch(event.request);

            // Si la requête réussit, mettez à jour le cache
            if (networkResponse.ok) {
                const cache = await caches.open(cacheName);
                cache.put(event.request, networkResponse.clone());
            }

            return networkResponse;
        } catch (error) {
            // Si la requête échoue, récupérez la ressource depuis le cache
            const cacheResponse = await caches.match(event.request);
            return cacheResponse;
        }
    }
    if (event.request.method === 'POST') {    
        let networkResponse;
        try {
            networkResponse = await fetch(event.request);
        }
        finally {
            return networkResponse;
        }
    }
}