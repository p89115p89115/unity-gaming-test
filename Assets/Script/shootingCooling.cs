using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shootingCooling : MonoBehaviour
{

    public float coolingTime = 2.0f; //冷卻時間
    private float currentTime = 0.0f; 
    public Image coolingImage;
    void Start()
    {
        currentTime = coolingTime;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

   

}
