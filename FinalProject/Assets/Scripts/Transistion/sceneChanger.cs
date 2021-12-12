using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    public string newScene;
    public float timer;
    // Start is called before the first frame update
    public void nextScene()
    {
        StartCoroutine(delay());
    }


    IEnumerator delay()
    {
        yield return new WaitForSecondsRealtime(timer);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(newScene);
    }


}
