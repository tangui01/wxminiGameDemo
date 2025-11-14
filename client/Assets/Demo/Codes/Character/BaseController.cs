using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public abstract class BaseController : MonoBehaviour
{
    protected float hp;
    public float Hp => hp;

    protected float maxHp;
    public float MaxHp => maxHp;

    protected bool isDead;
    public bool IsDead => isDead;

    protected float attackDis;
    protected float pursueDis;

    protected float attack;

    [HideInInspector]
    public TeamType team;

    public string enemyLayer;//攻击的对象所在层级
    private UnityAction HpUpdateEvent;//血量更新事件
    protected Collider[] enemies;//存储范围检测周边可攻击对象
    protected BaseController target;//当前攻击目标
    public void Awake()
    {
        enemyLayer = team == TeamType.Red ? "BlueTeam" : "RedTeam";
    }

    public BaseController CheckHaveEnemy()
    {
        enemies = Physics.OverlapSphere(transform.position,pursueDis , 1 << LayerMask.NameToLayer(enemyLayer));
        if (enemies != null && enemies.Length > 0)
        {
            BaseController result;
            for(var i = 0;i < enemies.Length;i++)
            {
                result = enemies[i].GetComponent<BaseController>();
                if (result != null && !result.isDead)
                    return result;
            }
        }
        return null;
    }

    public bool CheckInDistance(float range)
    {
        return Vector3.Distance(target.transform.position, transform.position) <= range;
    }

    public virtual void OnHurt(float damage)
    {
        if (IsDead) return;
        hp -= damage;
        HpUpdateEvent?.Invoke();
        if (hp <= 0)
        {
            hp = 0;
            OnDead();
        }
    }
    public virtual void OnDead()
    {
        isDead = true;
    }
    public abstract void FireEvent();
    public abstract void FireFinishEvent();
    public abstract void DeadFinishEvent();
    public void RegisterHpUpdate(UnityAction call)
    {
        HpUpdateEvent += call;
    }
    public void RemoveHpUpdate(UnityAction call)
    {
        HpUpdateEvent -= call;
    }
}
