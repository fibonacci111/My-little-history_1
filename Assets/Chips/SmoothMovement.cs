using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Transform targetPosition;
    [SerializeField] float LifeTime;
    private float time = 0;

    private void FixedUpdate()
    {
        if (time <= LifeTime) {
            time += 1f * Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            time = 0;
        }
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
    }
    

}