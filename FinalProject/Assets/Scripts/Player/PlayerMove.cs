using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BarthaSzabolcs.Tutorial_SpriteFlash.Example
{
    public class PlayerMove : MonoBehaviour
    {
        public GameObject weaponSlash;
        SpriteRenderer sprite;

        public float speed;
        //public float jumpSpeed;
        //public float jumpHoldTime;
        public float attackReload;
        public float knockbackReturn = 0.996f;

        float currAttackTime = 0f;
        //float currJumpTime = 0f;
        int dir = 1;
        int vertDir = 1;
        public bool verticalSlash = false;
        bool slashFlip = true;
        GameObject currentAttack;

        Vector2 knockbackDirection = Vector2.zero;

        HandleSound soundHandler;

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

        [SerializeField] SimpleFlash flashEffect;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            soundHandler = FindObjectOfType<HandleSound>();
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
            if (hmove != 0 && isGrounded) soundHandler.PlaySound(0);
            else soundHandler.StopSound(0);
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                vertDir = (int)Mathf.Sign(Input.GetAxis("Vertical"));
                verticalSlash = true;
            }
            else
            {
                verticalSlash = false;
                if (hmove != 0) dir = (int)Mathf.Sign(hmove);
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

            if (isGrounded == true && Input.GetKeyDown(KeyCode.Z))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                gameObject.GetComponent<PlayerSprite>().touchingFloor = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter += Time.deltaTime;
                soundHandler.PlaySound(1);
            }

            if (Input.GetKey(KeyCode.Z) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
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
                gameObject.GetComponent<PlayerSprite>().touchingFloor = true;
                soundHandler.PlaySound(2);
            }
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.name)
            {
                case "Music1":
                    soundHandler.StartMusic();
                    Destroy(collision.gameObject);
                    break;
                case "Music2":
                    soundHandler.StartCoroutine("FadeIn", 1);
                    Destroy(collision.gameObject);
                    break;
                case "Music3":
                    soundHandler.StartCoroutine("FadeIn", 2);
                    Destroy(collision.gameObject);
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("CameraControl"))
            {
                CameraTrigger cameraTrigger = collision.gameObject.GetComponent<CameraTrigger>();
                float camCoord;
                if (cameraTrigger.triggerIsVertical) camCoord = transform.position.y - collision.transform.position.y;
                else camCoord = transform.position.x - collision.transform.position.x;
                if (camCoord < 0) Camera.main.GetComponent<CameraFollow>().worldBounds = cameraTrigger.leftOrDownBound;
                else Camera.main.GetComponent<CameraFollow>().worldBounds = cameraTrigger.rightOrUpBound;
                Camera.main.SendMessage("SetBounds");
            }
        }
    }
}