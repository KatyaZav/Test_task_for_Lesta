using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Matrix
{
    public static Index ChoosedCircle;

    public static GameObject[,] obj = new GameObject[5, 5];

    public static void Create(int i, int j, GameObject prefab)
    {
        obj[i, j] = prefab;
    }

    public static List<Index> GenerateSteps(int i, int j)
    {
        //Debug.Log(i + " " + j);
        List<Index> ans = new List<Index>();

        if (i >= 1 && obj[i - 1, j] == null)
            ans.Add(new Index(i-1, j));

        if (j >= 1 && obj[i, j-1] == null)
            ans.Add(new Index(i, j-1));

        if (i <= 3 && obj[i + 1, j] == null)
            ans.Add(new Index(i + 1, j));

        if (j <= 3 && obj[i, j+1] == null)
            ans.Add(new Index(i, j+1));

        return ans;
    }

    public static Action WinNow;
    public static void checkIsWin()
    {      
        bool Blue = false, Orange=false, Green = false;
        try
        {
            
            if ((obj[0, 0].tag == "blue" && obj[1, 0].tag == "blue" && obj[2, 0].tag == "blue" && obj[3, 0].tag == "blue" && obj[4, 0].tag == "blue"))
                Blue = true;

            if (obj[0, 2].tag == "green" && obj[1, 2].tag == "green" && obj[2, 2].tag == "green" && obj[3, 2].tag == "green" && obj[4, 2].tag == "green")
                Green = true;
            if (obj[0, 4].tag == "orange" && obj[1, 4].tag == "orange" && obj[2, 4].tag == "orange" && obj[3, 4].tag == "orange" && obj[4, 4].tag == "orange")
                Orange = true;

            if (Orange && Green && Blue)
            {
                WinNow?.Invoke();
                Win();
                Manager.Reload();
            }
            
        }
        catch (System.Exception e)  { }

        Debug.Log(Blue + " " + Orange + " " + Green);
    }

    private static void Win()
    {        
        Debug.Log("Win!");
    }
}
