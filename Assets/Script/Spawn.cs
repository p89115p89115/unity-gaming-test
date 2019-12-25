using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject Object; //愈生出的物件
    public Transform spawnPoint; //生出的地方
    private float heathpoint;
    physicalQuality Heath;

    void Start()
    {
        Heath = GetComponent<physicalQuality>(); //獲取生上的heath script      
    }


    void Update()
    {
        heathpoint = Heath.heathPoint;
        if (heathpoint == 0) //血量歸 0 就生
        Instantiate(Object, spawnPoint.transform.position, Quaternion.identity);
    }
}
