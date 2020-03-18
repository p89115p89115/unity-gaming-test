using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeTest : MonoBehaviour
{


    public Vector3 positionShake;//震動幅度
    public Vector3 angleShake;   //震動角度
    public float cycleTime = 0.2f;//震動週期
    public int cycleCount = 6;    //震動次數
    public bool fixShake = false; //為真時每次幅度相同，反之則遞減
    public bool unscaleTime = false;//不考慮縮放時間
    public bool bothDir = true;//雙向震動
    public float fCycleCount = 2;//設定此引數，以此震動次數為主
    public bool autoDisable = true;//自動disbale
    public bool heartshake = false; //是否心型轉



    float currentTime;
    int curCycle;
    Vector3 curPositonShake;
    Vector3 curAngleShake;
    float curFovShake;
    Vector3 startPosition;
    Vector3 startAngles;
    Transform myTransform;

    void OnEnable()
    {
        currentTime = 0f;
        curCycle = 0; //現在震到第幾次
        curPositonShake = positionShake;
        curAngleShake = angleShake;
        myTransform = transform; //相機transform
        startPosition = myTransform.localPosition;
        startAngles = myTransform.localEulerAngles;
        if (fCycleCount > 0) //不知道為甚麼要這樣做
            cycleCount = Mathf.RoundToInt(fCycleCount);
    }

    void OnDisable()
    {
        myTransform.localPosition = startPosition;
        myTransform.localEulerAngles = startAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            print("quake");
            Restart();
        }
        if (fCycleCount > 0)
            cycleCount = Mathf.RoundToInt(fCycleCount);//
        if (curCycle >= cycleCount)
        {
            if (autoDisable)
                enabled = false;
            return;
        }

        float deltaTime = unscaleTime ? Time.unscaledDeltaTime : Time.deltaTime; //是否為縮放時間
        currentTime += deltaTime;
        while (currentTime >= cycleTime) //鏡頭移動
        {
            currentTime -= cycleTime;
            curCycle++;
            if (curCycle >= cycleCount)
            {
                myTransform.localPosition = startPosition;
                myTransform.localEulerAngles = startAngles;
                return;
            }

            if (!fixShake) //震動幅度遞減
            {
                if (positionShake != Vector3.zero)
                    curPositonShake = (cycleCount - curCycle) * positionShake / cycleCount;
                if (angleShake != Vector3.zero)
                    curAngleShake = (cycleCount - curCycle) * angleShake / cycleCount;
            }
        }

        if (curCycle < cycleCount) 
        {
            //用弧度去算 
            //弧度類似圓周的概念，是長度單位，1個圓周是2π(平常圓周是2πr，他已經假設r=1) 
            float theta = (bothDir ? 2 : 1) * Mathf.PI * currentTime / cycleTime;
            float offsetScale = Mathf.Sin(theta);
            if (positionShake != Vector3.zero && heartshake==true)
            {    
                //心型轉 
                float aaa = 1 - offsetScale;
                Vector3 offsetS = new Vector3(aaa * Mathf.Cos(theta), aaa * Mathf.Sin(theta), 0f);
                myTransform.localPosition = startPosition +new Vector3(curPositonShake.x * offsetS.x , curPositonShake.y * offsetS.y,0f);                     
            }
            if(positionShake != Vector3.zero && heartshake==false)//普通震
                myTransform.localPosition = startPosition + curPositonShake * offsetScale;
            if (angleShake != Vector3.zero)//角度震
                myTransform.localEulerAngles = startAngles + curAngleShake * offsetScale;
        }
    }
    //重置
    public void Restart()
    {
        if (enabled)
        {
            currentTime = 0f;
            curCycle = 0;
            curPositonShake = positionShake;
            curAngleShake = angleShake;
            myTransform.localPosition = startPosition;
            myTransform.localEulerAngles = startAngles;
            if (fCycleCount > 0)
                cycleCount = Mathf.RoundToInt(fCycleCount);
        }
        else
            enabled = true;
    }
}