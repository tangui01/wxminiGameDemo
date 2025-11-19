using System.Collections;
using UnityEngine;
using DG.Tweening;
namespace MyDemo
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float gunRotateMaxAngle = 90;
        [SerializeField] private float gunRotateMinAngle = -90;

        [SerializeField] private float rotationSpeed;
        [SerializeField] private float bulletSpeed; //子弹飞行速度
        [SerializeField] private float bulletDamage; //子弹造成伤害
        [SerializeField] private float attackSpeed; //攻击速度
        private Transform _shootPos; //射击位置

        private float _lastAttackTimer;
        private bool CanShoot => Time.time - _lastAttackTimer > attackSpeed;
        private float attackValue;
        public void Init(float AttackValue)
        {
            _shootPos = transform.Find("ShootPos");
            attackValue = AttackValue;
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
             GameObject obj = PoolManager.Instance.FromPoolGetGameObject("Bullet", bulletPrefab);
             Bullet bullet = obj.GetComponent<Bullet>();
             bullet.Init(_shootPos.position, transform.right.normalized, bulletSpeed, attackValue); 
        }
    }
}