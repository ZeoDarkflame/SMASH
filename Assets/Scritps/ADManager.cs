using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class ADManager : MonoBehaviour, IUnityAdsListener
{
    string GooglePlay_ID = "3651951";
    bool testmode = false;
    string rewardplacementid = "rewardedVideo";
    public Player playerscript;
    string bannerplacementid = "banner";
    // Start is called before the first frame update
    void Start()
    {
        playerscript = GameObject.Find("Player").GetComponent<Player>();
        Advertisement.AddListener(this);
        Advertisement.Initialize(GooglePlay_ID, testmode);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerscript)
        {

        } else
        {
            Debug.Log("Destoyed");
        }
    }

    public void RunAdOnGameOver()
    {
        if (Random.Range(1, 11) < 4)
        {
            Advertisement.Show();
        }
    }

    public void RunRewardedAd()
    {
        Advertisement.Show(rewardplacementid);
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("ErrorPlayingAD");
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished && placementId == rewardplacementid)
        {
            try //this somehow works
            {
                pleadforrevive();
            } catch
            {
                print("Met With an Error");
            }
        }
    }

    private void pleadforrevive()
    {
        playerscript.Reviveplayer();
    }

    public void showbannerfunc()
    {
        StartCoroutine(Showbanner());
    }

    public void hidebannerfunc()
    {
        Advertisement.Banner.Hide();
    }
     
    IEnumerator Showbanner()
    {
        while (!Advertisement.IsReady(bannerplacementid))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerplacementid);
    }
}
