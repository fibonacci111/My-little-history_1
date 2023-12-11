using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmoothMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Transform targetPosition;
    [SerializeField] float LifeTime;
    private float time = 0;
    [SerializeField] bool isKiller;

    private void FixedUpdate()
    {
        if (gameObject.active == true)
        {
            if (time <= LifeTime)
            {
                time += 1f * Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                
            }
        }
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&isKiller)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}