using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Matrix
{
    public static GameObject[,] obj = new GameObject[5, 5];

    public static void Create(int i, int j, GameObject prefab)
    {
        obj[i, j] = prefab;
    }
}
