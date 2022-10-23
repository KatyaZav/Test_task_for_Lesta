using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]GameObject menu;
    private void Start()
    {
        //menu.SetActive(true);

        //Matrix.WinNow += MakeWin;
    }
    public static void Reload()
    {
        //menu.SetActive(false);
        
        Generator.DeleteSteps();
        foreach(var e in Matrix.obj)
        {
            Destroy(e);
        }

        Matrix.obj = new GameObject[5, 5];
        GameObject.FindGameObjectWithTag("Generator").GetComponent<Generator>().Start();

        // SceneManager.LoadScene(0);
    }

    void MakeWin()
    {
        menu.SetActive(true);
    }

}
