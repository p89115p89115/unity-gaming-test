using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject target;//欲前往的目標
    public float moveSpeed;//移動速度
    Vector3 currentVelocity = Vector3.zero;
    public float smoothTime;
    
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref currentVelocity, smoothTime, moveSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
       // if(collision.gameObject.name == "bullet(Clone)")
        Destroy(this.gameObject);
        
    }
}
