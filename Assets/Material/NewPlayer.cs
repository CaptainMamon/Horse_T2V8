using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    private RaycastHit2D landingHit;
    private RaycastHit2D leftHit;
    private RaycastHit2D rightHit;
    private RaycastHit2D topHit;
    private BoxCollider2D playerColider;

    float rightPositionX;
    float topPositionY;
    float bottomPositionY;
    float leftPositionX;
    Rigidbody2D playerBody;

    public float speed;
    public float jumpHeight;

    public bool allowedToJump;
    public bool touchingWall;
    // Start is called before the first frame update
    void Start()
    {
        allowedToJump = true;

        playerBody = GetComponent<Rigidbody2D>();
        playerColider = GetComponent<BoxCollider2D>();
        rightPositionX = playerColider.bounds.max.x + .1f;
        topPositionY = playerColider.bounds.max.y + .1f;
        bottomPositionY = playerColider.bounds.min.y - .1f;
        leftPositionX = playerColider.bounds.min.x - .1f;

    }

    void Update()
    {

        GetInput();

        landingHit = Physics2D.Raycast(new Vector2(this.transform.position.x, bottomPositionY + transform.position.y), new Vector2(transform.position.x, 0.2f));
        leftHit = Physics2D.Raycast(new Vector2(leftPositionX + transform.position.x, this.transform.position.y), new Vector2(leftPositionX - 0.2f, 0.0f), 0.2f);
        rightHit = Physics2D.Raycast(new Vector2(rightPositionX + transform.position.x, this.transform.position.y), new Vector2(rightPositionX + 0.2f, 0.0f), 0.2f);
        topHit = Physics2D.Raycast(new Vector2(this.transform.position.x, topPositionY + transform.position.y), new Vector2(transform.position.x, 0.2f), 0.2f);

        Debug.DrawRay(new Vector2(rightPositionX + transform.position.x, this.transform.position.y), new Vector2(rightPositionX + 0.2f, 0.0f), Color.black);

        //Debug.Log(leftHit.collider.tag);
        if (landingHit.collider.tag == "floor")
        {
            allowedToJump = true;
            Debug.Log("Hit the floor");
        }
        if (topHit.collider != null)
        {
            if (topHit.collider.tag == "floor")
            {
                allowedToJump = false;
                Debug.Log("Hit the top");
            }
        }
        if (leftHit.collider != null)
        {
            if (leftHit.collider.tag == "wall")
            {

                touchingWall = true;
            }

        }


        if (rightHit.collider != null)
        {
            if (rightHit.collider.tag == "wall")
            {
                touchingWall = true;
            }
        }




        if (rightHit.collider == null && leftHit.collider == null)
        {
            touchingWall = false;
        }

        if (touchingWall)
        {
            allowedToJump = true;
        }



        /*   Debug.DrawRay(new Vector2(this.transform.position.x, bottomPositionY + transform.position.y), new Vector2(0, -0.5f), Color.red);
           Debug.DrawRay(new Vector2(this.transform.position.x, topPositionY + transform.position.y), new Vector2(0, 0.2f), Color.red);
           Debug.DrawRay(new Vector2(leftPositionX + transform.position.x, this.transform.position.y), new Vector2(leftPositionX - 0.2f, 0), Color.red);
           Debug.DrawRay(new Vector2(rightPositionX + transform.position.x, this.transform.position.y), new Vector2(0.2f, 0), Color.red);*/

    }
    void FixedUpdate()
    {

    }
    private void GetInput()
    {

        float direction = Input.GetAxisRaw("Horizontal");


        float move = 0.0f;
        if (direction != 0.0f)
        {
            move = direction * speed;
            playerBody.velocity = new Vector2(move, playerBody.velocity.y);
        }
        if (Input.GetKey(KeyCode.Space) && allowedToJump)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpHeight);
            allowedToJump = false;
        }
    }
}