using System;
using System.Collections;
using System.Collections.Generic;
using CAS;
using UnityEngine;

public class CASAds : MonoBehaviour
{
    public static CASAds instance = null;

    private static IMediationManager _manager = null;
    private static IAdView _lastAdView = null;
    private static IAdView _lastMrecAdView = null;
    private static Action _lastAction = null;

    private void Awake()
    {
        if ( instance == null )
            instance = this;
        
        DontDestroyOnLoad( this );
    }

    private void Start()
    {
        CAS.MobileAds.settings.isExecuteEventsOnUnityThread = true;

        Init();
    }

    private void Init()
    {
        _manager = MobileAds.BuildManager()
            .WithInitListener(CreateAdView)
            // Call Initialize method in any case to get IMediationManager instance
            .Initialize();

        _manager.OnRewardedAdCompleted += _lastAction;

    }

    private void CreateAdView(bool success, string error)
    {
        _lastAdView = _manager.GetAdView(AdSize.Banner);
        _lastMrecAdView = _manager.GetAdView(AdSize.MediumRectangle);
        _lastAdView.SetActive(false);
        _lastMrecAdView.SetActive(false);
    }
 
    public void ShowBanner(AdPosition position)
    {
        if (_lastAdView == null)
        {
            CreateAdView(true, ""); 
        }

        if (_lastAdView != null)
        {
            _lastAdView.position = position;
            _lastAdView.SetActive(true);
        }
    }

    public void ShowMrecBanner(AdPosition position)
    {
        if (_lastMrecAdView == null)
        {
            CreateAdView(true, "");
        }

        if (_lastMrecAdView != null)
        {
            _lastMrecAdView.position = position;
            _lastMrecAdView.SetActive(true);
        }
    }

    public void HideBanner()
    {
        if ( _lastAdView != null )
        {
            _lastAdView.SetActive( false );
        }
    }

    public void HideMrecBanner()
    {
        if (_lastMrecAdView != null)
        {
            _lastMrecAdView.SetActive(false);
        }
    }

    public void ShowInterstitial()
    {
        _manager?.ShowAd( AdType.Interstitial );
    }

    public void ShowRewarded( Action complete )
    {
        if ( _manager == null )
            return;
        
        if ( _lastAction != null)
        {
            _manager.OnRewardedAdCompleted -= _lastAction;
        }

        _lastAction = complete;
        _manager.OnRewardedAdCompleted += _lastAction;
        _manager?.ShowAd(AdType.Rewarded);
    }
}
