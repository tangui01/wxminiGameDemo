using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IdleState : BassState
{
    public IdleState(StateManager mgr)
    {
        this._mgr = mgr;
    }

    public override void OnEnter()
    {
        //GlobalFunc.Log("IdleState OnEnter");
        this._mgr._body.IdleAnima();
    }

    public override void OnExit()
    {
        //GlobalFunc.Log("IdleState OnExit");
    }

    public override void OnUpdate()
    {
        
    }

    public override void ReseInit(BassState target)
    {

    }

}
