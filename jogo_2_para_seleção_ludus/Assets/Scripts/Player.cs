using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;
    public float attack_range;
    private float nextattack;

    public bool isJumping;
    public bool doubleJump;
    private bool shoot;
    private bool isPaused;

    public GameObject bulletPosition;
    public GameObject bullet;

    [Header("Paineis e Menu")]
    public GameObject pausePanel;
    public string cena;

    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        Jump();
        Reset();
        Pause();
    }

    void FixedUpdate()
    {
        Shoot();
        Acao();
    }

    void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }

    void PauseScreen()
    {
        if(isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Reset()
    {
        shoot = false;
        if(Time.time > nextattack)
        {
            anim.SetBool("attack", false);
        }
    }

    void move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
    
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3 (0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3 (0f,180f,0f);
        }

        if(Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("walk", false);
        }
        
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            
            }
            
        }
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            shoot = true;
        }
    }


    void Acao()
    {
        if(shoot && Time.time > nextattack)
        {
            anim.SetTrigger("attack");
            rig.velocity = Vector3.zero;
            nextattack = Time.time + attack_range;
            ActionAttack();
        }
    }


    private void ActionAttack()
    {
        GameObject tmpBullet = (GameObject) (Instantiate(bullet, bulletPosition.transform.position, Quaternion.identity));

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
         if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("dead", true);
        }

        if(collision.gameObject.CompareTag("Shooter"))
        {
            anim.SetTrigger("dead");
        }
    }
}