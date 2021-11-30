using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    public int health;
    public int healthMax;
    public int hpDisplayed;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;



    // Update is called once per frame
    void Update()
    {

        if(health > healthMax) //if health is going to exceed max, just make it max
        {
            health = healthMax;
        }

        for(int i = 0; i < hearts.Length; i++) //for the length of the health bar
        {
            if(i < health) //if the place in array is less than the max
            { 
                hearts[i].sprite = fullHeart; //show the heart
            } else
            {
                hearts[i].sprite = emptyHeart; //if not, show the empty 
            }


            if(i < hpDisplayed) //
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }

    }

  
}
