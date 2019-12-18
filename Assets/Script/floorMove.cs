using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorMove : MonoBehaviour
{

    public float moveRange;
    public float moveSpeed;// 
    public float movingTime = 0.1f;      // 達到目標大約花費的時間。 一個較小的值將更快達到目標。
    private Vector3 Right,Left;   
    private bool rearchPoint=false;

    Vector3 currentVelocity = Vector3.zero;//移動速率
    void Start()
    {//設定左右點
         Left = new Vector3(transform.position.x - moveRange, transform.position.y, transform.position.z);
         Right = new Vector3(transform.position.x , transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        if (transform.localPosition == Left)//到達左點
            rearchPoint = false;
        if (transform.localPosition == Right)//到達右點
            rearchPoint = true;

        if (rearchPoint == true) //向左移動
            transform.position = Vector3.SmoothDamp(transform.position, Left, ref currentVelocity, movingTime, moveSpeed);
        //transform.position = Vector3.Lerp(transform.position, Left, moveSpeed);
        if (rearchPoint == false)//向右移動
            transform.position = Vector3.SmoothDamp(transform.position, Right, ref currentVelocity, movingTime, moveSpeed);
        //transform.position = Vector3.Lerp(transform.position, Right, moveSpeed);
    }
   






}
   

