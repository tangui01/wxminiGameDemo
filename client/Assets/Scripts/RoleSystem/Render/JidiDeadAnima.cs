using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JidiDeadAnima : MonoBehaviour
{
    [SerializeField]
    GameObject yanwu;

    [SerializeField]
    GameObject ShowBass;

    [SerializeField]
    GameObject ShowMask;

    Action finishCall;

    bool isRun = false;

    float maskJG = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRun)
        {
            return;
        }

        DownAnima();
    }

    public void ShowAnima(Action call)
    {
        enabled = true;
        finishCall = call;
        isRun = true;

        yanwu.SetActive(true);
        maskJG = 0.0f;

    }

    void DownAnima()
    {
        var fdt = Time.deltaTime;

        maskJG += fdt;

        if(maskJG >= 2.5f)
        {
            maskJG = 2.5f;
        }

        var speed = 1.3f * fdt;

        var curPos = ShowBass.transform.localPosition;
        curPos += new Vector3(0, -1, 0) * speed;
        ShowBass.transform.localPosition = curPos;

        curPos = ShowMask.transform.localPosition;
        curPos += new Vector3(0, -1, 0) * speed;
        ShowMask.transform.localPosition = curPos;

        if (maskJG >= 2.5f)
        {
            isRun = false;
            yanwu.SetActive(false);

            finishCall.Invoke();

            enabled = false;
        }
    }
}
