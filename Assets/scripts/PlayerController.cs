using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float walkSpeed, runSpeed, crouchSpeed, jumpVelocity;

    float speed;
    public LayerMask playerMask;
    Transform myTrans, tagGround;


    [SerializeField]
    Rigidbody2D myBody;

    [SerializeField]
    Animator animator;


    bool isGrounded = false;


    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myTrans = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        speed = walkSpeed;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    

        animator.SetFloat("speed", myBody.velocity.magnitude);



        flipCharacter();

      


        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);
        Move(Input.GetAxisRaw("Horizontal"));
        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    public void flipCharacter()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void Move(float horizontalInput)
    {
        //Input.GetKey(KeyCode.LeftShift);
        Vector2 moveVel = myBody.velocity;
        moveVel.x = horizontalInput * speed;
        myBody.velocity = moveVel;
    }

    public void Jump()
    {
        if (isGrounded)
            myBody.velocity += jumpVelocity * Vector2.up;
    }
}
