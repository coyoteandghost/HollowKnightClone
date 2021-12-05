using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour
{

   
    public float magnitude;
    public float duration;
    public float smoothing;

    public GameObject player;
    Vector3 playerPos;


    public void startShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f; //how much has passed

        playerPos = new Vector3(gameObject.GetComponent<CameraFollow>().smoothPos.x, gameObject.GetComponent<CameraFollow>().smoothPos.y, -10); 

        while (elapsed < duration) // as long as time elapsed is not more than duration
        {
            
            float x = Random.Range((gameObject.transform.localPosition.x - 1.0f) * magnitude, (gameObject.transform.localPosition.x + 1.0f) * magnitude);
            float y = Random.Range((gameObject.transform.localPosition.y - 0.5f) * magnitude, (gameObject.transform.localPosition.y + 0.5f) * magnitude);

            transform.localPosition = Vector3.Lerp
                (
                transform.position, 
                new Vector3(x, y, transform.localPosition.z), 
                smoothing
                );

            elapsed += Time.deltaTime; //add time until it exceeds duration

            yield return null;
        }

        transform.localPosition = playerPos;
    }

}
