using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    float length; // width of the sprite 
    float height;
    float startPosX;
    float startPosY;

    public GameObject mainCam;
    public float parallaxAmnt;

    bool nearby;

   
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x; //gets the width of the sprite 
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }


    void Update()
    {
        float tempX = mainCam.transform.position.x * (1 - parallaxAmnt); //checking if sprite about to be off the screen
        float tempY = mainCam.transform.position.y * (1 - parallaxAmnt);

        float distX = (mainCam.transform.position.x * parallaxAmnt); // the distance the image is travelling... moving in relation to the cam move
        float distY = mainCam.transform.position.y * parallaxAmnt;

        float distanceX = Mathf.Abs(mainCam.transform.position.x) - Mathf.Abs(gameObject.transform.position.x);
        float distanceY = Mathf.Abs(mainCam.transform.position.y) - Mathf.Abs(gameObject.transform.position.y); 

        if (distanceX < length && distanceY < height)
        {
            nearby = true;
        } else
        {
            nearby = false;
            transform.position = new Vector3(startPosX, startPosY, transform.position.z);
        }

        Debug.Log(nearby);

        if (nearby)
        {
            transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);
        }

        //if (tempX > startPosX + length) //if greater than length of sprite and current location
        //{
        //    startPosX += length; // mover the sprite
        //} 
        //else if ( tempX < startPosX - length)
        //{
        //    startPosX -= length;
        //}

        //if (tempY > startPosY + height) //if greater than height of sprite and current location
        //{
        //    startPosY += height; // mover the sprite
        //}
        //else if (tempY < startPosY - height)
        //{
        //    startPosY -= height;
        //}


    }
}
