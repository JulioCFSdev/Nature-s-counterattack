using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletproject;
    public Transform bulletposition;
    private bool shoot;
    public float bulletforce;
    public float bulletrange;
    private float nextattack;
    public float life;
    private Transform positionshoot;

    public bool isFinalBoss;
    public bool up;
    public bool center;
    public bool down;
    public bool nextdown;

    public Transform bulletpos2;
    public Transform bulletpos3;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Reset();
    }


    void Reset()
    {
        if(Time.time > nextattack)
        {
            anim.SetBool("attack", false);
        }
    }


    void Shoot()
    {
        if(!isFinalBoss)
        {
            if(Time.time > nextattack)
        {
            anim.SetTrigger("attack");
            nextattack = Time.time + bulletrange;
            GameObject temp = Instantiate(bulletproject);
            temp.transform.position = bulletposition.position;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletforce, 0);
            Destroy(temp.gameObject, 3f);
        }
        }

        else
        {
            if(Time.time > nextattack)
            {
                if(nextdown)
                {
                    down = true;
                    up = false;
                }

                if(up)
                {
                    nextdown = true;
                    center = false;
                    positionshoot = bulletposition;
                }

                if(center)
                {
                    up = true;
                    down = false;
                    positionshoot = bulletpos2;
                }

                if(down)
                {
                    center = true;
                    nextdown = false;
                    positionshoot = bulletpos3;
                }

                nextattack = Time.time + bulletrange;
                GameObject temp = Instantiate(bulletproject);
                temp.transform.position = positionshoot.position;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletforce, 0);
                Destroy(temp.gameObject, 3f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.ShowGameOver();
        }

        if(collision.gameObject.CompareTag("Bullet"))
        {
            life -= 1;
            Destroy(collision.gameObject);

            if(life == 0)
            {
                anim.SetTrigger("dead");
                Destroy(gameObject, 1.29f);
            }
        }
        
    }

}
