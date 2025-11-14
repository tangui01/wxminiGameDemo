using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态机
/// </summary>
namespace MyDemo
{
    public class StateMachine<TEntity> where TEntity : Entity
    {
        public IsState<TEntity> CurrentState { get; private set; }

        public void Init(IsState<TEntity> state)
        {
            CurrentState = state;
            CurrentState.Enter();
        }

        public void ChangeState(IsState<TEntity> state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

    }
}

