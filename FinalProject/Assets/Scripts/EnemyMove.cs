using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BarthaSzabolcs.Tutorial_SpriteFlash.Example
{
    public class EnemyMove : MonoBehaviour
    {

        public float speed = 1.19f;

        public GameObject platform;

        public ParticleSystem slashSys;
        public ParticleSystem bloodSys;

        int enemyHP = 3;

        float platformPosRight;
        float platformWidth;
        float initObjX;

        public float platformAdjust;

        void Start()
        {          
            initObjX = gameObject.transform.position.x; //get the starting pos

            platformWidth = platform.GetComponent<SpriteRenderer>().bounds.size.x; //get width of platform its on
            platformPosRight = initObjX + (platformWidth / platformAdjust); //add width of platform to start pos
        }

        void Update()
        {
            Debug.Log(enemyHP);
            enemyDie();

            //PingPong between 0 and 1
            float time = Mathf.PingPong(Time.time * speed, 1);
            if (time < 0.01)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (time > 0.99)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }


            transform.position = Vector3.Lerp(new Vector3(initObjX, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(platformPosRight, gameObject.transform.position.y, gameObject.transform.position.z), time);
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
                enemyHP -= 1;
            }
        }


        void enemyDie()
        {
            if (enemyHP < 0)
            {
                bloodSys.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                bloodSys.Play();
                Destroy(gameObject);
            }
        }



    }


}