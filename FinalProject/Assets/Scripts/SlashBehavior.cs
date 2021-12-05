using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBehavior : MonoBehaviour
{
    float slashTime = 0.35f;
    float time = 0f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= slashTime)
        {
            GameObject.FindObjectOfType<PlayerSprite>().playerSprite.SetBool("attacking", false);
            GameObject.FindObjectOfType<PlayerSprite>().playerSprite.SetBool("up", false);
            GameObject.FindObjectOfType<PlayerSprite>().playerSprite.SetBool("down", false);
            Destroy(gameObject);
        }
    }
}
