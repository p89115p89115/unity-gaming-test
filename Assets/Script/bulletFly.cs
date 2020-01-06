using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFly : MonoBehaviour
{

   
    public float destotyTime=3;
    public GameObject target; //飛行目標
    private Vector3 flyto;
    public float moveSpeed; //飛行速度
    private Vector2 aaa;


    private void Start()
    {
        
        Destroy(gameObject, destotyTime); //兩秒後消滅
      
        flyto = target.transform.position; //按下射擊時所偵測到的 target 位置    
        GetComponent<Rigidbody2D>().AddForce((flyto - transform.position).normalized * moveSpeed);        
        // normalized是為了防止 較近的target addforce較小
        // 可是感覺還是有速度不一樣的問題    
        
    }

    void FixedUpdate()
    {
        Debug.DrawLine(transform.position,flyto, Color.red);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    //    if (collision.gameObject.name != "Player")
    //        Destroy(gameObject);//撞到東西就消滅
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "TriggerCollider" && collision.gameObject.name != "Player")
            Destroy(gameObject);//撞到東西就消滅
    }

}


   
