using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    public Transform spawnPoint;


    public ParticleSystem dust;
    private enum State {idle, running, jumping, falling, hurt}
    private State currentState = State.idle;
    
    


    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jForce = 10f;
    [SerializeField] int diamond;
    [SerializeField] private Text diamondCount;
 
    [SerializeField] private float hurtForce = 10f;

    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource diamondSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource killSound;
    [SerializeField] private AudioSource finishSound;
    



    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    private void Update()
    {
        if(currentState != State.hurt)
        {
            Movement();
        }
        StateSwitch();
        anim.SetInteger("state", (int)currentState);
    }   
     private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Diamond")
        {
            diamondSound.Play();
            diamond += 1;
            diamondCount.text = "x" + diamond.ToString();
            EndMenuScript.ResultText = diamondCount.text;
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Trap")
        {
            currentState = State.hurt;
            deathSound.Play();
            Invoke("Respawn",1);
        
        }
        if(collision.tag == "Door")
        {
        anim.SetBool("Win",true);
        finishSound.Play();
        Invoke("nextScene",1);
        }
    }

    private void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Respawn(){
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 0 );
        currentState=State.idle;
        
    }
    private void Movement()
    {
        float horizontalDirection = Input.GetAxis("Horizontal");

        if (horizontalDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1,1);
        }

        else if (horizontalDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1,1);
        }

        //jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            jumpingSound.Play();
            Jump();
        }
    }
  
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jForce);
        currentState = State.jumping;
    }

    private void StateSwitch()
    {
        if(currentState == State.jumping){
            if (rb.velocity.y < .01f ){
                currentState = State.falling;
            }
        }
        else if(currentState == State.falling){
            if(coll.IsTouchingLayers(ground))
            {
                currentState = State.idle;
                CreateDust();
            }
        }
        else if(currentState == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > 2f){
            //moving
            currentState = State.running;
        }
        else{
            currentState = State.idle;
        }

    }

    void CreateDust()
    {
        dust.Play();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
            if(currentState == State.falling)
            {
                killSound.Play();
                enemy.Death();
                Jump();
            }
            else
            {
                currentState = State.hurt;
                
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    deathSound.Play();
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    
                }

                else
                {
                    deathSound.Play();
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }  
                Invoke("Respawn", 1);
            }
        }
    }
    
}

    


