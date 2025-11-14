using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPauseFightPanel : MonoBehaviour
{
    //[SerializeField]
    //UnityEvent _call;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoOverFight()
    {
        GlobalFunc.ADSResume();

        GameObject.Destroy(gameObject);
    }

    public void OverFight()
    {
        //_call.Invoke();
        NoOverFight();

        //通知过去, 结束了
        EventManager.Instance().EventTrigger(sky_mirror.SM_EventType.FightPauseClickOver);
    }

    public void AnimaFinish()
    {
        GlobalFunc.ADSPause();
    }
}
