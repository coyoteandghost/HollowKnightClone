using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BarthaSzabolcs.Tutorial_SpriteFlash.Example
{
    public class EnemyMove : MonoBehaviour
    {

        public float speed = 2f;
        public int dir = 1;

        public GameObject platform;

        public ParticleSystem slashSys;
        public ParticleSystem bloodSys;

        SpriteRenderer sprite;
        Rigidbody2D rbody;

        int enemyHP = 3;

        float platformMax;
        float platformMin;
        float initObjX;

        public float hitKnockback;
        public float playerKnockback = 40f;
        public float playerAtkKnockback;
        public float knockbackReturn;

        Vector2 knockbackDirection = Vector2.zero;

        void Start()
        {          
            initObjX = gameObject.transform.position.x; //get the starting pos

            sprite = GetComponent<SpriteRenderer>();
            rbody = GetComponent<Rigidbody2D>();

            if (dir == -1) sprite.flipX = true;

            platformMax = platform.GetComponent<SpriteRenderer>().bounds.max.x;
            platformMin = platform.GetComponent<SpriteRenderer>().bounds.min.x;
        }

        void Update()
        {
            if (GetComponent<BoxCollider2D>().bounds.min.x <= platformMin)
            {
                transform.Translate(platformMin - GetComponent<BoxCollider2D>().bounds.min.x, 0f, 0f);
                SwitchDirection();
            }
            else if (GetComponent<BoxCollider2D>().bounds.max.x >= platformMax)
            {
                transform.Translate(platformMax - GetComponent<BoxCollider2D>().bounds.max.x, 0f, 0f);
                SwitchDirection();
            }

            rbody.velocity = new Vector3(speed * dir, rbody.velocity.y);

            if (knockbackDirection != Vector2.zero) knockbackDirection *= Mathf.Pow(knockbackReturn, Time.deltaTime);
            if (knockbackDirection.sqrMagnitude <= 0.05f) knockbackDirection = Vector2.zero;
            rbody.velocity = rbody.velocity + knockbackDirection;
        }

        void SwitchDirection()
        {
            if (dir == 1)
            {
                dir = -1;
                sprite.flipX = true;
            }
            else
            {
                dir = 1;
                sprite.flipX = false;
            }
        }

        public void ApplyKnockback(float amount, bool vertical)
        {
            if (vertical) knockbackDirection = new Vector2(0f, amount);
            else knockbackDirection = new Vector2(amount, 0f);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player") // if collide with player subtract its hp
            {
                collision.gameObject.GetComponent<PlayerHP>().health -= 1;
                FindObjectOfType<PlayerMove>().ApplyKnockback(playerKnockback * Mathf.Sign(collision.gameObject.transform.position.x - transform.position.x), false);
                FindObjectOfType<freezeFrame>().Stop();
            }
        }



        [SerializeField] SimpleFlash flashEffect;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Slash")
            {
                flashEffect.Flash();
                slashSys.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 0);
                slashSys.Play();
                FindObjectOfType<freezeFrame>().Stop();
                enemyHP--;
                enemyDie();
                ApplyKnockback(hitKnockback * Mathf.Sign(transform.position.x - collision.gameObject.transform.position.x), false);
                FindObjectOfType<PlayerMove>().ApplyKnockback(playerAtkKnockback * Mathf.Sign(collision.gameObject.transform.position.x - transform.position.x), false);
            }
        }


        void enemyDie()
        {
            if (enemyHP <= 0)
            {
                bloodSys.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                bloodSys.Play();
                Destroy(gameObject);
            }
        }
    }
}