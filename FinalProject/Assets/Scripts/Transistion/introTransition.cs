using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSecondsRealtime(8);
        FindObjectOfType<Fade>().GetComponent<Fade>().blackScreen.SetBool("fadeOut", true);

        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("FirstArea");
    }
}
