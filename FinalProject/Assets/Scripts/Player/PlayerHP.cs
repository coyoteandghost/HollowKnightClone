using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    public int health;
    public int healthMax;
    public int healthMin;
    public int hpDisplayed;

    public BoxCollider2D respawnBounds;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public ParticleSystem bloodSys;
    public float deathTime = 1.5f;

    public Animator fadeAnimator;



    // Update is called once per frame
    void Update()
    {
        Debug.Log(fadeAnimator);
        DeathCheck();
        UIHearts();
    }





    void UIHearts()
    {
        if (health > healthMax) //if health is going to exceed max, just make it max
        {
            health = healthMax;
        }

        for (int i = 0; i < hearts.Length; i++) //for the length of the health bar
        {
            if (i < health) //if the place in array is less than the max
            {
                hearts[i].sprite = fullHeart; //show the heart
            }
            else
            {
                hearts[i].sprite = emptyHeart; //if not, show the empty 
            }


            if (i < hpDisplayed) //
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }


    void DeathCheck()
    {
        {
            if (health == healthMin)
            {
                bloodSys.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                bloodSys.Play();
                StartCoroutine(DeathCamera());

                health = healthMax;
            }
        }
    }

    IEnumerator DeathCamera()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        yield return new WaitForSeconds(deathTime/2);

        fadeAnimator.GetComponent<Fade>().blackScreen.SetBool("fadeIn", false);
        fadeAnimator.GetComponent<Fade>().blackScreen.SetBool("fadeOut", true);

        yield return new WaitForSeconds(deathTime/2);

        fadeAnimator.GetComponent<Fade>().blackScreen.SetBool("fadeOut", false);
        fadeAnimator.GetComponent<Fade>().blackScreen.SetBool("fadeIn", true);

        Camera.main.GetComponent<CameraFollow>().worldBounds = respawnBounds;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        gameObject.transform.position = new Vector3(0, 0, 0);
        Camera.main.SendMessage("SetBounds");
    }

}


        
     
        


    