using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public string hurtingObject;
    private GameObject HO;
    public string colliderTag;
    void Start()
    {      
        HO = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == colliderTag)
        {
            //enemyV = collision.transform.position;

            physicalQuality PQ;
            physicalQuality HP;
            HP = HO.GetComponent<physicalQuality>();
            PQ = collision.gameObject.GetComponent<physicalQuality>();
            HP.heathPoint -= PQ.attackPoint; //扣血            
            HP.isHit = true;
            
        }
    }
}
