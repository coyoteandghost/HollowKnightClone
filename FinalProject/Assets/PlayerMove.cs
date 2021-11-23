using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject weaponSlash;

    public float speed;
    public float jumpSpeed;
    public float jumpHoldTime;
    public float attackReload;
    float currAttackTime = 0f;
    float currJumpTime = 0f;
    int dir = 1;

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
        CheckAttack();
    }

    void CheckMove()
    {
        hmove = Input.GetAxisRaw("Horizontal") * speed;
        rb.velocity = new Vector2(hmove, rb.velocity.y);
        if (hmove != 0) dir = (int)Mathf.Sign(hmove);
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
    void CheckAttack()
    {
        currAttackTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.X) && currAttackTime > attackReload)
        {
            Instantiate(weaponSlash, new Vector3(transform.position.x + (1.05f * dir), transform.position.y - 0.1f, transform.position.z), transform.rotation, transform);
            currAttackTime = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y < transform.position.y) currJumpTime = 0;
    }
}
