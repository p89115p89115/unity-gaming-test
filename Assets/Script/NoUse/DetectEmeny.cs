using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class DetectEmeny : MonoBehaviour
{
    //public Color detectColor ;
    public float detectDistance ;
    public GameObject bullet; //子彈
    public GameObject gun; //發射的物件

    private void Start() 
    {
    }

    private bulletFly script;
    public GameObject target;
    private void Update() {
        // 碰撞圖層
        LayerMask ColliderMask = LayerMask.GetMask("Collider");
        // 判斷碰撞面積、可碰撞物件
        Collider2D[] ColliderDetect  = Physics2D.OverlapCircleAll(transform.position,detectDistance,ColliderMask);

        // 若有碰撞到東西
        if (ColliderDetect != null){
            // 陣列檢查  元素命名enemy

            foreach(var enemy in ColliderDetect){
                // 若碰撞物件tag為enemy
            
                if (enemy.tag == "Enemy"){
                    // 發射射線  起點：本體   方向：敵人-本體
                    RaycastHit2D hit = Physics2D.Raycast(this.transform.position,(enemy.transform.position - this.transform.position) ,10f,ColliderMask);
                    // 若碰撞結果為 tag == enemy
                    if (hit.collider.tag == "Enemy"){

                        //把物件傳給子彈 好讓他有敵人的座標
                        script = bullet.transform.GetComponent<bulletFly>();
                        script.target =hit.collider.gameObject;
                        //target = hit.collider.gameObject;
                        
                        Debug.DrawLine(this.transform.position, hit.transform.position,Color.white);
                       // print(hit.collider.name);
                        
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //如果範圍內有敵人才射
            if (script.target != null)

                Instantiate(bullet, gun.transform.position, gun.transform.rotation);

        }
        script.target = null; //把target 重設成null 不然會一直鎖定到
    }




}