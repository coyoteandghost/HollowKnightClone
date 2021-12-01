using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public Animator playerSprite;
    public SpriteRenderer currentSprite;

    public ParticleSystem walkTrailLeft;
    public ParticleSystem walkTrailRight;

    bool touchingFloor;



    // Update is called once per frame
    void FixedUpdate()
    {
        CheckVelocity();
        CheckMove();

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
       
            if (playerBody.velocity.y > 0)
            {
                playerSprite.SetBool("jumping", true);
            } else
              {
                  playerSprite.SetBool("jumping", false);
               }
            
        
            if (playerBody.velocity.y < 0)
            {
                playerSprite.SetBool("falling", true);
            } 
            else
             {
                   playerSprite.SetBool("falling", false);  
             }

        //check if attacking
        //ummmm do later lol



        //determine left or right facing
        if (playerBody.velocity.x > 0)
        {
            currentSprite.flipX = false;
            
        }
        
        
        if(playerBody.velocity.x < 0)
        {
            currentSprite.flipX = true;
        } 
       

    }



    bool isPlaying;

    void CheckMove()
    {

       if(playerSprite.GetBool("walking") == true && currentSprite.flipX == true && !isPlaying && touchingFloor)
        {
            walkTrailLeft.Play();
            isPlaying = true;
        }
        else
        {
            walkTrailLeft.Stop();
            isPlaying = false;
        }



        if (playerSprite.GetBool("walking") == true && currentSprite.flipX == false && !isPlaying && touchingFloor)
        {
            walkTrailRight.Play();
            isPlaying = true;
        }
        else
        {
            walkTrailRight.Stop();
            isPlaying = false;
        }


    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            touchingFloor = true;
        } else
        {
            touchingFloor = false;
        }
    }




}
