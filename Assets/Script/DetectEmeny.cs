using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
//需導入Editor
public class DetectEmeny : MonoBehaviour
{

    void Start()
    {

    }
   

    void Update()
    {
      
    }



    public Transform m_Transform;
    public float m_Radius; // 圆环的半径
    public float m_Theta; // 值越低圆环越平滑
    public Color m_Color; // 线框颜色
    void GetEnemy()
    {

        //球形射線檢測,得到半徑radius米範圍內所有的物件
        Collider2D[] cols = Physics2D.OverlapCircleAll(this.transform.position, m_Radius);

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

    void OnCollisionStay2D(Collision2D col)
    {
        //不能用collision 他一定要把鋼體設成dynamic才會觸發
        //要兩邊都剛體?
        if (col.gameObject.tag == "Enemy")
        {
            print("Collision Enemy!!!!!");
        }
    }
    //*******畫兩條線 綠線射往觸發到的物體 紅線是沒撞牆的才射********
    //所以我們要的是紅線的效果 可是總有出bug的地方w

    void OnTriggerStay2D(Collider2D hit)
    {
        //用Stay可以讓她一直觸發，應該是要這樣的
        //可是試用起來他只是多持續觸發一小段時間，不知道是不是我有用錯

        bool hitWall=false;//設布林來判斷有無射到牆
       
       
        Debug.DrawLine(transform.position, hit.transform.position, Color.green);//畫綠線

        RaycastHit2D[] target = Physics2D.RaycastAll(this.transform.position, hit.gameObject.transform.position);
        //宣告一條Ray 從自己到觸發的物件 並記在 target 裡面(會有多個

     
           
            //射線射往觸發的物件
        foreach (RaycastHit2D rrr in target) 
        {
            if (rrr.collider.gameObject.name == "Wall") 
            {
                hitWall = true;
                print("rrr.collider.gameObject.name"+ rrr.collider.gameObject.name);
                
            }
        }
        //對Ray 射到的物件 做判斷
        for (int i = 0; i < target.Length; i++)
        {

            if (target[i].collider.gameObject.name == "Wall" )
            {
               hitWall = true;
                // print("射到強");
                print("target[i].collider.gameObject.name  "+ target[i].collider.gameObject.name);
                

            }
            
            
        }
        if (hitWall != true)//沒射到牆的話才畫紅線
        {
            Debug.DrawLine(transform.position, hit.transform.position, Color.red);
        }
    }
       
 
 
    
    }

  







