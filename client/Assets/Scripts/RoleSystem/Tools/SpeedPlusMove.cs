using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlusMove : MonoBehaviour
{
    [SerializeField]
    private RoleLogicAddin _logic;

    [SerializeField]
    private float stepScale = 100.0f;

    [SerializeField]
    private float subScale = 6.0f;

    [SerializeField]
    private float MaxValue = 100.0f;

    private Vector3 speedplus;
    public bool isAction;

    // Start is called before the first frame update
    void Start()
    {
        speedplus = new Vector3(0, 0, 0);
        isAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FightSceneAddin.Instance().IsOver())
        {
            return;
        }

        if (isAction)
        {
            var temp = speedplus * stepScale * Time.deltaTime;
            //temp.y = 0.0f;
            var pos = gameObject.transform.position;
            pos += temp;
            _logic.SetPositionAndResetSorting(pos);

            speedplus *= (1.0f - Time.deltaTime * subScale);

            if(Mathf.Abs(speedplus.x) <= 0.1f)
            {
                speedplus.x = 0;
            }

            if (Mathf.Abs(speedplus.y) <= 0.1f)
            {
                speedplus.y = 0;

                if(speedplus.x == 0 && speedplus.y == 0)
                {
                    isAction = false;
                }
            }
        }
    }

    public void AddSpeedPlus(Vector3 add) 
    {
        speedplus += add;

        if(Mathf.Abs(speedplus.x) >= MaxValue)
        {
            speedplus.x = MaxValue * (Mathf.Abs(speedplus.x) / speedplus.x);
        }

        if (Mathf.Abs(speedplus.z) >= MaxValue)
        {
            speedplus.z = MaxValue * (Mathf.Abs(speedplus.z) / speedplus.z);
        }

        if (Mathf.Abs(speedplus.y) >= MaxValue)
        {
            speedplus.y = MaxValue * (Mathf.Abs(speedplus.y) / speedplus.y);
        }

        isAction = true;
    }

    public void SetSpeedPlus(Vector3 add)
    {
        speedplus = add;

        if (Mathf.Abs(speedplus.x) >= MaxValue)
        {
            speedplus.x = MaxValue * (Mathf.Abs(speedplus.x) / speedplus.x);
        }

        if (Mathf.Abs(speedplus.z) >= MaxValue)
        {
            speedplus.z = MaxValue * (Mathf.Abs(speedplus.z) / speedplus.z);
        }

        if (Mathf.Abs(speedplus.y) >= MaxValue)
        {
            speedplus.y = MaxValue * (Mathf.Abs(speedplus.y) / speedplus.y);
        }

        isAction = true;
    }

    public void ResetZero()
    {
        Start();
    }
}
