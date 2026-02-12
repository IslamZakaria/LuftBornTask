/* Favicon Override for LeptonX Lite Theme */
(function () {
    var link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    link.type = 'image/svg+xml';
    link.rel = 'icon';
    link.href = '/images/logo/collapse-icon.svg';
    document.getElementsByTagName('head')[0].appendChild(link);
})();
