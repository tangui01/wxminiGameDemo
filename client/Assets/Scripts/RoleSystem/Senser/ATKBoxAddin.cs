using sky_mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.GraphicsBuffer;

public class ATKBoxAddin : MonoBehaviour
{
    [SerializeField]
    ATKBoxEnum _curAtkEnum = ATKBoxEnum.Dan;

    [SerializeField]
    private float damager = 0.0f;


    [SerializeField]
    private float delayTime = 0.05f;
    float curTime = 0.0f;

    RoleLogicAddin _target;

    bool isReadyDamage = false;

    private Dictionary<string, bool> _Dis = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curTime > 0.0f)
        {
            curTime -= Time.deltaTime;
            if (curTime <= 0.0f)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_Dis.ContainsKey(other.gameObject.name))
        {
            return;
        }

        _Dis.Add(other.gameObject.name, true);

        if (!isReadyDamage)
        {
            return;
        }

        HitTarget(other.gameObject.GetComponent<RoleLogicAddin>());

        if (_curAtkEnum == ATKBoxEnum.Dan)
        {
            isReadyDamage = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_Dis.ContainsKey(other.gameObject.name))
        {
            _Dis.Remove(other.gameObject.name);
            return;
        }
    }

    public void HitTarget(RoleLogicAddin target)
    {
        //对象受伤
        if (target && !target.IsDead())
        {
            target.Injured(damager);
        }
    }

    public void ShowHit(Vector3 closestPoint)
    {
    }

    public void StartCheck(RoleLogicAddin target)
    {
        _Dis.Clear();
        curTime = delayTime;

        gameObject.SetActive(true);

        //如果指定了对象, 那就不用检测了,直接伤害
        if (target && _curAtkEnum == ATKBoxEnum.Dan)
        {
            //关掉碰撞盒
            GetComponent<BoxCollider2D>().enabled = false;

            isReadyDamage = false;
            HitTarget(target);

            return;
        }

        isReadyDamage = true;
    }

    public void StopCheck()
    {
        gameObject.SetActive(false);
        isReadyDamage = false;
        curTime = 0.0f;
    }
}
