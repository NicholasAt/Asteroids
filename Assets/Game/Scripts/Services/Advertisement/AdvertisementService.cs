using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace Assets.Scripts.Services.Advertisement
{
    public class AdvertisementService : IAdvertisementService
    {
        private const string AndroidId = "AD_UNIT_ID";
        private bool _inited;
        private BannerView _bannerView;

        public async UniTask Init()
        {
            MobileAds.Initialize((initStatus) =>
            {
                _inited = true;
                Debug.Log("Advertisement initialized.");
            });
            await UniTask.WaitUntil(() => _inited).Timeout(TimeSpan.FromSeconds(10));
        }

        public void ShowBanner()
        {
            if (_bannerView != null)
                return;

            _bannerView = new BannerView(AndroidId, AdSize.Banner, AdPosition.Top);
            _bannerView.OnAdClicked += OnClickedBanner;
            _bannerView.LoadAd(new AdRequest());
        }

        private void OnClickedBanner()
        {
            Debug.Log("clicked on the banner");
        }
    }
}