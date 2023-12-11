using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject SpawnObject;
    private bool enter;
    private bool enter2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enter)
        {
            SpawnObject.SetActive(true);
            enter = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!enter2) { 
        enter = true;
            enter2= true;
        }
    }
}
