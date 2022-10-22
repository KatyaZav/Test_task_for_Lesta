using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drag : MonoBehaviour
{
    public static Action<float, float> Draging; 

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        var position = Input.mousePosition;
        position.z = Camera.main.nearClipPlane + 1;
        var Pose = Camera.main.ScreenToWorldPoint(position); 

        Draging?.Invoke(Pose.x, Pose.y);
    }
}
