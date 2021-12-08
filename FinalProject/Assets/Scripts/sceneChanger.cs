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
        SceneManager.LoadScene(newScene);
    }
}
