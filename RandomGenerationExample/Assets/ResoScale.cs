using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoScale : MonoBehaviour
{
    // Use this for initialization
    Vector2 targetReso = new Vector2 (854f,480f);
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = targetReso.x / targetReso.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = targetReso.y / 2 /10;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = targetReso.y / 2 / 10* differenceInSize;
        }
    }

}
