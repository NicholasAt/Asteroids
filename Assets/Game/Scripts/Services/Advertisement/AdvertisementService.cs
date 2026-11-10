using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Advertisement
{
    public class AdvertisementService : IAdvertisementService, IDisposable
    {
        private const string AndroidId = "AD_UNIT_ID";
        private bool _inited;
        private bool _bannerRequested;
        private BannerView _bannerView;

        public async UniTask Init()
        {
            try
            {
                MobileAds.Initialize((initStatus) =>
                {
                    _inited = true;
                    Debug.Log("Advertisement initialized.");
                });
                await UniTask.WaitUntil(() => _inited).Timeout(TimeSpan.FromSeconds(10));

                if (_bannerRequested)
                    ShowBanner();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void ShowBanner()
        {
            if (_inited == false)
            {
                _bannerRequested = true;
                return;
            }

            if (_bannerView != null)
                return;

            _bannerView = new BannerView(AndroidId, AdSize.Banner, AdPosition.Top);
            _bannerView.OnAdClicked += OnClickedBanner;
            _bannerView.LoadAd(new AdRequest());
        }

        public void HideBanner()
        {
            _bannerRequested = false;

            if (_bannerView == null)
                return;

            _bannerView.OnAdClicked -= OnClickedBanner;
            _bannerView.Destroy();
            _bannerView = null;
        }

        public void Dispose()
        {
            HideBanner();
        }

        private void OnClickedBanner()
        {
            Debug.Log("clicked on the banner");
        }
    }
}