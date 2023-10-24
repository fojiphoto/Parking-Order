using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRECAds : MonoBehaviour
{
    private void OnEnable()
    {
        //AdsManager.instance.ShowMRec();
        CASAds.instance.ShowMrecBanner(CAS.AdPosition.TopCenter);
    }
    private void OnDisable()
    {
        //AdsManager.instance.HideMRec();
        CASAds.instance.HideMrecBanner();
    }
}
