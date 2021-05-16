using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpSpeed = 5f;

    private Rigidbody2D myBody;
    private Animator anim;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0f)
        {
            myBody.velocity = new Vector2(movementSpeed, myBody.velocity.y);
            GetComponent<SpriteRenderer>().flipX = false;
            anim.SetInteger("Speed", 1);
        }
        else if (h < 0f)
        {
            myBody.velocity = new Vector2(-movementSpeed, myBody.velocity.y);
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetInteger("Speed", 1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
            anim.SetInteger("Speed", 0);
        }

        // anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));

    }

    void PlayerJump()
    {
        if (myBody.velocity.y == 0f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                anim.SetBool("Jumping", true);
            }
            else
            {
                anim.SetBool("Jumping", false);
            }
        }
    }


}
