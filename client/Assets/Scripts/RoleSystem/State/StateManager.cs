using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    public RoleBody _body;

    //[SerializeField]
    //public UnityEvent _idelCall;

    //[SerializeField]
    //public UnityEvent _moveCall;

    [SerializeField]
    public UnityEvent _fightCall;

    [SerializeField]
    public UnityEvent _deathCall;

    private BassState curState = null;

    // Start is called before the first frame update
    void Start()
    {
        if(_body == null)
        {
            _body = GetComponent<RoleBody>();
        }

        if(curState == null)
        {
            Idle();
            //Fire();
        }
    }

    private bool ChangeState(BassState state)
    {
        if(curState == null)
        {
            curState = state;
            curState.OnEnter();

            //GlobalFunc.Log("enter state:" + state.GetType().Name);
            return true;
        }
        else
        {
            ////如果允许重设
            //if(curState.isResetInit)
            //{
            //    curState.ReseInit(state);
            //    return true;
            //}

            if (curState.GetType().Name != state.GetType().Name || curState.isResetInit)
            {
                curState.OnExit();
                //GlobalFunc.Log("exit state:" + curState.GetType().Name);

                curState = state;
                curState.OnEnter();
                //GlobalFunc.Log("enter state:" + state.GetType().Name);
                return true;
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FightSceneAddin.Instance().IsOver())
        {
            //暂停所有动画
            _body.Clear();
            return;
        }

        curState?.OnUpdate();
    }

    public void Move()
    {
        ChangeState(new MoveState(this));
    }

    public bool IsMove()
    {
        return curState.GetType().Name == "MoveState";
    }

    public void Idle()
    {
        ChangeState(new IdleState(this));
    }

    public bool Fire()
    {
        return ChangeState(new FightState(this));
    }

    public bool IsFight()
    {
        return curState.GetType().Name == "FightState";
    }

    public bool IsDead()
    {
        return curState.GetType().Name == "DeathState";
    }

    public void Dead()
    {
        _deathCall?.Invoke();
        //ChangeState(new DeathState(this));
    }

    public void MoveToTarget(GameObject target)
    {
        ChangeState(new MoveToTargetState(this, target));
    }

    public void Injured()
    {
        //没有状态
        _body.Injured();
    }

    private void OnDestroy()
    {
        Clear();
    }

    public void Clear()
    {
        if (curState != null)
        {
            curState.OnExit();
        }
        curState = null;

        _body.Clear();
    }


}
