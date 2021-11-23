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


    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);

        if(health > healthMax)
        {
            health = healthMax;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }


            if(i < hpDisplayed)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }

    }

  
}
