using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatMonster : MonoBehaviour
{
    public GameObject Monster;//要生的怪
    public float repeatRate;//出怪頻率
    public int MonsterNum;//一次生成數量     
    public GameObject[] spot; 
    void Start()
    {
        InvokeRepeating("CreatMoneter", 1, repeatRate);//一秒後每隔repeatRate秒執行一次
    }

    public void CreatMoneter()
    {
        for (int spotCount = 0; spotCount <= spot.Length; spotCount++)
        {
            int count;
            count = Random.Range(0, MonsterNum);

            for (int i = 0; i < count; i++)
            {
                float x;
                x = Random.Range(-25, 25);
                float y = Random.Range(10, 0);
                Instantiate(Monster, spot[spotCount].transform.position, Quaternion.identity);
            }
        }
    }
}
