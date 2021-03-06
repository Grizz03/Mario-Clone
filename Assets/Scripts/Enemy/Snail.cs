using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator animator;

    public LayerMask playerLayer;

    private bool moveLeft;

    private bool canMove;
    private bool stunned;

    public Transform left_Collision, right_Collision, top_Collision, down_Collision;
    private Vector3 left_Collision_Pos, right_Collision_Pos;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        left_Collision_Pos = left_Collision.position;
        right_Collision_Pos = right_Collision.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }

        CheckCollision();

    }

    void CheckCollision()
    {
        // Raycasting for collisions around snail
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);


        // When jumping on top of snail will stun it(change animation) and player will also jump up
        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);

                    animator.Play("Stunned");
                    stunned = true;

                    // Beetle Code
                    if (tag == MyTags.BEETLE_TAG)
                    {
                        animator.Play("Beetle_Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        // When stunned is true, move shell Left or Right
        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // APPLY DAMAGE TO PLAYER
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(10f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    // APPLY DAMAGE TO PLAYER
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(-10f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }


        // If we don't detect collision
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            left_Collision.position = left_Collision_Pos;
            right_Collision.position = right_Collision_Pos;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            left_Collision.position = right_Collision_Pos;
            right_Collision.position = left_Collision_Pos;
        }
        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

}
