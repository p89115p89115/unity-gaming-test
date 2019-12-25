using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GameObject target;//欲前往的目標
    public float moveSpeed;//移動速度
    Vector3 currentVelocity = Vector3.zero;
    public float smoothTime;
    
    void Update()
    {
        target = GameObject.Find("Player");
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref currentVelocity, smoothTime, moveSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
       if(collision.gameObject.tag == "bullet")
        Destroy(this.gameObject);
        
    }
}
