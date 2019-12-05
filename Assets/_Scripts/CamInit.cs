using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamInit : MonoBehaviour {


    bool waitForFirstFrame;

    void Start() {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = GetComponent<Camera>().orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        Debug.Log("Init: H:" + camHalfHeight + "W:" + camHalfWidth);
        transform.position = new Vector3(camHalfWidth, 100 - camHalfHeight, -10);
        waitForFirstFrame = false;
    }
}
