using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDemo 
{
    /// <summary>
    /// 状态基类
    /// </summary>
    public interface IsState<T> where T : Entity
    {
        protected StateMachine<T> StateMachine { get; }
        protected T Entity { get; }

        public void Init(StateMachine<T> stateMachine, T entity);

        public void Enter();

        public void Execute();

        public void Exit();
    }
}

