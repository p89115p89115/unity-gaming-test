using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talk : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Cantalk=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Cantalk = true;
        if (Input.GetKeyDown(KeyCode.Z) && Cantalk == true) 
        {

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Cantalk = false;
    }
}
