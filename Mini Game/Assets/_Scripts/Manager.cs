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
        SceneManager.LoadScene(0);
    }
}
