using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
//需導入Editor
public class DetectEmeny : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetEnemy();
    }

    public GameObject enemy;
    public float radius = 1;
    public void GetEnemy()
    {



        //球形射線檢測,得到半徑radius米範圍內所有的物件
        Collider2D[] cols = Physics2D.OverlapCircleAll(this.transform.position, 3);

        //判斷檢測到的物件中有沒有Enemy
        if (cols.Length > 0)
            for (int i = 0; i < cols.Length; i++)
                if (cols[i].tag.Equals("Enemy"))
                {
                    print("Enemy's name is " + cols[i].name);
                   
                }
    
       
    }
    public float distanceToMe;           //智能体到目标的距离
    public GameObject me;                //目标角色
    public float isSeekDistance = 10.0f;  //可靠近范围
    public int state;  


    void Idle()
    {
        //获取两者间的距离
        distanceToMe = Vector3.Distance(me.transform.position, this.transform.position);
        if (distanceToMe > isSeekDistance) //大于可靠近范围，进入空闲状态
        {
            print("大於");
        }
        else
        {
            print("小於");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //要兩邊都剛體?
        if (col.gameObject.tag == "Enemy")
        {
            print("Collision Enemy!!!!!");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            print("Trigger Enemy!!!!!");

           

        }
    }

}





