using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private void Start()
    {
        //DontDestroyOnLoad(this);
    }
    public void Reload()
    {
        foreach(var e in Matrix.obj)
        {
            Destroy(e);
        }

        Matrix.obj = new GameObject[5, 5];
        GameObject.FindGameObjectWithTag("Generator").GetComponent<Generator>().Start();
        
       // SceneManager.LoadScene(0);
    }
}
