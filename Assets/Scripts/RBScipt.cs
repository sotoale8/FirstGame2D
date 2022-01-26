using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBScipt : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip powerUpSound;
    public float flyForce=8.0f;
    private float xmovement;
    private float ymovement;
    private Rigidbody2D RBrigidbody;
    private Animator RBanimator;

    public float jumpForce=4.3f;
    public float walkVelocity=1.0f;
    public float runVelocity=3.0f;
    private float velMovement;
    private bool isGrounded;
    private bool flyEnable = false;
    public GameObject switchOn;
    public GameObject PowerUp;
     

    // Start is called before the first frame update
    void Start()
    {
        RBrigidbody = GetComponent<Rigidbody2D>();
        RBanimator = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        xmovement = Input.GetAxisRaw("Horizontal");
        ymovement = Input.GetAxisRaw("Vertical");

               
      
       
    if (Input.GetKey(KeyCode.Space) && isGrounded)
    {
            isGrounded = false;
            Jump();
    }
        

    }
    void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RBanimator.SetBool("isWalking", true);
            RBanimator.SetBool("isRunning",false);
            velMovement = walkVelocity * xmovement;
        }
        else 
        {
            RBanimator.SetBool("isWalking", false);
            RBanimator.SetBool("isRunning", true);
            velMovement = runVelocity * xmovement;
        }

        if (xmovement > 0.0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            
        }
        if (xmovement < 0.0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }

        RBrigidbody.velocity = new Vector2(velMovement, RBrigidbody.velocity.y);

        
    }

    void Jump() 
    {   

        RBrigidbody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(jumpSound);
    }


    private void FixedUpdate()
    {
        if (xmovement != 0.0f)
        {
            Move();
        }
        else
        {
            RBanimator.SetBool("isRunning", false);
            RBanimator.SetBool("isWalking", false);
        }
        if (!isGrounded && flyEnable && Input.GetKey(KeyCode.W) ) 
        {
            fly();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground")) 
        { 
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Object.Destroy(PowerUp);
            flyEnable = true;
            Camera.main.GetComponent<AudioSource>().PlayOneShot(powerUpSound);

        }

      }

    private void fly() 
    {
        RBrigidbody.AddForce(Vector2.up * flyForce, ForceMode2D.Force);
    }
    

}
