using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ScreenAdaptive : MonoBehaviour
{
    CanvasScaler UICanva;

    void Awake()
    {
        UICanva = GetComponent<CanvasScaler>();

        var screenScale = 1.0f;
        float ratio = (float)Screen.height / (float)Screen.width;
        if (ratio <= 1.34)
        {
            screenScale = 1.5f;
        }

        Vector2 _size = UICanva.referenceResolution;
        UICanva.referenceResolution = _size * screenScale;
        if (UICanva.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera)
        {
            UICanva.GetComponent<Canvas>().worldCamera.orthographicSize *= screenScale;
        }

        //for (int i = 0; i < UICanvas.Count; i++)
        //{
        //    Vector2 _size = UICanvas[i].referenceResolution;
        //    UICanvas[i].referenceResolution = _size * screenScale;
        //    if (UICanvas[i].GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera)
        //    {
        //        UICanvas[i].GetComponent<Canvas>().worldCamera.orthographicSize *= screenScale;
        //    }
        //}
    }
}