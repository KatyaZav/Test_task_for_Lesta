using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{   

    private void OnMouseDown()
    {
        Debug.Log("Click");
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
