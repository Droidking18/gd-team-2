using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerData
{
    public float timer;
    public float[] position;

    public PlayerData(Player player, float timer)
    {
        this.timer = timer;
        position = new float[3];

        if (player != null)
        {
            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;
        }
        else
        {
            // Default transform values
            position[0] = 10f;  
            position[1] = 37f;  
            position[2] = -29f;
        }
    }
}

