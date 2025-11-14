using UnityEngine;
using System;
using System.Collections;

public class CameraAutoSize: MonoBehaviour
{
    public string scaleMode = "fixedWidth";

    public float designWidth = 7.5f;
    public float designHeight = 16.6f;

    // Use this for initialization
    void Start()
    {
        float aspectRatio = Screen.width * 1.0f / Screen.height;
        float orthographicSize = 0;

        switch (scaleMode)
        {
            case "fixedWidth":
                orthographicSize = designWidth / (2 * aspectRatio);
                break;
            case "fixedHeight":
                orthographicSize = designHeight / 2;
                break;
        }

        this.GetComponent<Camera>().orthographicSize = orthographicSize;
        Debug.Log(orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {

    }
}