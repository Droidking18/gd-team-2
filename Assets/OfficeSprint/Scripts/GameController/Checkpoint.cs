using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter(Collider other)
    {
        //here im checking if the entity colliding with the checkpoint markre has tag "Player"
        if (other.CompareTag("Player")){
            gm.lastCheckPointPos = transform.position;
        }
    }
}
