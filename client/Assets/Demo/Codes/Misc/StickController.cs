using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickController : MonoBehaviour
{
    public Image imgStick;//摇杆
    private Vector3 centerPoint;
    public float radius;
    private bool showStick;

    [HideInInspector]
    public Vector3 moveDir;
    void Start()
    {
        SwitchStick(false);
        radius = 100;
    }

    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("鼠标按下");
            //记录鼠标点击的位置
            centerPoint = Input.mousePosition;
            //显示摇杆
            SwitchStick(true);
        }
        if (showStick)
        {
            var cur = Input.mousePosition;
            var dis = Vector2.Distance(cur, centerPoint);
            //计算方向:限定范围内,输出移动向量
            moveDir = (cur - centerPoint).normalized;

            if (dis >= radius)
            {
                imgStick.transform.position = centerPoint + moveDir.normalized * radius;
            }
            else
                imgStick.transform.position = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && showStick)
        {
            //Debug.Log("鼠标抬起");
            SwitchStick(false);
        }


    }
    public void SwitchStick(bool visible)
    {
        imgStick.gameObject.SetActive(visible);
        showStick = visible;
        if (visible)
            imgStick.transform.position = centerPoint;
        else
            moveDir = Vector3.zero;
    }
}
