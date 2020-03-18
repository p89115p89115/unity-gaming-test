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
    private Vector2 enemy;
    public string colliderTag; //碰撞對象的tag 
    public float backForce; //後彈的力道
    public float backForceX;
    public float backForceY;
    //public bool Invincible;
    private GameObject PS; //子物件 playerSprite
    private SpriteRenderer PSSR; //player的圖片
    private GameObject TriggerCollider;
    private shakeTest sT;

    private Cinemachine.CinemachineCollisionImpulseSource MyInpulse;
    private void Start()
    {
        //  ani = GetComponent<Animator>();
        md = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody2D>();
        PS = gameObject.transform.GetChild(3).gameObject;
        PSSR = PS.GetComponent<SpriteRenderer>();
        TriggerCollider = gameObject.transform.GetChild(2).gameObject;
        sT = GameObject.Find("Main Camera").GetComponent<shakeTest>();
        MyInpulse =GameObject.Find("CMvcam").GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
    }
    void FixedUpdate()
    {
        if (heathPoint <= 0) //血量歸 0 就消滅 
        {
            if (transform.name == "Player")
            {
                gameObject.active = false;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        if (isHit)
        {
            StartCoroutine(blink()); //閃爍效果 和 無敵
            backoff(); //後彈
            //Shake(); 
            if(gameObject.name == "Player")
            MyInpulse.GenerateImpulse();//震動

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.transform.position;
        if (collision.gameObject.tag == colliderTag)
        {
            physicalQuality PQ;
            PQ = collision.gameObject.GetComponent<physicalQuality>();
            heathPoint -= PQ.attackPoint; //扣血            
            isHit = true;

        }
        if (collision.gameObject.transform.name == "DeadZone") //掉到懸崖
        {
            heathPoint = 0;
            if (this.transform.name == "Player")
            {

                gameObject.active = false;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void backoff()
    {
        rb.velocity = Vector2.zero; //歸零(不知道有沒有效果        
        Vector2 backV;
        if (enemy.x > transform.position.x)
        {
            backV = new Vector2(transform.position.x - backForceX, transform.position.y + backForceY);
        }
        else
        {
            backV = new Vector2(transform.position.x + backForceX, transform.position.y + backForceY);
        }
        rb.AddForce(backV * backForce);
        //transform.position = backV;
        isHit = false;
    }

    IEnumerator blink()
    {

        TriggerCollider.active = false;
        PSSR.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        PSSR.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        PSSR.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        PSSR.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        PSSR.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        PSSR.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        TriggerCollider.active = true;
    }

    private void Shake()
    {

        
    }
}
