using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callWalkingPart : MonoBehaviour
{
    public GameObject player;
    ParticleSystem currentParticle;
    public ParticleSystem left;
    public ParticleSystem right;

    public void callParticle()
    {
        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            currentParticle = left;
        } else
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            currentParticle = right;
        }

        currentParticle.Play();
        Debug.Log("burst");
    }

  
}
