using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace MyDemo
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed; //子弹飞行速度
        [SerializeField] private float bulletDamage; //子弹造成伤害
        [SerializeField] private float attackSpeed; //攻击速度
        [SerializeField] private float bulletTargetX;
        private Transform _shootPos; //射击位置

        private float _lastAttackTimer;
        private bool CanShoot => Time.time - _lastAttackTimer > attackSpeed;
        private int _attackValue;
        

        public void Init(int attackValue)
        {
            _shootPos = transform.Find("ShootPos");
            _attackValue = attackValue;
        }
        private void OnEnable()
        {
            EventManager.Register<Vector3>(GameEventKey.GunShoot, Shoot);
        }

        private void OnDisable()
        {
            EventManager.Unregister<Vector3>(GameEventKey.GunShoot, Shoot);
        }

        private void Shoot(Vector3 targetPos)
        {
            if (!CanShoot) return;
            //发射
            _lastAttackTimer = Time.time;
            PoolManager.Instance.FromPoolGetGameObject("Bullet",(a)=>
            {
                Bullet bullet = a.GetComponent<Bullet>();
                bullet.Init(_shootPos.position, bulletTargetX,bulletSpeed, _attackValue);
            }
            );
        }
    }
}