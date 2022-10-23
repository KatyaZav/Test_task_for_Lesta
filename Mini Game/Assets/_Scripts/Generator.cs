using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] AllElements = new GameObject[5];

    [SerializeField] GameObject rightBottom;
    [SerializeField] GameObject leftTopMax;

    [SerializeField] GameObject green_one;
    [SerializeField] GameObject blue_one;
    [SerializeField] GameObject grey_one;
    [SerializeField] GameObject orange_one;

    [SerializeField] GameObject nextStep;


    public void Start()
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
                else if (zero > 0 && UnityEngine.Random.Range(0, 5) > 3)
                {
                    zero--;
                }
                else if (orange > 0 && UnityEngine.Random.Range(0, 5) > 2)
                {
                    Generate(orange_one, i, j);
                    orange--;
                }
                else if (blue > 0 && UnityEngine.Random.Range(0, 5) > 2)
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
        var e = i * 5 + j;
        var _obj = AllElements[e].transform.position;

        //Debug.Log(i + " " + j + " " + pref +" "+e);

        var _object = Instantiate(pref,
                new Vector3(_obj.x, _obj.y, 0),
                Quaternion.identity);
        _object.name = string.Format("{0}{1}", i, j);

        Matrix.Create(i, j, _object);
    }


    private void DragObj(float x, float y, GameObject gm)
    {
        if (CheckBorders(x, y))
        {        
            DeleteSteps();
            Instantiate(nextStep, new Vector3(x, y, 0), Quaternion.identity);


            int cord = Int32.Parse(gm.name);
            int _x = cord / 10;
            int _y = cord % 10;

            //var choosedPos = GetIndex(x, y);

            if (x != -1)
            {
                Matrix.ChoosedCircle = new Index(_x, _y);//choosedPos;

                var steps = Matrix.GenerateSteps(_x, _y);//choosedPos.I, choosedPos.J);

                foreach (var step in steps)
                {
                    var e = step.I * 5 + step.J;
                    var pos = AllElements[e].transform.position;

                    Instantiate(nextStep, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                }
            }
            else Debug.LogWarning("Index not gets");
            
        }
    }

    private Index GetIndex(float x, float y)
    {        
        for (int i=0; i<25; i++)
        {
            if ((int)(AllElements[i].transform.position.x) == Mathf.Ceil(x) && (int)(AllElements[i].transform.position.y) == Mathf.Ceil(y)) 
            {
                var first = i / 5;
                return new Index(first, i - first * 5);
            }
        }
            /*for (int j=0; j<5; j++)
        {
            if(Matrix.obj[i,j]!= null)
                if ((int)(Matrix.obj[i,j].transform.position.x) == (int)x && (int)Matrix.obj[i, j].transform.position.y == (int)y)
                {
                        return new Index(i, j);
                }}*/
        

        return new Index(-1, -1);
    }

    private void DropObj(float x, float y)
    {
        var stepIndex = GetIndex(x, y);

        if (stepIndex.I != -1)
        {
            Debug.Log(stepIndex.I + " " + stepIndex.J);

            Matrix.obj[stepIndex.I, stepIndex.J] = Matrix.obj[Matrix.ChoosedCircle.I, Matrix.ChoosedCircle.J];
            Matrix.obj[Matrix.ChoosedCircle.I, Matrix.ChoosedCircle.J] = null;

            Matrix.ChoosedCircle = null;

            //var stepCoord = new Coordinate(stepIndex.I, stepIndex.J, leftTopMax, rightBottom);
            Matrix.obj[stepIndex.I, stepIndex.J].transform.position = new Vector3(x, y, 0);
        }
        else Debug.LogWarning("Step not in borders.");
        
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
            Debug.Log("X: " + Matrix.ChoosedCircle.I + " Y: " + Matrix.ChoosedCircle.J);
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

    public static void DeleteSteps()
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
        public int I;
        public int J;

        /// <summary>
        /// Position to Index
        /// </summary>
        public Index(float x, float y, GameObject lt, GameObject rb)
        {
            J = (int)((x / ((rb.transform.position.x - lt.transform.position.x) / 5) - 0.5f) - 1);
            //(int)Mathf.Ceil(((x - lt.transform.position.x) / ((rb.transform.position.x - lt.transform.position.x) / 5))) - 1;
            
            I = (int)((y / ((rb.transform.position.y - lt.transform.position.y) / 5) - 0.5f) - 1);
        //(int)Mathf.Ceil(((x - lt.transform.position.x) / ((rb.transform.position.x - lt.transform.position.x) / 5))) - 1;

        Debug.Log(I + " " + J);
        }

        public Index(int x, int y)
        {
            I = x;
            J = y;
        }
    }


