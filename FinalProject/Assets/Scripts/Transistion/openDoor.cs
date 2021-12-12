using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    private bool playerDetected;
    //public Transform doorPos;
    public float width;
    public float height;

    public LayerMask whatIsPlayer;

    [SerializeField]
    private string sceneName;

    SceneSwitch sceneSwitch;

    private void Start()
    {
        sceneSwitch = FindObjectOfType<SceneSwitch>();
    }

    private void Update()
    {
        //playerDetected = Physics2D.OverlapBox(doorPos.position, new Vector2(width, height), 0, whatIsPlayer);

        if (playerDetected == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(changeScene());
                
            }
        }
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        FindObjectOfType<Fade>().GetComponent<Fade>().blackScreen.SetBool("fadeOut", true);

        yield return new WaitForSecondsRealtime(0.5f);
        sceneSwitch.SwitchScene(sceneName);
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(doorPos.position, new Vector3(width, height, 1));
    }*/

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.tag == "Player")
        {
            playerDetected = true;
        }
    }
}
