using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float jumpHoldTime;
    float currJumpTime;

    float hmove;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        CheckJump();
    }

    void CheckMove()
    {
        hmove = Input.GetAxisRaw("Horizontal") * speed;
        rb.velocity = new Vector2(hmove, rb.velocity.y);
    }
    void CheckJump()
    {
        if (currJumpTime != 0)
        {
            currJumpTime += Time.deltaTime;
            if (Input.GetKey(KeyCode.Z) && currJumpTime < jumpHoldTime) rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            if (currJumpTime == 0) currJumpTime += Time.deltaTime;
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currJumpTime = 0;
    }
}
