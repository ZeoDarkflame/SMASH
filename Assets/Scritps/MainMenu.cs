using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI bestscore;
    public GameObject playbtn;
    public GameObject catalogbtn;
    public GameObject besttext;
    public GameObject credits;
    public GameObject title;
    public GameObject infopanel;
    public GameObject backbtn;
    public GameObject simplebtn;
    public GameObject nssimplebtn;
    public GameObject nssimple_lockedtext;
    public GameObject followbtn;
    public GameObject follow_lockedtext;
    private int best;
    public GameObject simplehazard;
    public GameObject nssimplehazard;
    public GameObject followhazard;
    public bool nssimple_locked;
    public bool follow_locked;
    public TMPro.TextMeshProUGUI infotext;
    public string simpleinfo;
    public string nssimpleinfo;
    public string followinfo;
    public string nssimpleinfo_locked;
    public string followinfo_locked;
    public bool oncatalog = false;
    public GameObject setaudiooff;
    public GameObject setaudioon;
    public AudioSource bgsound;
    public GameObject stompfx;
    public GameObject swooshfx;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Audio",1) == 1)
        {
            bgsound.Play();
            stompfx.SetActive(true);
            swooshfx.SetActive(true);
            setaudiooff.SetActive(true);
            setaudioon.SetActive(false);
        } else
        {
            stompfx.SetActive(false);
            swooshfx.SetActive(false);
            setaudiooff.SetActive(false);
            setaudioon.SetActive(true);
        }
        oncatalog = false;
        infotext.text = " ";
        backbtn.SetActive(false);
        simplebtn.SetActive(false);
        nssimplebtn.SetActive(false);
        followbtn.SetActive(false);
        nssimple_locked = true;
        follow_locked = true;
        infopanel.SetActive(false);
        Time.timeScale = 1;
        nssimplehazard.SetActive(false);
        followhazard.SetActive(false);
        best = PlayerPrefs.GetInt("Best",1);
        if(best >= 3)
        {
            nssimplehazard.SetActive(true);
            nssimple_locked = false;
        }
        if(best >= 5)
        {
            followhazard.SetActive(true);
            follow_locked = false;
        }
        if(best >= 7)
        {
            Instantiate(simplehazard, new Vector3(0,9,simplehazard.transform.position.z), Quaternion.identity);
        }
        bestscore.text = "Best: " + PlayerPrefs.GetInt("Best", 0).ToString();
        simpleinfo = "Spikey\nA standard Hazard, falling randomly to hit the Player";
        nssimpleinfo_locked = "Saw\nSomewhat Intelligent, known to randomly fall at super speed to catch the player off-guard\nReach Level 3 For More Info";
        followinfo_locked = "EvilEye\nThe Power of the Red Pupil allows the Eye to follow the player, may or may not superfall\nReach Level 5 For More Info";
        nssimpleinfo = "Saw\nSomewhat Intelligent, known to randomly fall at super speed to catch the player off-guard\nKeep Moving To avoid Superfall";
        followinfo = "EvilEye\nThe Power of the Red Pupil allows the Eye to follow the player, may or may not superfall\nDashing May Help Avoid It";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (oncatalog)
            {
                onbackbtn();
            }
            else
            {
                Debug.Log("QuittingGame");
                Application.Quit();
            }
        }
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onCatalog()
    {
        infotext.text = "";
        playbtn.SetActive(false);
        catalogbtn.SetActive(false);
        besttext.SetActive(false);
        credits.SetActive(false);
        title.SetActive(false);
        infopanel.SetActive(true);
        simplebtn.SetActive(true);
        nssimplebtn.SetActive(true);
        followbtn.SetActive(true);
        backbtn.SetActive(true);
        oncatalog = true;
        if (nssimple_locked)
        {
            nssimple_lockedtext.SetActive(true);
        } else
        {
            nssimple_lockedtext.SetActive(false);
        }
        if (follow_locked)
        {
            follow_lockedtext.SetActive(true);
        } else
        {
            follow_lockedtext.SetActive(false);
        }
    }

    public void onsimplebtn()
    {
        simplehazard.transform.localScale = new Vector3(2,2,1);
        nssimplehazard.transform.localScale = new Vector3(0.36f, 0.36f, 1);
        followhazard.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        infotext.text = simpleinfo;
    }

    public void onnssimplebtn()
    {
        simplehazard.transform.localScale = new Vector3(1, 1, 1);
        nssimplehazard.transform.localScale = new Vector3(0.36f * 2, 0.36f * 2, 1);
        followhazard.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        if (nssimple_locked)
        {
            infotext.text = nssimpleinfo_locked;
        } else
        {
            infotext.text = nssimpleinfo;
        }
    }

    public void onfollowbtn()
    {
        simplehazard.transform.localScale = new Vector3(1, 1, 1);
        nssimplehazard.transform.localScale = new Vector3(0.36f, 0.36f, 1);
        followhazard.transform.localScale = new Vector3(1.5f * 2, 1.5f * 2, 1);
        if (follow_locked)
        {
            infotext.text = followinfo_locked;
        } else
        {
            infotext.text = followinfo;
        }
    }

    public void onbackbtn()
    {
        playbtn.SetActive(true);
        catalogbtn.SetActive(true);
        besttext.SetActive(true);
        credits.SetActive(true);
        title.SetActive(true);
        infopanel.SetActive(false);
        simplebtn.SetActive(false);
        nssimplebtn.SetActive(false);
        followbtn.SetActive(false);
        backbtn.SetActive(false);
        nssimple_lockedtext.SetActive(false);
        follow_lockedtext.SetActive(false);
        simplehazard.transform.localScale = new Vector3(1, 1, 1);
        nssimplehazard.transform.localScale = new Vector3(0.36f, 0.36f, 1);
        followhazard.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        oncatalog = false;
    }

    public void OnSetaudiooff()
    {
        bgsound.Stop();
        stompfx.SetActive(false);
        swooshfx.SetActive(false);
        PlayerPrefs.SetInt("Audio", 0);
        setaudiooff.SetActive(false);
        setaudioon.SetActive(true);
    }

    public void OnSetaudioon()
    {
        bgsound.Play();
        stompfx.SetActive(true);
        swooshfx.SetActive(true);
        PlayerPrefs.SetInt("Audio", 1);
        setaudioon.SetActive(false);
        setaudiooff.SetActive(true);
    }
}
