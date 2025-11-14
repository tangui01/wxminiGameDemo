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
        [SerializeField] private bool _isShooting;//是否处于射击状态

        private bool CanShoot => Time.time - _lastAttackTimer > attackSpeed;
        private Player _player;
        private float attackValue;
        public void Init(float AttackValue)
        {
            _shootPos = transform.Find("ShootPos");
            _player = PlayerManager.Instance.Player;
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
            if (!CanShoot || _isShooting) return;
            StartCoroutine(ShootAni(targetPos));
        }

        IEnumerator ShootAni(Vector3 targetPos)
        {
            _isShooting = true;
            // 先旋转到敌人角度
            Vector3 direction = (targetPos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // 限制角度
            angle = Mathf.Clamp(angle, gunRotateMinAngle, gunRotateMaxAngle);
            transform.DOLocalRotate(new Vector3(0, 0, angle), rotationSpeed).onComplete += () =>
              {
            //发射
            _lastAttackTimer = Time.time;
                  GameObject obj = PoolManager.Instance.FromPoolGetGameObject("Bullet", bulletPrefab);
                  Bullet bullet = obj.GetComponent<Bullet>();
                  Vector3 directionToTarget = targetPos - transform.position;
                  bullet.Init(_shootPos.position, directionToTarget.normalized, bulletSpeed, attackValue);
              };
            yield return new WaitForSeconds(0.5f);
            //旋转到原来的角度
            transform.DOLocalRotate(Vector3.zero, rotationSpeed).onComplete += () =>
            {
                _isShooting = false;
            };
        }
    }
}