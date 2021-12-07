using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject weaponSlash;
    SpriteRenderer sprite;
    
    public float speed;
    //public float jumpSpeed;
    //public float jumpHoldTime;
    public float attackReload;
    public float knockbackReturn = 0.996f;

    public AudioClip footstep, jump, land;
    AudioSource source;

    float currAttackTime = 0f;
    //float currJumpTime = 0f;
    int dir = 1;
    int vertDir = 1;
    public bool verticalSlash = false;
    bool slashFlip = true;
    GameObject currentAttack;

    Vector2 knockbackDirection = Vector2.zero;

    float hmove;
    Rigidbody2D rb;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        CheckJump();
        CheckAttack();
        if (knockbackDirection != Vector2.zero) knockbackDirection *= Mathf.Pow(knockbackReturn, Time.deltaTime);
        if (knockbackDirection.sqrMagnitude <= 0.05f) knockbackDirection = Vector2.zero;
        rb.velocity = rb.velocity + knockbackDirection;
    }

    void CheckMove()
    {

        hmove = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(hmove, rb.velocity.y);
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            vertDir = (int)Mathf.Sign(Input.GetAxis("Vertical"));
            verticalSlash = true;
        }
        else
        {
            verticalSlash = false;
            if (hmove != 0)
            {
                dir = (int)Mathf.Sign(hmove);
                if (!source.isPlaying && jumpTimeCounter == 0)
                {
                    source.clip = footstep;
                    //source.Play();
                    //source.loop = true;
                }
            }
            else
            {
                //source.Stop();
                //source.loop = false;
            }
        }
        sprite.flipX = dir < 0;
    }
    void CheckJump()
    {
        /*if (currJumpTime != 0)
        {
            currJumpTime += Time.deltaTime;
            if (Input.GetKey(KeyCode.Z) && currJumpTime < jumpHoldTime)
            {
                gameObject.GetComponent<PlayerSprite>().touchingFloor = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            if (currJumpTime == 0)
            {
                source.loop = false;
                //source.Stop();
                source.clip = jump;
                //source.Play();
                currJumpTime += Time.deltaTime;
            }
        }*/

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            source.loop = false;
                //source.Stop();
                source.clip = jump;
                //source.Play();
                jumpTimeCounter += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Z) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            isJumping = false;
        }
            
        
    }
    void CheckAttack()
    {
        currAttackTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.X) && currAttackTime > attackReload)
        {
            if (currAttackTime > 1f) slashFlip = true;
            Quaternion slashRot;
            Vector3 slashPos;
            if (verticalSlash)
            {
                slashRot = Quaternion.Euler(0, 0, 90 * vertDir);
                slashPos = new Vector3(transform.position.x, transform.position.y + (1.6f * vertDir), transform.position.z);
            }
            else
            {
                slashRot = Quaternion.Euler(Vector3.zero);
                slashPos = new Vector3(transform.position.x + (1.3f * dir), transform.position.y - 0.1f, transform.position.z);
            }
            currentAttack = Instantiate(weaponSlash, slashPos, transform.rotation * slashRot, transform);
            if (!verticalSlash) currentAttack.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
            currentAttack.GetComponent<SpriteRenderer>().flipY = slashFlip;
            if (!slashFlip) slashFlip = true;
            else slashFlip = false;
            currAttackTime = 0f;
        }
    }

    public void ApplyKnockback(float amount, bool vertical)
    {
        if (vertical) knockbackDirection = new Vector2(0f, amount);
        else knockbackDirection = new Vector2(amount, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y < transform.position.y)
        {
            jumpTimeCounter = 0;
            //source.Stop();
            source.loop = false;
            source.clip = land;
            //source.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CameraControl"))
        {
            Camera.main.GetComponent<CameraFollow>().worldBounds = (BoxCollider2D)collision;
            Camera.main.SendMessage("SetBounds");
        }
    }
}
