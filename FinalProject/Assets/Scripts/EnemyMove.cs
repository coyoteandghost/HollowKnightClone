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

        public float platformAdjust;

        void Start()
        {          
            initObjX = gameObject.transform.position.x; //get the starting pos

            sprite = GetComponent<SpriteRenderer>();
            rbody = GetComponent<Rigidbody2D>();

            platformMax = platform.GetComponent<SpriteRenderer>().bounds.max.x;
            platformMin = platform.GetComponent<SpriteRenderer>().bounds.min.x;
            Debug.Log(platformMin);
        }

        void Update()
        {
            if (GetComponent<BoxCollider2D>().bounds.min.x <= platformMin || GetComponent<BoxCollider2D>().bounds.max.x >= platformMax) SwitchDirection();

            rbody.velocity = new Vector3(speed * dir, rbody.velocity.y);
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


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player") // if collide with player subtract its hp
            {
                collision.gameObject.GetComponent<PlayerHP>().health -= 1;
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