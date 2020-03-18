using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class weaponSystem : MonoBehaviour
{
    public int WeaponStyle;
   // public GameObject Gun;
    private GameObject player;
    private playerControl PC;
    public GameObject pic;
    private SpriteRenderer SR; //換彈圖示

    public GameObject bullet; //子彈
    public GameObject gun; //發射的物件
    public GameObject explosion; //shotgun
    public int Meg; // 彈夾數量
    private int currnetMeg; //現在彈夾數量

    public Image coolingImage; //冷卻UI圖片
    public float coolingTime = 2.0f; //冷卻時間
    private float currentTime = 0.0f;
    public Text MegText; //彈夾顯示text

    float alpha = 1; //換彈圖示透明度
    float Adecrese = 0.1f; //每次減少的透明度
    int count = 10;
    void Start()
    {
        coolingImage = GameObject.Find("cooingImage").GetComponent<Image>();
        MegText = GameObject.Find("MegText").GetComponent<Text>();
        currnetMeg = Meg;
        player = GameObject.Find("Player");
        PC = player.transform.GetComponent<playerControl>(); 
        currentTime = coolingTime; //初始化現在時間
       // pic = gameObject.transform.GetChild(5).gameObject;
        SR = pic.transform.GetComponent<SpriteRenderer>();
        SR.material.color = new Color(1, 1, 1, 0);       
    }
    void Update()
    {          
        coolingShowing(); // 換彈夾
        MegText.text = currnetMeg+" / "+Meg; //殘彈顯示
    }
    public void CW() 
    {
        InvokeRepeating("changeWeapon", 0, 0.1f); //換彈圖示慢慢消失
    }
    void changeWeapon()// 換武器
    {
        alpha -= Adecrese;
        count--;      
        SR.material.color = new Color(1, 1, 1, alpha); 
        if (count <= 0)
        { 
            this.CancelInvoke();
            count = 10;
            alpha = 1f;
        }
    }
   public void Weapon(int WeaponStyle)
    {
        if(currnetMeg <= 0)
        {
            OnBtnClickSkill(); //冷卻
            currnetMeg = Meg; //reload
        }
        switch (WeaponStyle)
        {
          
            case 1:
                //發射子彈
                if (currnetMeg != 0 && coolingImage.fillAmount == 0) 
                {
                    currnetMeg--;
                    Instantiate(bullet, gun.transform.position, gun.transform.rotation);                  
                }                
                break;
            case 2:
                //向前散彈
                explosion.GetComponent<SpriteRenderer>().flipY = false;
                if (GameObject.Find("Player").GetComponent<playerControl>().facingRight)
                {
                    Instantiate(explosion, new Vector3(PC.transform.position.x + 3, transform.position.y, transform.position.z), explosion.transform.rotation);
                }
                else
                {
                    explosion.GetComponent<SpriteRenderer>().flipY = true;
                    Instantiate(explosion, new Vector3(PC.transform.position.x - 3, transform.position.y, transform.position.z+180), explosion.transform.rotation);
                   

                }
                break;            
        }
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
}


