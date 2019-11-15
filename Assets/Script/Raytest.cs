using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Raytest : MonoBehaviour
{
  

    //射線本身不能加collider 不然第一個射到的會是自己
    //不過也可以用其他方法來避免

    void Update()
    {
        Vector3 fwd = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    //設定一個射線的方向 y+1讓他往前射
        UnityEngine.Ray2D ray = new UnityEngine.Ray2D(transform.position, fwd * 100);
        //宣告一條ray 沒用到
        
        Debug.DrawLine(transform.position,  fwd , Color.red);
        //畫一條線 方便看

        //用Raycast 只能記錄射到的第一個
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, fwd);
        //射一個射線並記錄射中哪些目標 這個好像是無窮距離
        

        if (hit.Length > 0)
            for (int i = 0; i < hit.Length; i++)
         
            {
                print(hit[i].collider.gameObject);

            }

        
    }
}
