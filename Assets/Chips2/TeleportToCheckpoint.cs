using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToCheckpoint : MonoBehaviour
{
    private bool onStay;
    public Transform player;

    float timer = 0;
    private void FixedUpdate()
    {
        if (onStay)
        {
           

            Checkpoint.TeleportToLastCheckpoint(player.transform);
            onStay= false;

        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onStay = true;
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onStay = false;
        }
    }

}

