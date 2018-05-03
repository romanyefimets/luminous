using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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

    [SerializeField] private int health;
    [SerializeField] Transform respawnPt;



    bool isGrounded = false;
	public bool invControl = false;
	public float startTime;
    float timeDown = 0;

    private float colliderH;
    private float colliderW;

    private float colliderOffX;
    private float colliderOffY;


    void Start()
    {
		startTime = 0;
        myBody = GetComponent<Rigidbody2D>();
        myTrans = this.transform;
        //tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        speed = walkSpeed;

        colliderH = GetComponent<BoxCollider2D>().size.y;
        colliderW = GetComponent<BoxCollider2D>().size.x;

        colliderOffX =  GetComponent<BoxCollider2D>().offset.x;
        colliderOffY = GetComponent<BoxCollider2D>().offset.y;




    }

    void Update()
    {
		if (Time.time - startTime > 10f) {
			invControl = false;
		}
        //respawn player if they "die"
        if (health <= 0)
        {
            transform.position = respawnPt.transform.position;
            health = 1;
        }

        
        if (Input.GetButtonUp("Crouch") || Input.GetButtonUp("Sprint"))
        {
            speed = walkSpeed;
            GetComponent<BoxCollider2D>().size = new Vector2(colliderW, colliderH);
           GetComponent<BoxCollider2D>().offset = new Vector2(colliderOffX, colliderOffY);


        }
        else if (Input.GetButton("Sprint") && isGrounded)
        {
            print("sprint");
            speed = runSpeed;
            GetComponent<BoxCollider2D>().size = new Vector2(colliderW, colliderH);
            GetComponent<BoxCollider2D>().offset = new Vector2(colliderOffX, colliderOffY);



        }
        else if (Input.GetButton("Crouch"))
        {
             GetComponent<BoxCollider2D>().size = new Vector2(colliderW, colliderH / 2);
            GetComponent<BoxCollider2D>().offset = new Vector2(colliderOffX,  - colliderH / 4);
            speed = crouchSpeed;
        }




        animator.SetFloat("speed", myBody.velocity.magnitude);



        flipCharacter();




        //isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);
        //isGrounded = true;
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
		if (invControl == false) {
			//Input.GetKey(KeyCode.LeftShift);
			Vector2 moveVel = myBody.velocity;
			moveVel.x = horizontalInput * speed;
			myBody.velocity = moveVel;
		} else {
			Vector2 moveVel = myBody.velocity;
			moveVel.x = (-1) * horizontalInput * speed;
			myBody.velocity = moveVel;
		}
    }

    public void Jump()
    {
        if (isGrounded)
            myBody.velocity += jumpVelocity * Vector2.up;
    }

	private void OnTriggerStay2D(Collider2D other){
		if(other.tag != "Player")
			isGrounded = true;
	}

	private void OnTriggerExit2D(Collider2D other){
		if(other.tag != "Player")
			isGrounded = false;
	}

}
