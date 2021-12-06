using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarthaSzabolcs.Tutorial_SpriteFlash.Example
{
    public class FlyerMove : MonoBehaviour
    {

        public float speed = 2f;
        public float followRange = 10f;

        public ParticleSystem slashSys;
        public ParticleSystem bloodSys;

        SpriteRenderer sprite;
        Rigidbody2D rbody;

        int enemyHP = 3;
        
        float initObjX;
        bool following = false;

        public float hitKnockback;
        public float playerKnockback = 40f;
        public float playerAtkKnockback;
        public float knockbackReturn;

        Vector2 knockbackDirection = Vector2.zero;
        GameObject player;

        void Start()
        {
            initObjX = gameObject.transform.position.x; //get the starting pos

            sprite = GetComponent<SpriteRenderer>();
            rbody = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {

            if (Vector3.Distance(transform.position, player.transform.position) < followRange && !following) following = true;
            if (following)
            {
                Vector3 move = (player.transform.position - transform.position).normalized;
                rbody.velocity = move * speed;
                if (move.x < 0f) sprite.flipX = true;
                else sprite.flipX = false;
            }
            else rbody.velocity = Vector3.zero;

            if (knockbackDirection != Vector2.zero) knockbackDirection *= Mathf.Pow(knockbackReturn, Time.deltaTime);
            if (knockbackDirection.sqrMagnitude <= 0.05f) knockbackDirection = Vector2.zero;
            rbody.velocity = rbody.velocity + knockbackDirection;
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
