using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1HazardScript : MonoBehaviour
{
    private Rigidbody2D body;
    public float maxside = 8.1f;
    public float maxsidevel = 9f;
    private float init_y;
    public GameObject stompparticles;
    public AudioSource stompfx;
    // Start is called before the first frame update
    void Start()
    {
        init_y = transform.position.y;
        maxside = 8.1f;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 8.2f)
        {
            transform.position = new Vector3(8.2f, transform.position.y);
            body.velocity = new Vector2(-1 * body.velocity.x, body.velocity.y);
        }
        if(transform.position.x < -8.2f)
        {
            transform.position = new Vector3(-8.2f, transform.position.y);
            body.velocity = new Vector2(-1 * body.velocity.x, body.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor")
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
        float randx = Random.Range(-1 * maxside, maxside);
        float randvel = Random.Range(-1 * maxsidevel, maxsidevel);
        transform.position = new Vector3(randx, init_y);
        body.velocity = Vector2.zero;
        body.velocity += new Vector2(randvel,0f);
    }
}
