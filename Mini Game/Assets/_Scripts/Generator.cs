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
       var e = Instantiate(pref,
                (new Vector3(new Coordinate(i, j, leftTopMax, rightBottom).X, new Coordinate(i, j, leftTopMax, rightBottom).Y)),
                Quaternion.identity);

        Matrix.Create(i, j, e);
    }

    private void DragObj(float x, float y)
    {
        if (CheckBorders(x, y))
        {
            DeleteSteps();

            var e = new Index(x, y, leftTopMax, rightBottom);
            var steps = Matrix.GenerateSteps(e.X, e.Y);

            foreach(var step in steps)
            {
                var pos = new Coordinate(step.X, step.Y, leftTopMax, rightBottom);

                Instantiate(nextStep, new Vector3(pos.X, pos.Y, 0), Quaternion.identity);
                Debug.Log(step.ToString());
            }
        }
    }

    private static void DeleteSteps()
    {
        var steps = GameObject.FindGameObjectsWithTag("Step");

        foreach (var step in steps)
            Destroy(step);
    }

    public bool CheckBorders(float x, float y)
    {
        if (leftTopMax.transform.position.x - 0.2f <= x && x <= 0.8f + rightBottom.transform.position.x &&
            rightBottom.transform.position.y - 0.2f <= y && y <= 0.8f + leftTopMax.transform.position.y)
            return true;

        Debug.LogWarning("Circle out of borders!");
        return false;
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
            X = lt.transform.position.x + 0.2f + (rb.transform.position.x - lt.transform.position.x) / 4 * y;
            Y = lt.transform.position.y + 0.2f + (rb.transform.position.y - lt.transform.position.y) / 4 * x;

            //Debug.Log(X + " " + Y);
        }

        public Coordinate(float x, float y)
        {
            X = x;
            Y = y;
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

        public Index(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

