using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
