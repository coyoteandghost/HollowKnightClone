using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1.19f;

    public GameObject platform;

    float platformPosRight;
    float platformWidth;
    float initObjX;

    public float platformAdjust;

    void Start()
    {

        initObjX = gameObject.transform.position.x;

        platformWidth = platform.GetComponent<SpriteRenderer>().bounds.size.x;
        platformPosRight = initObjX + (platformWidth/platformAdjust);
    }

    void Update()
    {
        //PingPong between 0 and 1
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp( new Vector3(initObjX, gameObject.transform.position.y , gameObject.transform.position.z) , new Vector3 (platformPosRight, gameObject.transform.position.y , gameObject.transform.position.z), time);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHP>().health -= 1;
        }
    }



}
