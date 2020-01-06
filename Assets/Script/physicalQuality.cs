using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class physicalQuality : MonoBehaviour
{
    public float heathPoint; //血量
    public float attackPoint; //攻擊力
    //private Animator ani;
    private MeshRenderer md;
    private Rigidbody2D rb;
    public bool isHit; 
    private Vector2 enemyV;
    public string colliderTag; //碰撞對象的tag 
    public float backForceX; //後彈的力道
    public float backForceY;
    //public bool Invincible;



    private void Start()
    {        
      //  ani = GetComponent<Animator>();
        md = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody2D>();
    
    }   
    void FixedUpdate()
    {
        if (heathPoint <= 0) //血量歸 0 就消滅 
            Destroy(this.gameObject);
        if (isHit)
        {
            StartCoroutine(blink()); //閃爍效果
            backoff(); //後彈
            isHit = false;
        }          
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == colliderTag)
        {
            enemyV = collision.transform.position;
            physicalQuality PQ;
            PQ = collision.gameObject.GetComponent<physicalQuality>();
            heathPoint -= PQ.attackPoint; //扣血            
            isHit = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == colliderTag)
        {
            //enemyV = collision.transform.position;

            physicalQuality PQ;

            PQ = collision.gameObject.GetComponent<physicalQuality>();
            heathPoint -= PQ.attackPoint; //扣血            
            isHit = true;
        }
    }

    void backoff() 
    {
        rb.velocity = Vector2.zero; //歸零(不知道有沒有效果
        Vector2 backV;
        if (enemyV.x > transform.position.x)
        {
            backV = new Vector2(transform.position.x - backForceX, transform.position.y + backForceY);           
        }
        else
        {
            backV = new Vector2(transform.position.x + backForceX, transform.position.y + backForceY);
        }
        //rb.AddForce(backV * backForce);
        transform.position = backV;
    }
   
    IEnumerator blink()
    {      
        md.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        md.GetComponent<MeshRenderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        md.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        md.GetComponent<MeshRenderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        md.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        md.GetComponent<MeshRenderer>().material.color = Color.white;
    }

}
