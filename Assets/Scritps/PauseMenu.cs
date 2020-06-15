using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    public GameObject resumebtn;
    public GameObject Startbtn;
    public GameObject instruct1;
    public GameObject instruct2;
    public GameObject instruct3;
    public GameObject instruct4;
    public Player playerscript;
    public int level;
    public TMPro.TextMeshProUGUI levelbartext;
    public GameObject levelbar;
    public int levelduration;
    public Animator levelupanim;
    public TMPro.TextMeshProUGUI besttext;
    public GameObject panel;
    public AudioSource bgsound;
    public bool started;
    public ADManager admanager;
    public GameObject stompfx;
    public GameObject swooshfx;
    public GameObject bgsoundsource;
    private int levelset1;
    private int levelset2;
    // Start is called before the first frame update
    void Start()
    {
        levelset1 = 1;
        levelset2 = 0;
        if (PlayerPrefs.GetInt("Audio", 1) == 1)
        {
            bgsound.volume = 0;
            bgsound.Play();
            StartCoroutine(SlowStartBgSound());
            stompfx.SetActive(true);
            swooshfx.SetActive(true);
            bgsoundsource.SetActive(true);
        } else
        {
            stompfx.SetActive(false);
            swooshfx.SetActive(false);
            bgsoundsource.SetActive(false);
        }
        started = false;
        playerscript.reading = false;
        panel.SetActive(false);
        level = 1;  //change initial level for testing
        levelbartext = levelbar.GetComponent<TextMeshProUGUI>();
        playerscript = GameObject.Find("Player").GetComponent<Player>();
        Time.timeScale = 0f;
        Startbtn.SetActive(true);
        instruct1.SetActive(true);
        instruct2.SetActive(true);
        instruct3.SetActive(true);
        instruct4.SetActive(true);
        resumebtn.SetActive(false);
        paused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        levelbartext.text = "Level: " + level.ToString();
        if (playerscript.time - 12 * levelset1 - 20 * levelset2 > 0)
        {
            levelupanim.SetTrigger("LevelUp");
            if(level < 5)
            {
                levelset1++;
            } else
            {
                levelset2++;
            }
            level++;
            if (level == 6)
            {
                bgsound.Stop();
                bgsound = GameObject.Find("BGSound2").GetComponent<AudioSource>();
                bgsound.volume = 0;
                playerscript.bgsound = bgsound;
                bgsound.Play();
                StartCoroutine(SlowStartBgSound());
            }
            if(level > PlayerPrefs.GetInt("Best", 1))
            {
                PlayerPrefs.SetInt("Best", level);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (started)
            {
                if (paused)
                {
                    paused = false;
                    resume();
                }
                else
                {
                    paused = true;
                    pause();
                }
            } else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void pause()
    {
        admanager.showbannerfunc();
        bgsound.Pause();
        Time.timeScale = 0f;
        panel.SetActive(true);
        resumebtn.SetActive(true);
        besttext.text = "Best: " + PlayerPrefs.GetInt("Best", 0).ToString();
        //paused = true;
    }

    public void resume()
    {
        admanager.hidebannerfunc();
        bgsound.Play();
        resumebtn.SetActive(false);
        panel.SetActive(false);
        Time.timeScale = 1f;
        //paused = false;
    }

    public void OnStart()
    {
        started = true;
        playerscript.reading = true;
        Startbtn.SetActive(false);
        instruct1.SetActive(false);
        instruct2.SetActive(false);
        instruct3.SetActive(false);
        instruct4.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExitbtn()
    {
        admanager.hidebannerfunc();
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator SlowStartBgSound()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (bgsound.volume == 1)
        {
            StopCoroutine(SlowStartBgSound());
        }
        else
        {
            bgsound.volume += 0.1f;
            StartCoroutine(SlowStartBgSound());
        }
    }
}
