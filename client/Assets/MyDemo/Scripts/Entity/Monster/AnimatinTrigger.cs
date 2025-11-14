using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动画触发事件
/// </summary>
namespace MyDemo
{ 
public class AnimatinTrigger : MonoBehaviour
{
    private Player Player=>PlayerManager.Instance.Player;
    private Monster _monster;

    private void Awake()
    {
        _monster=transform.parent.GetComponent<Monster>();
    }

    public void AttackAniTrigger()
    {
        if (Vector3.Distance(Player.transform.position,transform.position) <= 0.5f)
        {
            EventManager.Execute(GameEventKey.PlayerHit, 20);
        }
    }

    public void DieAniTrigger()
    {
        _monster.Die();
    }
}
}
