using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{
    public float coolingTime = 2.0f; //冷卻時間
    private float currentTime = 0.0f;
    //public Image coolingImage; //冷卻UI圖片

    public float movespeed;  //移動速度

    public float CheckRadius;//檢測長度

    public float jumpHeight; //跳躍高度
    public float jumpAirForce;

    public bool isGround;
    private bool isMove;
    private bool isJump;
    private bool isJumpUp;
    public bool isShoot=false;
    private bool isRoll;

    public float detectDistance; 
    private Rigidbody2D rb;
       
    public LayerMask ColliderMask; //可碰撞LAYER
    public GameObject bullet; //子彈
    public GameObject gun; //發射的物件

    private bulletFly script;
    public GameObject target; //接收用 不用設

    private Animator ani;

    public float rollSpeed;// 滾動距離
    private int Rollingcount = 0; 
    public float rollingtime; // 滾動時間 先不要改 可能有BUG

    public bool inputLock=false; //鎖按鍵
    
    private GameObject PS; //子物件 playerSprite
    private SpriteRenderer PSSR; //player的圖片
    public bool facingRight; //判斷面向
    private GameObject TriggerCollider;

    private weaponSystem WS;
    public int WeaponStyle=1; //武器種類
    void Start()
    {
        currentTime = 0; //初始化現在時間
        rb = GetComponent<Rigidbody2D>();
        ColliderMask = LayerMask.GetMask("Collider");
        ani = GetComponent<Animator>();
        PS = gameObject.transform.GetChild(3).gameObject;
        PSSR = PS.GetComponent<SpriteRenderer>();// 取得playerSprite的 SpriteRenderer
        TriggerCollider = gameObject.transform.GetChild(2).gameObject;
        WS = GetComponent<weaponSystem>();
    }
    private void Update()
    {       
        if (Input.GetKeyDown(KeyCode.Space) && isGround && inputLock != true)
        {            
            isJump = true;
        }
        if (Input.GetAxis("Horizontal").ToString() != "" )
        {
            isMove = true;
            
        }
        if (Input.GetKeyUp(KeyCode.Space))  //跳躍上升
        {
            isJumpUp = true;
        }
        if (Input.GetKeyDown(KeyCode.X) && isGround == true) 
        {
            isRoll = true;
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            WS.CW();// 換武器
            WeaponStyle++;
            if (WeaponStyle == 3)
                WeaponStyle = 1;
        }        
        isShoot = false;        
        if (isMove && inputLock!=true) //移動
        {            
            Move();            
        }                      
        coolingShowing();//子彈冷卻
        //覺得我記錄冷卻的時間應該反過來 不然currentTime會越來越大w
        if (Input.GetKeyDown(KeyCode.Z) && currentTime >=coolingTime  && inputLock != true) //射擊
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

        if (isRoll) //滾
        {
            InvokeRepeating("Roll", 0, 0.01f);// 每o.o1秒執行一次
        }
        else
        {
            AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
            ani.SetBool("roll", false); 
        }
    }    
    void Roll() 
    {
        TriggerCollider.active = false;// 去掉碰撞(無敵)isRoll =true;        
        inputLock = true;
        ani.SetBool("roll", true);
        
        if (facingRight == true) { //向右滾
            Vector3 Rollto = new Vector3(transform.position.x + rollSpeed, transform.position.y, transform.position.z);
            transform.position = Rollto;
        }
        if (facingRight == false) { //向左滾
            Vector3 Rollto = new Vector3(transform.position.x - rollSpeed, transform.position.y, transform.position.z);
            transform.position = Rollto;
        }
        //翻滾是 每0.01秒執行一次 每次移動rollspeed距離 共移動rollingtime秒
        //動畫是跑 0.4秒 所以可能不要超過比較好
        Rollingcount++;
        if (Rollingcount >= rollingtime*100)
        {
            Rollingcount = 0;
            inputLock = false;
            isRoll = false;
            TriggerCollider.active = true; //無敵結束
            CancelInvoke("Roll");  //翻滾結束         
        }
        isRoll = false;
    }    
    void Move( )
    {
      
        float movementH = 0f;
        movementH = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        //rb.AddForce(Vector2.right * movespeed * movementH);
        Vector3 newPos = new Vector3(transform.position.x + movementH, transform.position.y, transform.position.z);
        transform.position = newPos;
        //print(movementH);
        if (movementH > 0)
        {
            facingRight = true; // 面向
            PSSR.flipX = false;
        }
        if(movementH < 0)
        {
            facingRight = false;
            PSSR.flipX = true;            
        }
    }
    void coolingShowing() 
    {
        if (currentTime < coolingTime)
        {
            currentTime += Time.deltaTime;
            //coolingImage.fillAmount = 1 - currentTime / coolingTime; //按時間比例計算出Fill Amount值
        }
    }
    public void OnBtnClickSkill()// 冷卻時間
    {        
        if (currentTime >= coolingTime)
        {           
            currentTime = 0.0f;
           // coolingImage.fillAmount = 1.0f;
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
            isShoot = true;              
            WS.Weapon(WeaponStyle);//依現在武器種類射擊
            OnBtnClickSkill(); //冷卻
        }
        script.target = null; //把target 重設成null 不然會一直鎖定到      
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        
        if (c.gameObject.name == "moving floor")
        {
           
            //var target = c.gameObject.transform;
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
  
