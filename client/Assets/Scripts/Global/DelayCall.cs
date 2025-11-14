using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayCall : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 1.0f;

    float curTime = 0.0f;

    [SerializeField]
    private UnityEvent eventCall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        curTime = delayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(curTime > 0.0f)
        {
            curTime -= Time.deltaTime;
            if (curTime <= 0.0f)
            {
                eventCall.Invoke();
                enabled= false;
            }
        }
    }
}
