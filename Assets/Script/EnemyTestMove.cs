using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestMove : MonoBehaviour
{
    public float movingTime,moveSpeed;
    Vector2 currentVelocity = Vector3.zero;
    // Update is called once per frame
    public Vector3 target;
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, target, ref currentVelocity, movingTime, moveSpeed);
    }
}
