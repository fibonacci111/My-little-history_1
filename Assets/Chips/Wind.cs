using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private bool enter;
    public float newGravity;
    
   


    void Update()
    {
        if(enter && PlayerController.Player_Singltone.umbrellaIsOpen)
        {
            PlayerController.Player_Singltone.gravity= newGravity;
        }else if(!enter&& PlayerController.Player_Singltone.umbrellaIsOpen) {
            PlayerController.Player_Singltone.gravity = PlayerController.Player_Singltone.staticGravity;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }
}
