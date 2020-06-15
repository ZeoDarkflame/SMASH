using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHazardScript : MonoBehaviour
{
    private Rigidbody2D body;
    public float maxside = 8.1f;
    public float maxsidevel = 9f;
    private float init_y;
    public GameObject stompparticles;
    public AudioSource stompfx;
    public AudioSource swooshfx;
    public bool cansuperfall = false;
    public int superfallprob = 3;
    public float superfallspeed = 18;
    public Player playerscript;
    public float followspeed = 3;
    public GameObject player;
    public GameObject pupil;
    public float pupil_offset = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        pupil.transform.localPosition = new Vector3(0f, -0.1f, -6f);
        player = GameObject.Find("Player");
        init_y = transform.position.y;
        maxside = 8.1f;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 8.2f)
        {
            transform.position = new Vector3(8.2f, transform.position.y);
            body.velocity = new Vector2(-1 * body.velocity.x, body.velocity.y);
        }
        if (transform.position.x < -8.2f)
        {
            transform.position = new Vector3(-8.2f, transform.position.y);
            body.velocity = new Vector2(-1 * body.velocity.x, body.velocity.y);
        }
        try
        {
            body.velocity = new Vector2(playerscript.rightleft * followspeed, body.velocity.y);
            //transform.position += new Vector3(playerscript.rightleft * followspeed,0f);
            pupil.transform.localPosition = new Vector3(playerscript.rightleft * pupil_offset, -0.1f, -6f);
        } catch
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            try
            {
                stompfx.PlayOneShot(stompfx.clip);
            } catch { }
            var obj = Instantiate(stompparticles, this.transform.position, Quaternion.identity);
            //Debug.Log("Respawning");
            respawn();
            Destroy(obj, 1);
        }
    }

    private void respawn()
    {
        //float randvel = Random.Range(-1 * maxsidevel, maxsidevel);
        try
        {
            transform.position = new Vector3(player.transform.position.x, init_y);
        } catch
        {
            transform.position = new Vector3(Random.Range(-1 * maxside, maxside), init_y);
        }
        if (cansuperfall && Random.Range(1, 11) > (10 - superfallprob))
        {
            try
            {
                swooshfx.PlayOneShot(swooshfx.clip);
            } catch { }
            body.velocity = new Vector2(0f, -1 * superfallspeed);
        }
        else
        {
            body.velocity = new Vector2(0f, 0f);
        }
    }
}
