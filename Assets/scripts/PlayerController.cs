using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed = 10, jumpVelocity = 10;
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
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Shift") && isGrounded)
        {
            speed = 20;
        }
        if (Input.GetButtonUp("Shift"))
        {
            speed = 10;
        }

        animator.SetFloat("speed", myBody.velocity.magnitude);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = false;


        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);
        Move(Input.GetAxisRaw("Horizontal"));
        if (Input.GetButtonDown("Jump"))
            Jump();
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
