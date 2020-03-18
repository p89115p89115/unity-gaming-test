using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //streamWriter需要
using Cinemachine;


public class SaveLoad : MonoBehaviour
{
    private float heathPoint;
    private Vector2 playerPosition;
    public GameObject Hero; // 主角
    public GameObject Cam;
    public void SaveClick() //按下存檔紐
    {
        heathPoint = GameObject.Find("Player").GetComponent<physicalQuality>().heathPoint;
        playerPosition = GameObject.Find("Player").transform.position;
        SaveByJson();
    }
    public void LoadClick() //按下讀取紐
    {
        LoadByJson();        
    }
    
    [System.Serializable]
    public class Save
    {
        //怪物位置
        //public List<int> livingTargetPositions = new List<int>();
        //怪物型別
        //public List<int> livingMonsterTypes = new List<int>();
        //得分情況
        public float HP; //主角血量
        public Vector2 PP ; //主角位置
    }
    
    
    private void SaveByJson()
    {
        Save save = new Save();
        string filePath = Application.dataPath + "/StreamingAssets" + "/save.json";
        //利用JsonMapper將save物件轉換為Json格式的字串 (要用JsonUtility dont know why
        
        save.HP = heathPoint;
        save.PP = playerPosition;

        string saveJsonStr = JsonUtility.ToJson(save);
        //將這個字串寫入到檔案中
        //建立一個StreamWriter，並將字串寫入檔案中
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        
        //關閉StreamWriter
        sw.Close();
        print("儲存成功");
        print("儲存在： "+ filePath);
        //TODO: 提示儲存成功
    }

    private void LoadByJson()
    {
        string filePath = Application.dataPath + "/StreamingAssets" + "/save.json";
        if (File.Exists(filePath))
        {
            //建立一個StreamReader，用來讀取流
            StreamReader sr = new StreamReader(filePath);
            //將讀取到的流賦值給jsonStr
            string jsonStr = sr.ReadToEnd();
            //關閉
            sr.Close();
            Save loaddata = new Save();
            //將字串jsonStr轉換為Save物件
            loaddata = JsonUtility.FromJson<Save>(jsonStr);

           // Hero = GameObject.Find("Player");
           // Instantiate(Hero,loaddata.PP, Quaternion.identity); //生成主角
           // GameObject.Find("Player(Clone)").transform.name = "Player"; //換成Player 比較不會有bug
            //Hero = GameObject.Find("Player");
            Hero.GetComponent<physicalQuality>().heathPoint = loaddata.HP; //讀取血量
            Hero.transform.position = loaddata.PP;
            Hero.active = true;
            //Destroy(GameObject.Find("Main Camera"));
            //Instantiate(Cam, new Vector3(0,0,0), Quaternion.identity);
            //GameObject.Find("Main Camera(Clone)").transform.name = "Main Camera";
            print("讀取成功");
            //SetGame(save);//將儲存的資訊類初始遊戲           
        }
        else
        {
            print("讀取失敗");
        }
    }
}
