using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Transform targetPosition;

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
    }
}