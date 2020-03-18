using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathText : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private float heathPoint;
    public Text text;
    void Start()
    {
        
       // heathPoint=player.GetComponent<physicalQuality>().heathPoint;
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        heathPoint = player.GetComponent<physicalQuality>().heathPoint;
        text.text = "HP："+heathPoint;
    }
}
