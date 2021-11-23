using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{

    public Vector3 safePos;
   // public Vector3 checkpointSafePos;
   // public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHP>().health -= 1; //whenplayer gets hit, minus health

            collision.gameObject.GetComponent<Transform>().position = safePos; //sends player to safePos
        }
    }

}
