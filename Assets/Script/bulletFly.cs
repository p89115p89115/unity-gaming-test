using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFly : MonoBehaviour
{
    //飛行速度
    public float flyingSpeed=0.5f;
    //飛行目標
    public GameObject target;
    private void Start()
    {
        Destroy(gameObject, 5);
    }
    void Update()
    {
        //物件移動 
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, target.transform.position, flyingSpeed);
        //碰到就消滅
       // if (this.transform.position == target.transform.position)
         //   Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        Destroy(gameObject);
    }

}


   
