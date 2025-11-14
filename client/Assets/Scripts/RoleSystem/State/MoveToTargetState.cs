using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MoveToTargetState : BassState
{
    public Vector3 _TargetPos;

    public MoveToTargetState(StateManager mgr, GameObject target)
    {
        isResetInit = true;
        _mgr = mgr;
        _TargetPos = target.transform.position;
    }

    public override void ReseInit(BassState target)
    {
        var tempTarget = (MoveToTargetState)target;
        _TargetPos = tempTarget._TargetPos;
    }

    public override void OnEnter()
    {
        _mgr._body.MoveAnima();
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        //if(_Target == null)
        //{
        //    //角色死了
        //    _mgr.Idle();
        //    return;
        //}

        var moveToSpeed = _mgr.GetComponent<RoleLogicAddin>().GetAbility().GetMoveSpeed();

        var jg = _TargetPos - _mgr.transform.position;

        var step = moveToSpeed * Time.deltaTime;

        var leg = jg.magnitude;
        if (leg >= step)
        {
            var normal = jg.normalized;

            var nextPos = _mgr.transform.position + normal * step;
            //移动过去
            _mgr.GetComponent<RoleLogicAddin>().SetPositionAndResetSorting(nextPos);

        }
        else
        {
            //到达了
            //_mgr.Idle();
        }

    }

}
