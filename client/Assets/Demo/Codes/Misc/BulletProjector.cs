using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjector : MonoBehaviour,IPoolItem
{
    private float speed;
    private float damage;
    private Vector3 moveDir;
    private Transform target;
    private float pushCD;
    private float timer;
    void Start()
    {
        speed = 10f;
        pushCD = 3f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= pushCD)
        {
            PushBackPool();
            return;
        }
        transform.position += moveDir * speed * Time.deltaTime;
        if (target == null) return;
        var dis = Vector3.Distance(transform.position, target.position);
        if (dis < 1.8f)
        {
            target.GetComponent<BaseController>().OnHurt(damage);
            gameObject.SetActive(false);
            PushBackPool();
        }
    }
    public void Init(Transform target,float damage)
    {
        this.target = target;
        this.damage = damage;
        moveDir = (target.position - transform.position).normalized;
        moveDir.y = 0;
    }
    public void PushBackPool()
    {
        PoolMgr.Instance.Push(AppConst.Bullet,gameObject);
    }

    public void ResetState()
    {
        timer = 0;
    }
}
