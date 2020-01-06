using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadScene : MonoBehaviour
{
    public int Scene;
    public GameObject DD; //不要消滅的物件
    // Start is called before the first frame update
    void Start()
    {
       // SceneManager.LoadScene(Scene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(Scene);
            DontDestroyOnLoad(DD);
        }
    }
}
