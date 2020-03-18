using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edgeDetect : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private Vector3 EdgeL,EdgeR;
    private GameObject floor; //身處的平台
    private bool rearchPoint= true;
    void Start()
    {
       
       // EdgeL = new Vector3(floor.transform.position.x-floor.transform.localScale.x / 2, floor.transform.position.y + floor.transform.localScale.y / 2);
       // EdgeR = new Vector3(floor.transform.position.x+floor.transform.localScale.x / 2, floor.transform.position.y + floor.transform.localScale.y / 2);

    }
    void FixedUpdate()
    {
        EdgeL = new Vector3(floor.transform.position.x - floor.transform.localScale.x / 2, floor.transform.position.y + floor.transform.localScale.y / 2);
        EdgeR = new Vector3(floor.transform.position.x + floor.transform.localScale.x / 2, floor.transform.position.y + floor.transform.localScale.y / 2);

        if (rearchPoint == true)
        {         
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (rearchPoint == false)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
       
        if (transform.position.x <= EdgeL.x)//到達左點
        { 
            rearchPoint = false;
        }
            
        if (transform.position.x >= EdgeR.x)//到達右點
        {
            rearchPoint = true;  
        }      
        Debug.DrawLine(EdgeL,EdgeR,Color.red);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        floor = collision.gameObject;
    }
}
