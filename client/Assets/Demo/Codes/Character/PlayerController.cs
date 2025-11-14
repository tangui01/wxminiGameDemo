using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerController : BaseController
{
    private StickController stickController;
    public Vector3 moveDir;
    public float moveSpeed;
    public float rotateSpeed;

    private Animator animator;
    private int IsMoveHash = Animator.StringToHash("IsMove");
    private int DeadHash = Animator.StringToHash("Dead");
    private int ShootHash = Animator.StringToHash("Shoot");

    private GameObject bulletPref;
    public Transform gunPoint;
    private GameController owner;

    public bool CanInteract => !animator.GetBool(IsMoveHash);
    public bool CanControl { get; set; }
    

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //初始化角色属性参数
        maxHp = 20000;
        hp = maxHp;
        attack = 50;
        attackDis = 20f;
        pursueDis = attackDis;
        moveDir = Vector3.zero;
        team = TeamType.Blue;
        CanControl = true;

        var handle = Addressables.LoadAssetAsync<GameObject>("Bullet");
        handle.Completed += (obj) => { bulletPref = handle.Result; };
        
    }
    public void Init(GameController gameController)
    {
        owner = gameController;
        Camera.main.GetComponent<CameraController>().SetFollowTarget(transform);
        stickController = GameObject.FindObjectOfType<StickController>();
    }

    void Update()
    {
        if (!CanControl || IsDead || stickController == null) return;
        moveDir.x = stickController.moveDir.x;
        moveDir.z = stickController.moveDir.y;

        var isMove = moveDir != Vector3.zero;
        animator.SetBool(IsMoveHash, isMove);

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        if (isMove)
        {
            var look = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * rotateSpeed);
        }

        CheckEnemyAround();
    }

    private void CheckEnemyAround()
    {
        if (moveDir == Vector3.zero)
        {
            target = CheckHaveEnemy();
            if (target == null)
            {
                StopAttack();
                return;
            }
            var enemyDir = (target.transform.position - transform.position).normalized;
            var canAttack = CheckInDistance(attackDis) && target != null;
            animator.SetBool(ShootHash, canAttack);
            if (canAttack)
            {
                //有敌人时 && 没有移动向量 && 在范围内 时转向敌人
                var lookDir = Quaternion.LookRotation(enemyDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, Time.deltaTime * rotateSpeed);
            }
        }else
            StopAttack();
    }
    public void StopAttack()
    {
        if (animator.GetBool(ShootHash))
            animator.SetBool(ShootHash, false);
    }
    public override void FireEvent()
    {
        if (target == null) return;
        var bulletIns = PoolMgr.Instance.Get(AppConst.Bullet, null);
        bulletIns.transform.position = gunPoint.position;
        bulletIns.GetComponent<BulletProjector>().Init(target.transform, attack);
        bulletIns.SetActive(true);
    }

    public override void FireFinishEvent()
    {

    }
    public override void DeadFinishEvent()
    {
    }
    public override void OnHurt(float damage)
    {
        base.OnHurt(damage);
        Debug.Log(Hp);
    }
    public override void OnDead()
    {
        if (IsDead) return;
        animator.SetTrigger(DeadHash);
        owner.OnPlayerDead();
        base.OnDead();
    }

}
