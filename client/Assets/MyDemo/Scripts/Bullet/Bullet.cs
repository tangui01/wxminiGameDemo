using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 子弹基类
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        private int _attackValue;

        /// <summary>
        /// 子弹初始化
        /// </summary>
        /// <param name="initPosition">初始位置</param>
        /// <param name="shootSpeed"></param>
        /// <param name="attack"></param>
        public void Init(Vector3 initPosition, float targetX, float shootSpeed, int attack)
        {
            transform.position = initPosition;
            _attackValue = attack;
            transform.DOMoveX(targetX, shootSpeed).onComplete += Destroy;
        }

        private void Destroy()
        {
            EventManager.Execute(GameEventKey.MonsterHit, _attackValue);
            PoolManager.Instance.EnterPool("Bullet", gameObject);
        }

        public float GetAttackValue()
        {
            return _attackValue;
        }

        private void Update()
        {
        }
    }
}