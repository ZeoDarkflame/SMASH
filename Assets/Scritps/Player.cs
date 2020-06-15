using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject debugger;
    private TMPro.TextMeshProUGUI debugtext;
    public bool reading = true;
    public bool lost = false;
    public int rightleft;
    public float movementspeed;
    public float dashspeed;
    public float dashtime;
    private Rigidbody2D pbody;
    public float time;
    public TMPro.TextMeshProUGUI timer;
    public GameObject playerdeathparticles;
    private SpriteRenderer sprite;
    private AudioSource deathsound;
    public AudioSource bgsound;
    public ADManager ADmanager;
    public bool revived = false;
    public GameObject YouLose;
    public GameObject Revive;
    public GameObject Giveup;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
        YouLose.SetActive(false);
        Revive.SetActive(false);
        Giveup.SetActive(false);
        deathsound = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        timer.text = "Time: 0";
        time = 0;
        pbody = GetComponent<Rigidbody2D>();
        //reading = true;    initialization managed in pausemenu script
        lost = false;
        rightleft = 0;
        debugtext = debugger.GetComponent<TextMeshProUGUI>();
        debugtext.text = "Game Working Yes";
        revived = false;
    }

    // Update is called once per frame
    void Update()
    {
        time = (int)Time.timeSinceLevelLoad;
        timer.text = "Time: " + time.ToString("0");
        if (transform.position.x > 8.2)
        {
            transform.position = new Vector3(8.2f, transform.position.y);
            pbody.velocity = new Vector2(-1 * pbody.velocity.x, 0);
        }
        if (transform.position.x < -8.2)
        {
            transform.position = new Vector3(-8.2f, transform.position.y);
            pbody.velocity = new Vector2(-1 * pbody.velocity.x, 0);
        }
        if (reading)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(Input.touchCount - 1);
                Vector2 touchposition = Camera.main.ScreenToWorldPoint(touch.position);
                if (touchposition.x > 0 && touchposition.y < 0)
                {
                    rightleft = 1;
                }
                else if (touchposition.x < 0 && touchposition.y < 0)
                {
                    rightleft = -1;
                }
                else if (touchposition.y > 0)
                {
                    if (touchposition.x > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended)
                    {
                        dashright();
                        debugtext.text = "dashing right";
                    }
                    else if (touchposition.x < 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended)
                    {
                        dashleft();
                        debugtext.text = "dashing left";
                    }
                }
            }
            else
            {
                rightleft = 0;
            }
            transform.position += new Vector3(rightleft * movementspeed * Time.deltaTime, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hazard" && !lost)
        {
            lost = true;
            if (PlayerPrefs.GetInt("Audio", 1) == 1)
            {
                bgsound.Pause();
                deathsound.Play();
            }
            reading = false;
            sprite.color = new Color(0.7f, 0.115f, 0.115f);
            playerdeathparticles.SetActive(true);
            Time.timeScale = 0.25f;
            if (revived == false)
            {
                Invoke("GiveLastChance", 0.5f);
            }
            else
            {
                Invoke("GameOver", 0.5f);
            }
        }
    }

    private void dashleft()
    {
        transform.localScale = new Vector3(0.5f, 0.7f, 0.5f);
        transform.rotation = Quaternion.Euler(0, 0, 45);
        reading = false;
        pbody.velocity += new Vector2(-1 * dashspeed, 0);
        Invoke("reset", dashtime);
    }

    private void dashright()
    {
        transform.localScale = new Vector3(0.5f, 0.7f, 0.5f);
        transform.rotation = Quaternion.Euler(0, 0, -45);
        reading = false;
        pbody.velocity += new Vector2(dashspeed, 0);
        Invoke("reset", dashtime);
    }

    private void reset()
    {
        transform.localScale = new Vector3(0.7f, 0.7f, 0.5f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        pbody.velocity = Vector2.zero;
        reading = true;
    }

    public void GameOver()
    {
        ADmanager.RunAdOnGameOver();
        SceneManager.LoadScene("MainMenu");
    }

    private void GiveLastChance()
    {
        if (revived == false)
        {
            Time.timeScale = 0f;
            YouLose.SetActive(true);
            Revive.SetActive(true);
            Giveup.SetActive(true);
        } else
        {
            GameOver();
        }
    }

    public void Reviveplayer()
    {
        lost = false;
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(invincibility());
        Time.timeScale = 1f;
        sprite.color = new Color(1, 1, 1,0.3f);
        playerdeathparticles.SetActive(false);
        revived = true;
        YouLose.SetActive(false);
        Revive.SetActive(false);
        Giveup.SetActive(false);
        reading = true;
        if (PlayerPrefs.GetInt("Audio", 1) == 1)
        {
            bgsound.Play();
        }
        reset();
    }

    IEnumerator invincibility()
    {
        yield return new WaitForSecondsRealtime(3f);
        GetComponent<CircleCollider2D>().enabled = true;
        sprite.color = new Color(1, 1, 1, 1);
    }
}
