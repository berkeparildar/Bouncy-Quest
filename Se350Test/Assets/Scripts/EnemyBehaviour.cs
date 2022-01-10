using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float leftcap;
    [SerializeField]private float rightcap;
    [SerializeField] private float speed = 3f;
    [SerializeField] private LayerMask ground;
    private bool facingleft = true;
    
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }
   public void Death(){
       gameObject.transform.GetComponent<Rigidbody2D>().drag = 10000;
       anim.SetTrigger("Death");
   }

   private void destroy(){
       Destroy(this.gameObject);
   }
    
    private void EnemyMovement(){
       
        if(facingleft)
        {
            if(transform.position.x > leftcap)
            {
                transform.localScale = new Vector2(-1,1);
                if(transform.localScale.x != 1)
                {
                    
                    rb.velocity = new Vector3(-speed, rb.velocity.y);
                    anim.SetBool("Running",true);
                    
                }
            }
            else
            {
                facingleft = false;
            }
        }  
        else
        {
            if(transform.position.x < rightcap)
            {
                transform.localScale = new Vector2(1,1);
                if(transform.localScale.x != -1){
                    
                    rb.velocity = new Vector3(speed, rb.velocity.y);
                    anim.SetBool("Running",true);

                }
            }
            else
            {
                facingleft = true;
            }    
        }
    }
    
}



