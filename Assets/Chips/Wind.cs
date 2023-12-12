using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private bool enter;
    public float newGravity;
    
  public  void Update()
    {
        if(enter && PlayerController.Player_Singltone.umbrellaIsOpen)
        {
            PlayerController.Player_Singltone.gravity= newGravity;
            PlayerController.Player_Singltone.umbrellaOnWind= true;
        }else if(!enter&& PlayerController.Player_Singltone.umbrellaIsOpen) {
            PlayerController.Player_Singltone.gravity = (float)PlayerController.Player_Singltone.staticGravity;
            PlayerController.Player_Singltone.umbrellaOnWind = false;

        }
        if (!PlayerController.Player_Singltone.umbrellaIsOpen)
        {
            PlayerController.Player_Singltone.gravity = (float)PlayerController.Player_Singltone.staticGravity;
            PlayerController.Player_Singltone.umbrellaOnWind = false;
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
