using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AIController : BaseController,IPoolItem
{
    public Transform gunPoint;

    private Vector3 moveDir;
    private float moveSpeed;
    private float rotateSpeed;

    public Animator animator;

    private int IsMoveHash = Animator.StringToHash("IsMove");
    private int DeadHash = Animator.StringToHash("Dead");
    private int ShootHash = Animator.StringToHash("Shoot");

    private TeamGenerator owner;
    private EnemyData configData;


    public void Init(TeamGenerator teamGenerator,EnemyData data,TeamType teamType)
    {
        owner = teamGenerator;
        configData = data;
        maxHp = data.hp;
        hp = maxHp;
        attack = data.attack;
        pursueDis = data.pursueDis;
        attackDis = data.attackDis;
        rotateSpeed = 10f;
        moveSpeed = 4f;
        team = teamType;
    }

    void Update()
    {
        if (isDead || animator == null) return;
        target = CheckHaveEnemy();
        if (target == null)
        {
            if (animator.GetBool(IsMoveHash))
            {
                animator.SetBool(ShootHash, false);
                animator.SetBool(IsMoveHash, false);
            }
            return;
        }

        moveDir = (target.transform.position - transform.position).normalized;

        var canAttack = CheckInDistance(attackDis) && target != null;
        if(!canAttack)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        animator.SetBool(ShootHash, canAttack);
        animator.SetBool(IsMoveHash, !canAttack);
        var look = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * rotateSpeed);

    }


    public override void OnDead()
    {
        if (IsDead) return;
        animator.SetTrigger(DeadHash);
        owner.OnActorDead();
        base.OnDead();
    }

    public override void FireEvent()
    {
        if (target == null) return;
        var bulletIns = PoolMgr.Instance.Get(AppConst.Bullet,null);
        bulletIns.transform.position = gunPoint.position;
        var bulletProjector = bulletIns.GetComponent<BulletProjector>();
        bulletProjector.Init(target.transform, attack);
        bulletProjector.ResetState();
        bulletIns.SetActive(true);
    }

    public override void FireFinishEvent()
    {

    }

    public override void DeadFinishEvent()
    {
        PoolMgr.Instance.Push(team == TeamType.Red ? AppConst.RedTeam : AppConst.BlueTeam, gameObject);
    }

    public void ResetState()
    {
        isDead = false;
    }
}
