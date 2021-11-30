using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public Animator playerSprite;
    public SpriteRenderer currentSprite;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckVelocity();

    }

    void CheckVelocity()
    {
        //check if walking
        if (playerBody.velocity.x != 0)
        {
            playerSprite.SetBool("walking", true);
        } else
        {
            playerSprite.SetBool("walking", false);
        }

        //check if jumping or falling
        if (playerBody.velocity.y != 0)
        {
            if (playerBody.velocity.y > 0)
            {
                playerSprite.SetBool("jumping", true);
            }
            else if (playerBody.velocity.y < 0)
            {
                playerSprite.SetBool("falling", true);
            }
        }
        else
        {
            playerSprite.SetBool("falling", false);
            playerSprite.SetBool("jumping", false);
        }

        //check if attacking
        //ummmm do later lol


        //determine left or right facing
        if (playerBody.velocity.x > 0)
        {
            currentSprite.flipX = false;
        }
        else if(playerBody.velocity.x < 0)
        {
            currentSprite.flipX = true;
        }

    }
}
