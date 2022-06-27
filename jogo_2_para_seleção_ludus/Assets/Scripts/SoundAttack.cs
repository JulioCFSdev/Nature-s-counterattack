using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class SoundAttack : MonoBehaviour
{

    [SerializeField]
    private float speed;
    private Vector2 direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.right; // (1, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject, 0.8f);
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Cyborg"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
