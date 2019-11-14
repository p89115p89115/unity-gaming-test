using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public float movespeed;
    //移動速度
  
    void Update()
    {
        float movementH = 0f;
        float movementV = 0f;
        movementH = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        movementV = Input.GetAxis("Vertical") * movespeed * Time.deltaTime;
        Vector3 newPos = new Vector3(transform.position.x + movementH, transform.position.y + movementV, transform.position.z);
        transform.position = newPos;
    }
}
