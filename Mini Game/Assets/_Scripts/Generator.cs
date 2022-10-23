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
        Drop.Droping += DropObj;

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

        //DebugLog();
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
            Matrix.ChoosedCircle = e;

            var cords = new Coordinate(e.X, e.Y, leftTopMax, rightBottom);
            Instantiate(nextStep, new Vector3(cords.X, cords.Y, 0), Quaternion.identity);
                        
            var steps = Matrix.GenerateSteps(e.X, e.Y);

            foreach(var step in steps)
            {
                var pos = new Coordinate(step.X, step.Y, leftTopMax, rightBottom);

                Instantiate(nextStep, new Vector3(pos.X, pos.Y, 0), Quaternion.identity);
            }
        }
    }

    private void DropObj(float x, float y)
    {
        var stepIndex = new Index(x, y, leftTopMax, rightBottom);

        Matrix.obj[stepIndex.X, stepIndex.Y] = Matrix.obj[Matrix.ChoosedCircle.X, Matrix.ChoosedCircle.Y];
        Matrix.obj[Matrix.ChoosedCircle.X, Matrix.ChoosedCircle.Y] = null;

        Matrix.ChoosedCircle = null;

        var stepCoord = new Coordinate(stepIndex.X, stepIndex.Y, leftTopMax, rightBottom);
        Matrix.obj[stepIndex.X, stepIndex.Y].transform.position = new Vector3(stepCoord.X, stepCoord.Y, 0);

        DeleteSteps();
        DebugLog();
    }

    public static void DebugLog()
    {
        Debug.Log("___________________");
        Debug.Log(string.Format("{0} {1} {2} {3} {4}",
            GetName(Matrix.obj[0,0]), GetName(Matrix.obj[0, 1]), GetName(Matrix.obj[0, 2]), GetName(Matrix.obj[0, 3]), GetName(Matrix.obj[0, 4])));

        Debug.Log(string.Format("{0} {1} {2} {3} {4}",
            GetName(Matrix.obj[1, 0]), GetName(Matrix.obj[1, 1]), GetName(Matrix.obj[1, 2]), GetName(Matrix.obj[1, 3]), GetName(Matrix.obj[1, 4])));

        Debug.Log(string.Format("{0} {1} {2} {3} {4}",
           GetName(Matrix.obj[2, 0]), GetName(Matrix.obj[2, 1]), GetName(Matrix.obj[2, 2]), GetName(Matrix.obj[2, 3]), GetName(Matrix.obj[2, 4])));

        Debug.Log(string.Format("{0} {1} {2} {3} {4}",
           GetName(Matrix.obj[3, 0]), GetName(Matrix.obj[3, 1]), GetName(Matrix.obj[3, 2]), GetName(Matrix.obj[3, 3]), GetName(Matrix.obj[3, 4])));

        Debug.Log(string.Format("{0} {1} {2} {3} {4}",
           GetName(Matrix.obj[4, 0]), GetName(Matrix.obj[4, 1]), GetName(Matrix.obj[4, 2]), GetName(Matrix.obj[4, 3]), GetName(Matrix.obj[4, 4])));

        if (Matrix.ChoosedCircle != null)
            Debug.Log("X: " + Matrix.ChoosedCircle.X + " Y: " + Matrix.ChoosedCircle.Y);
        else Debug.Log("null");
        Debug.Log("___________________");

        string GetName(GameObject obj)
        {
            if (obj == null)
                return "null";

            if (obj.ToString() == "Green_Circle(Clone) (UnityEngine.GameObject)")
                return "green";

            if (obj.ToString() == "Grey_Circle Variant(Clone) (UnityEngine.GameObject)")
                return "grey";

            if (obj.ToString() == "Blue_Circle Variant(Clone) (UnityEngine.GameObject)")
                return "blue";

            if (obj.ToString() == "Orange_Circle Variant(Clone) (UnityEngine.GameObject)")
                return "orange";

            return "null";

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
        public Coordinate(int i, int j, GameObject lt, GameObject rb)
        {
            
            X = lt.transform.position.x + ((rb.transform.position.x - lt.transform.position.x) / 5) * (j+0.5f);
            Y = lt.transform.position.y + ((rb.transform.position.y - lt.transform.position.y) / 5) * (i+0.5f);
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
            
            X = Mathf.Abs((int)((lt.transform.position.y - y ) / ((lt.transform.position.y - 1f - rb.transform.position.y) / 4)));
        }

        public Index(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

