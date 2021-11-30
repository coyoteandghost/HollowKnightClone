using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour
{

   
    public float magnitude;
    public float duration;
    public float smoothing;


    public void startShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        
        float elapsed = 0.0f; //how much has passed

        Vector3 initPos = transform.localPosition;

        while(elapsed < duration) // as long as time elapsed is not more than duration
        {

            float x = Random.Range(-1.0f * magnitude, 1.0f * magnitude);
            float y = Random.Range(-1.0f * magnitude, 1.0f * magnitude);

            transform.localPosition = Vector3.Lerp
                (
                transform.localPosition, 
                new Vector3(x, y, transform.localPosition.z), 
                smoothing
                );

            elapsed += Time.deltaTime; //add time until it exceeds duration
            Debug.Log("elapsed =" + elapsed);

            yield return null;
        }

        transform.localPosition = initPos; //return to init pos

    }

}
