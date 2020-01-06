using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onload : MonoBehaviour
{
    private GameObject ob;
    // Start is called before the first frame update
    void Start()
    {
        ob = gameObject.transform.GetChild(0).gameObject; ;
        ob.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
