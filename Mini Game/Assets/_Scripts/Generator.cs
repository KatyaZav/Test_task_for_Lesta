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


        for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
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
                    else if (zero > 0)
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
        if (leftTopMax.transform.position.x - 0.2f <= x && x <= 0.8f + rightBottom.transform.position.x &&
            rightBottom.transform.position.y - 0.2f <= y && y <= 0.8f + leftTopMax.transform.position.y)
            return true;

        Debug.LogWarning("Circle out of borders!");
        return false;
    }

    public int[] FindCoordinate(float x, float y)
    {
        Debug.Log("Find cord");
        //var _x = x/(leftTopMax.transform.position.x + 0.2f +
        //     (rightBottom.transform.position.x - leftTopMax.transform.position.x)/4)-1;

        var e = new Index(x, y, leftTopMax, rightBottom);
        Debug.Log(e.X + " " + e.Y);

        //Instantiate(grey_one, new Vector3(e.X, e.Y, 0), Quaternion.identity);
        //Debug.Log(x);
        return new int[] { 0, 1 };
    } 
}

    public class Coordinate
    {
        public float X;
        public float Y;

        /// <summary>
        /// Index to Position
        /// </summary>
        public Coordinate(int x, int y, GameObject lt, GameObject rb)
        {
            X = lt.transform.position.x + 0.2f + (rb.transform.position.x - lt.transform.position.x) / 4 * x;
            Y = lt.transform.position.y + 0.2f + (rb.transform.position.y - lt.transform.position.y) / 4 * y;

            //Debug.Log(X + " " + Y);
        }
    }

    public class Index
    {
        public int X;
        public int Y;

        /// <summary>
        /// Position to Index
        /// </summary>
        public Index(float x, float y, GameObject lt, GameObject rb)
        {
            Y = Mathf.Abs((int)((lt.transform.position.x - x)/((rb.transform.position.x - 0.5f - lt.transform.position.x) / 4)));
            
            X = Mathf.Abs((int)((lt.transform.position.y - y) / ((lt.transform.position.y - 1f - rb.transform.position.y) / 4)));
        }
    }

