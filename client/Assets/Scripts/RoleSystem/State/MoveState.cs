using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BassState
{
    public MoveState(StateManager mgr)
    {
        this._mgr = mgr;
    }
    public override void OnEnter()
    {
        this._mgr._body.MoveAnima();
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void ReseInit(BassState target)
    {

    }

}
