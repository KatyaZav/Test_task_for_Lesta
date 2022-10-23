using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drag : MonoBehaviour
{
    public static Action<float, float> Draging; 

    private void OnMouseDown()
    {
        /*var position = Input.mousePosition;
        position.z = Camera.main.nearClipPlane + 1;
        var Pose = Camera.main.ScreenToWorldPoint(position);*/

        var Pose = transform.position;
        Debug.Log(Pose.x + " " + Pose.y);

        Draging?.Invoke(Pose.x, Pose.y);
    }
}
