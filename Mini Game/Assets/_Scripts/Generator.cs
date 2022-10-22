using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] GameObject rightBottom;
    [SerializeField] GameObject leftTopMax;

    [SerializeField] GameObject green_one;
    [SerializeField] GameObject blue_one;
    [SerializeField] GameObject grey_one;
    [SerializeField] GameObject orange_one;

    [SerializeField] GameObject nextStep;

    private void Start()
    {
        Drag.Draging += DragObj;

        var zero = 4;
        var green = 5;
        var orange = 5;
        var blue = 5;


        for (var i=0; i<5; i++)
            for (var j=0; j<5; j++)
        {
                if ((j == 1 && i == 0) || (j == 3 && i == 0) ||
                    (j == 1 && i == 2) || (j == 3 && i == 2) ||
                     (j == 1 && i == 4) || (j == 3 && i == 4))
                    Generate(grey_one, i, j);
                else if (zero > 0 && Random.Range(0, 5) > 3)
                {
                    zero--;
                }
                else if (orange > 0 && Random.Range(0, 5) > 2)
                {
                    Generate(orange_one, i, j);
                    orange--;
                }
                else if (blue > 0 && Random.Range(0, 5) > 2)
                {
                    Generate(blue_one, i, j);
                    blue--;
                }
                else if (green > 0)
                {
                    Generate(green_one, i, j);
                    green--;
                }
                else
                {
                    if (blue > 0)
                    {
                        Generate(blue_one, i, j);
                        blue--;
                    }
                    else if (zero>0)
                    {
                        zero--;
                    }
                    else
                    {
                        Generate(orange_one, i, j);
                        orange--;
                    }
                }
            }


    }

    private void Generate(GameObject pref, int i, int j)
    {
        Instantiate(pref,
                (new Vector3(
                    leftTopMax.transform.position.x + 0.2f + ((rightBottom.transform.position.x - leftTopMax.transform.position.x) / 4 * j),
                    leftTopMax.transform.position.y + 0.2f + ((rightBottom.transform.position.y - leftTopMax.transform.position.y) / 4 * i))),
                Quaternion.identity);

        Matrix.Create(i, j, pref);
    }

    private void DragObj(float x, float y)
    {
        if (CheckBorders(x, y))
        {           
            var mas = FindCoordinate(x, y);
        }
    }

    public bool CheckBorders(float x, float y)
    {
        if (leftTopMax.transform.position.x <= x && x <= rightBottom.transform.position.x &&
            rightBottom.transform.position.y <= y && y <= leftTopMax.transform.position.y)
            return true;

        Debug.LogWarning("Circle out of borders!");
        return false;
    }

    public static int[] FindCoordinate(float x, float y)
    {
        Debug.Log("Find cord");
        return new int[] {0, 1};                   
    }
}
