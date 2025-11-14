//using Spine;
//using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : BassState
{
    //Spine.AnimationState.TrackEntryDelegate onComplete = null;
    //Spine.AnimationState.TrackEntryEventDelegate onFireEvent = null;
    public override void ReseInit(BassState target)
    {

    }
    public FightState(StateManager mgr)
    {
        this._mgr = mgr;
    }
    public override void OnEnter()
    {
        //var db = this._mgr._body.GetComponent<SkeletonAnimation>();

        ////db.skeleton.SetToSetupPose();
        ////db.AnimationState.ClearTracks();

        ////监听一下
        
        //var state = db.AnimationState;
        //onComplete = delegate
        //{
        //    DbFightCompleteCall();
        //};

        //db.AnimationState.Complete += onComplete;

        //onFireEvent = delegate (TrackEntry entry, Spine.Event e)
        //{
        //    DbFightEventCall(e); 
        //};

        //db.AnimationState.Event += onFireEvent;

        //_mgr._body.FightAnima();
    }

    public override void OnExit()
    {
        //var db = this._mgr._body.GetComponent<SkeletonAnimation>();

        //var state = db.AnimationState;
        //state.Complete -= onComplete;
        //state.Event -= onFireEvent;

       
    }

    public override void OnUpdate()
    {

    }

    public void DbFightCompleteCall()
    {
        this._mgr.Idle();
    }
    //攻击帧事件
    //public void DbFightEventCall(Spine.Event obj)
    //{
    //    if(obj.Data.Name == "fire")
    //    {
    //        _mgr._fightCall.Invoke();
    //        _mgr.GetComponent<RoleAbility>().RunATKCD();
    //    }
    //}
}
