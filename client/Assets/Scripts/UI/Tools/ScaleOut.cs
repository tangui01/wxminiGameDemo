using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOut : MonoBehaviour
{
    [SerializeField]
    float ScaleValue = 1.0f;

    bool isRun = false;
    float curScale = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRun)
        {
            curScale -= Time.deltaTime;

            if(curScale <= 1.0f)
            {
                curScale = 1.0f;

                isRun = false;
            }

            GetComponent<RectTransform>().localScale = new Vector3(curScale, curScale, curScale);
        }
    }

    public void StartAnima()
    {
        isRun = true;
        curScale = ScaleValue;

        GetComponent<RectTransform>().localScale = new Vector3(curScale, curScale, curScale);
    }
}
