using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameObject spawnspot;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clickReset() 
    {
        //GameObject player = GameObject.Find("Player");
     player.GetComponent<physicalQuality>().heathPoint=100;
        player.transform.position = spawnspot.transform.position;
        player.active = true;
        
        

    }
}
