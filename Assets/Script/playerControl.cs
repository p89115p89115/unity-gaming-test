using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{
    public float coolingTime = 2.0f; //冷卻時間
    private float currentTime = 0.0f;
    public Image coolingImage; //冷卻UI圖片

    public float movespeed;  //移動速度
    public bool isGround;
    public float CheckRadius;//檢測長度

    public float jumpHeight; //跳躍高度
    public float jumpAirForce;

    private bool isMove;
    private bool isJump;
    private bool isJumpUp;

    public float detectDistance; 
    private Rigidbody2D rb;
       
    public LayerMask ColliderMask; //可碰撞LAYER
    public GameObject bullet; //子彈
    public GameObject gun; //發射的物件

    private bulletFly script;
    public GameObject target; //接收用 不用設

    private Animator ani;
    void Start()
    {
        currentTime = coolingTime; //初始化現在時間
        rb = GetComponent<Rigidbody2D>();
        ColliderMask = LayerMask.GetMask("Collider");
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            print("JUMP");
            isJump = true;
        }
        if (Input.GetAxis("Horizontal").ToString() != "")
        {
            isMove = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))  //跳躍上升
        {
            isJumpUp = true;
        }
    }
    void FixedUpdate()
    {
        if (isMove) //移動
        {            
            Move(); 
        }
                      
        coolingShowing();

        if (Input.GetKeyDown(KeyCode.Z) && coolingImage.fillAmount == 0) //射擊
        {
            detectEnemy();            
        }
        if (isJump) //跳躍
        {
            Jump();
        }
        if (isJumpUp)
        {
            JumpUp();
        }
        groundDetect(); //落地偵測    

        if (Input.GetKeyDown(KeyCode.X)) 
        {
            ani.SetBool("punch",true);
        }
        else { 
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        ani.SetBool("punch", false);
        }

    }
    void Move( )
    {
        float movementH = 0f;
        movementH = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        //rb.AddForce(Vector2.right * movespeed * movementH);
        Vector3 newPos = new Vector3(transform.position.x + movementH, transform.position.y, transform.position.z);
        transform.position = newPos;

    }
    void coolingShowing() 
    {
        if (currentTime < coolingTime)
        {
            currentTime += Time.deltaTime;
            coolingImage.fillAmount = 1 - currentTime / coolingTime; //按時間比例計算出Fill Amount值
        }
    }
    public void OnBtnClickSkill()// 冷卻時間
    {        
        if (currentTime >= coolingTime)
        {
            currentTime = 0.0f;
            coolingImage.fillAmount = 1.0f;
        }
    }
    void Jump() 
    {        
        rb.AddForce(Vector2.up * jumpHeight);      
        isJump = false;
    }
    void JumpUp() 
    {
        if (rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpAirForce);//緩緩上升
        isJumpUp = false;
    } 
    void groundDetect() 
    {
        isGround = false;//初始化       
        RaycastHit2D []hit = Physics2D.RaycastAll(transform.position,Vector2.down, CheckRadius);        
        foreach (var count in hit)
        {
            if (count.collider.gameObject.tag == "Ground")
            {
                isGround = true;
            }
        }
       
    }

    void OnDrawGizmos() //偵測範圍
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }

    void detectEnemy() 
    {       
        Collider2D[] ColliderDetect = Physics2D.OverlapCircleAll(transform.position, detectDistance, ColliderMask);       
        if (ColliderDetect != null)
        {            
            // 陣列檢查  元素命名enemy

            foreach (var enemy in ColliderDetect)
            {
           
                // 若碰撞物件tag為enemy
                if (enemy.tag == "Enemy")
                {
                    
                    // 發射射線  起點：本體   方向：敵人-本體
                    RaycastHit2D hit = Physics2D.Raycast(this.transform.position, (enemy.transform.position - this.transform.position), 10f, ColliderMask);
                    // 若碰撞結果為 tag == enemy
                    if (hit.collider.tag == "Enemy")
                    {
                        //把物件傳給子彈 好讓他有敵人的座標
                        script = bullet.transform.GetComponent<bulletFly>();
                        script.target = hit.collider.gameObject;     
                        Debug.DrawLine(this.transform.position, hit.transform.position, Color.white);     
                    }
                }
            }
        }

        //如果範圍內有敵人才射
        if (script.target != null)
        {
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            OnBtnClickSkill();
        }
        script.target = null; //把target 重設成null 不然會一直鎖定到

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        
        if (c.gameObject.name == "moving floor")
        {
           
            var target = c.gameObject.transform;
            // target.SetParent(this.transform);
            this.transform.parent = c.transform;
        }
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.name == "moving floor")
        {
            print("OnCollisionEnter2D");
          //  var target = c.gameObject.transform;
            //var original = target.GetComponent<transformState>().OriginalParent;
            // target.SetParent(original);
            this.transform.parent = null;
        }
    }

}
  
