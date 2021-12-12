using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator blackScreen;
    
    public void fadeIn()
    {
        blackScreen.SetBool("fadeIn", true);
    }

    public void fadeOut()
    {
        blackScreen.SetBool("fadeOut", true);
    }




}
