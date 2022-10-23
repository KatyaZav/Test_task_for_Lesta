using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drop : MonoBehaviour
{
    public static Action<float, float> Droping;
    private void OnMouseDown()
    {
        var position = Input.mousePosition;
        position.z = Camera.main.nearClipPlane + 1;
        var Pose = Camera.main.ScreenToWorldPoint(position);

        Droping?.Invoke(Pose.x, Pose.y);
    }
}