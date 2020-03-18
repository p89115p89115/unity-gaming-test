using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFly : MonoBehaviour
{   
    public float destotyTime=3;
    public GameObject target; //飛行目標
    private Vector3 flyto;
    public float moveSpeed; //飛行速度
    public GameObject particle;

    private bool ex = false;
    private bool bu;

    public float backForce; //後彈的力道
    public float backForceX;
    public float backForceY;
    private void Start()
    {
        //判斷不同的武器種類
        if (transform.name == "explosion(Clone)")
        {
            Destroy(gameObject, 0.4f);
            ex = true;
       
        }
        if (transform.name == "bullet(Clone)")
        {
        flyto = target.transform.position; //按下射擊時所偵測到的 target 位置    
        GetComponent<Rigidbody2D>().AddForce((flyto - transform.position).normalized * moveSpeed); 
        // normalized是為了防止 較近的target addforce較小
        // 可是感覺還是有速度不一樣的問題          
         Destroy(gameObject, destotyTime); //兩秒後消滅      
            bu = true;
        } 
    }
    void FixedUpdate()
    {
        Debug.DrawLine(transform.position,flyto, Color.red);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collision" + collision.transform.name);
      
        if (bu)
        {
            if (collision.gameObject.name != "TriggerCollider" && collision.gameObject.name != "Player")
            {
                print("collision.gameObject.name: " + collision.gameObject.name);
                // Instantiate(particle, collision.transform.position, collision.transform.rotation);
                ContactPoint2D[] contacts = new ContactPoint2D[1];
                collision.GetContacts(contacts);

                foreach (ContactPoint2D contact in contacts)
                {
                    Vector2 hitPoint = contact.point;
                    print(hitPoint.ToString());
                    Instantiate(particle, hitPoint, collision.transform.rotation);
                }
                Destroy(gameObject);//撞到東西就消滅
                
            }
        }
        else if (transform.name == "explosion(Clone)") 
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject gun = GameObject.Find("Gun");
                print("gun.transform.position: " + gun.transform.position);
                print("collision.transform.position: " + collision.transform.position);

                Vector2 backV;
                if (gun.transform.position.x < collision.transform.position.x)
                {
                    backV = new Vector2 (backForceX, collision.transform.position.y + backForceY);
                }
                else
                {
                    backV = new Vector2(- backForceX, collision.transform.position.y + backForceY);
                }
                print("backV: "+backV);
                collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.transform.GetComponent<Rigidbody2D>().AddForce(backV * backForce);
            }
        }
    }
}


   
