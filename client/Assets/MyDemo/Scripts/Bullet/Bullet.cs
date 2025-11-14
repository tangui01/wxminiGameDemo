using System;
using System.Collections;
using System.Collections.Generic;
using MyDemo;
using UnityEngine;

/// <summary>
/// 子弹基类
/// </summary>
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb;

    private float attackAalue;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 子弹初始化
    /// </summary>
    /// <param name="initPosition">初始位置</param>
    /// <param name="direction">发射方向</param>
    /// <param name="shootSpeed"></param>
    public void Init(Vector3 initPosition, Vector3 direction,float shootSpeed,float Attackvalue)
    {
        transform.position = initPosition;
        _rb.velocity = direction*shootSpeed;
        attackAalue = Attackvalue;
        Invoke(nameof(Destroy), 2f);
    }

    private void Destroy()
    {
        PoolManager.Instance.EnterPool("Bullet", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //如果打到了怪物
        if (other.CompareTag("Monster"))
        {
            Destroy();
        }
    }
    public float GetAttackValue()
    {
        return attackAalue;
    }
}