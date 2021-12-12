using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    public string newScene;
    // Start is called before the first frame update
    public void nextScene()
    {
        StartCoroutine(delay());
    }


    IEnumerator delay()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(newScene);
    }


}
