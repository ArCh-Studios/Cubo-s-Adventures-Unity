using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    
    public float speed = 1f;
    public float jump = 15f;
    private float moveHorizontal = 0f;
    private float x = 0;
    private bool moveUp = false;
    private bool floor = true;
    private bool wall = false;
    private bool faceRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveUp = Input.GetKey("up") || Input.GetKey("w");
        if ((moveHorizontal < 0 && faceRight) || (moveHorizontal > 0 && !faceRight))
        {
            Flip();
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void FixedUpdate()
    {
        x += moveHorizontal;
        x *= .9f;
        if (moveUp)
        {
            if (floor)
            {
                floor = false;
                rb.velocity = new Vector3(x, jump, 0);
            }
            else if (wall)
            {
                wall = false;
                rb.velocity = new Vector3(x, jump * 2f / 3f, 0);
                if (x < 0)
                    x = jump / 3f;
                else
                    x = jump * -1f / 3f;
            }

        }
        rb.velocity = new Vector3(x, rb.velocity.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            floor = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            wall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            floor = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            wall = false;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        faceRight = !faceRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
