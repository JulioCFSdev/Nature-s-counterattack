using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public float speed;
    public float bulletrange;
    public float timeattack;
    private float nextattack;
    private float cdanim;

    public Transform rightCol;
    public Transform leftCol;
    public Transform bulletposition;
    public GameObject bulletproject;

    public bool isDrone;

    private bool colliding;

    public LayerMask layer;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if(colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }

        Shoot();
        Reset();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject, 0.8f);
            GameController.instance.ShowGameOver();
        }

        if(collision.gameObject.CompareTag("Bullet"))
        {
            if(isDrone)
            {
                anim.SetTrigger("dead");
            }
        }
    }

    void Reset()
    {
        if(isDrone)
        {
            if(Time.time == cdanim)
            {
                anim.SetBool("attack", false);
            }
        }
    }

    void Shoot()
    {
        if(isDrone)
        {
            if(Time.time > nextattack)
            {
                anim.SetTrigger("attack");
                nextattack = Time.time + bulletrange;
                GameObject temp = Instantiate(bulletproject);
                temp.transform.position = bulletposition.position;
            }
        }
        
    }
}
