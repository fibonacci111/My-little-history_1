using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject SpawnObject;
    private bool enter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enter)
        {
            SpawnObject.SetActive(true);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 
        enter = true;
        }
    }
}
