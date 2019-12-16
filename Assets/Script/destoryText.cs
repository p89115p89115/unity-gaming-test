using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryText : MonoBehaviour
{
    // Start is called before the first frame update
    public float DestroyTime;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
