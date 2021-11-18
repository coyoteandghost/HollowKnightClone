using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform; //transform of the player/obj to be followed
    public BoxCollider2D worldBounds; //where to stop the camera

    float XMin, XMax, YMin, YMax;
    float camX, camY;

    float camSize;
    float camRatio;

    Camera mainCam;
    Vector3 smoothPos; //intermediate position

    public float smoothRate; //control how quickly movement occurs


    // Start is called before the first frame update
    void Start()
    {
        XMin = worldBounds.bounds.min.x;
        XMax = worldBounds.bounds.max.x;
        YMin = worldBounds.bounds.min.y;
        YMax = worldBounds.bounds.max.y;

        mainCam = gameObject.GetComponent<Camera>();

        camSize = mainCam.orthographicSize; //gets size of camera
        camRatio = (XMax + camSize) / 8.0f; // creates ratio of cameras width to overall bounds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        camY = Mathf.Clamp(followTransform.position.y, YMin + camSize, YMax - camSize); // limit camera move between two numbers
        camX = Mathf.Clamp(followTransform.position.x, XMin + camRatio, XMax - camRatio);

        smoothPos = Vector3.Lerp(gameObject.transform.position, new Vector3(camX, camY, gameObject.transform.position.z), smoothRate);
        gameObject.transform.position = smoothPos;
    }

}
