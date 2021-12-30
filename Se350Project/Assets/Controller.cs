using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    
    
    private enum State {idle, running, jumping, falling}
    private State state = State.idle;
    


    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jForce = 10f;


    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    private void Update()
    {
        Movement();
        StateSwitch();
        anim.SetInteger("state", (int)state);


    }
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1,1);
           

        }

        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1,1);
          

        }

        //jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jForce);
            state = State.jumping;
        }


    }

    private void StateSwitch()
    {
        if(state == State.jumping){
            if(rb.velocity.y < .1f ){
                state = State.falling;
            }
        }
        else if(state == State.falling){
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > 2f){
            //moving
            state = State.running;
        } 
        else{
            state = State.idle;
        }

    }
}

    


