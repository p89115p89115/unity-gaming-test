using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public float movespeed;  //移動速度
    public bool isGround;
    public float CheckRadius;//檢測長度
       
    public float jumpHeight; //跳躍高度
    public float jumpAirForce;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        
    
        //落地偵測    
        isGround = false;//初始化
        Collider2D[] coli = Physics2D.OverlapCircleAll(transform.position, CheckRadius);
        foreach (var count in coli)
        {
            if (count.gameObject.tag == "Ground")
            {
                isGround = true;
                //print(count.gameObject.tag);
            }
         }
        
        
        //左右移動
        float movementH = 0f;
        movementH = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        Vector3 newPos = new Vector3(transform.position.x + movementH, transform.position.y, transform.position.z);
        transform.position = newPos;

        
        //跳躍
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector2.up * jumpHeight);
           isGround = false;
            
        }
        //跳躍上升
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.velocity.y > 0)
              rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y * 0.5f);//緩緩上升
        }


    }
}
