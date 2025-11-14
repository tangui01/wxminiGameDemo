using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformAddin : MonoBehaviour
{
    [SerializeField]
    UnityEvent InitCall;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Action call = () => { InitCall.Invoke(); };

        //初始化平台管理器
        PlatformMgr.Instance().InitPlatform(call);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
