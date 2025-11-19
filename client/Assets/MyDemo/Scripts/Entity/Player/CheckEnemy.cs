using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 检查敌人是否进入攻击范围
    /// </summary>
    public class CheckEnemy : MonoBehaviour
    {
        private Queue<Monster> _monsters = new Queue<Monster>();

        private void Update()
        {
            
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                EventManager.Execute(GameEventKey.GunShoot, other.transform.position);
            }
        }

        /// <summary>
        /// 射线检测前方是否有怪物
        /// </summary>
        /// <param name="monster"></param>
        private void RayCheckMonster(Monster monster)
        {
             
        }
    }
}