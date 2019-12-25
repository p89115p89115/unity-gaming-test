using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    public Text Text; // COMBO的TEXT
    public Image timeBar; // combo時間圖片

    private int comboNum;
    public float comboTime=3;// Combo可持續的秒數
    private float currentTime;
    void Update()
    {
        Timer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            comboTimer();
        }
    }
    void Timer() 
    {
        currentTime -= Time.deltaTime; //時間遞減
        timeBar.fillAmount =  currentTime / comboTime; // MASK隨時間調整  
        if (currentTime < 0) 
        {
            comboNum = 0; 
            Text.text = null; //關掉combo的TEXT
        }
    }
    void comboTimer()
    {
    
        currentTime = comboTime;
        comboNum++;
        Text.text =  comboNum+" COMBO!!";
        

    }
}
