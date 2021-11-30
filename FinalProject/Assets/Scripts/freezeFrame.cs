using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeFrame : MonoBehaviour
{

    public float duration;

    // Start is called before the first frame update
    public void Stop()
    {
        Time.timeScale = 0.0f; //stops the game -- 1.0f is normal speed, 2.0f would be double the speed, etc.
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSecondsRealtime(duration); //wait one second 
        Time.timeScale = 1.0f;
        FindObjectOfType<camShake>().startShake();
    }



}
