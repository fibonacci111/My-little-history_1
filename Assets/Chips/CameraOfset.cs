using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOfset : MonoBehaviour
{
    private bool enter;
    public Vector3 NewOfset;
    public Vector3 DefoulteOfset;

    public bool _NewOfset;
    void Update()
    {
        
        if (enter)
        {
            if (_NewOfset)
            {
                CameraController.instance.offset = NewOfset;
                enter = false;
            }
            else if (!_NewOfset)
            {
                CameraController.instance.offset = DefoulteOfset;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }
}
