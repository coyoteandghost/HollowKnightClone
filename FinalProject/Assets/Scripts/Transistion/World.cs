using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : SceneSwitch
{
    public Transform player;
    public float posX;
    public float posY;
    public float posZ;

    public string previous;

    public override void Start()
    {
        base.Start();

        if(prevScene == previous)
        {
            player.position = new Vector3(posX, posY, posZ);
        }
    }

}
