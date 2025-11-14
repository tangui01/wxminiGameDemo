using sky_mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaiChiAddin : MonoBehaviour
{
    [SerializeField]
    RoleLogicAddin _logic;

    [SerializeField]
    float PaichiValue = 1.0f;

    float curTime = 0.0f;
    int curRoleCnt = 0;

    private Dictionary<string, RoleLogicAddin> _TargetDic = new Dictionary<string, RoleLogicAddin>();

    // Start is called before the first frame update
    void Start()
    {
        //根据阵营,修改自己layer
        var camp = _logic.GetCamp();
        switch (camp)
        {
            case Camp.My:
                gameObject.layer = LayerMask.NameToLayer("Paichi-My");
                break;
            case Camp.Target:
                gameObject.layer = LayerMask.NameToLayer("Paichi-Target");
                break;
            default:
                break;
        }

        //监听角色死亡，方便移除
        EventManager.Instance().AddEventListener(SM_EventType.RoleDead, RoleDead);
    }

    void OnDestroy()
    {
        //移除
        EventManager.Instance().RemoveEventListener(SM_EventType.RoleDead, RoleDead);
    }
    public void Clear()
    {
        _TargetDic.Clear();
        curRoleCnt = 0;
    }


    public void RoleDead(string roleid, string info2 = "", string info3 = "")
    {
        _TargetDic.Remove(roleid);
    }

    public bool IsHaveRoleFormRoleId(string RoleId)
    {
        return _TargetDic.ContainsKey(RoleId);
    }

    public void SensorClear(string isShow, string info2 = "", string info3 = "")
    {
        Clear();

    }

    // Update is called once per frame
    void Update()
    {
        //持续给排斥力
        if(curRoleCnt > 0)
        {
            curTime += Time.deltaTime;

            if(curTime >= 1.0f)
            {
                curTime = 0.0f;

                foreach (var item in _TargetDic)
                {
                    var rolelogic = item.Value;

                    var normal = _logic.transform.position - rolelogic.transform.position;
                    normal = normal.normalized * -1.0f;
                    normal.x = 0.0f;
                    if (Mathf.Abs(normal.y) <= 0.2f)
                    {
                        if (normal.y > 0)
                        {
                            normal.y = 0.2f;
                        }
                        else if(normal.y < 0)
                        {
                            normal.y = -0.2f;
                        }
                    }
                    rolelogic.Paichili(normal * PaichiValue);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ////判断是否指定tag
        //if (!IsTagReady(other.gameObject))
        //{
        //    return;
        //}

        if (_logic.IsDead()) { return; }

        //检查是否是SenserBody
        var senserBody = other.gameObject.GetComponent<SenserBodyAddin>();
        if (!senserBody)
        {
            return;
        }

        //给他一个排斥力
        var rolelogic = senserBody.GetRoleLogic();
        if (rolelogic == null) { return; }

        var roleid = rolelogic.GetRoleId();

        if (_TargetDic.ContainsKey(roleid) || roleid == _logic.GetRoleId())
        {
            return;
        }

        _TargetDic.Add(roleid, rolelogic);

        curRoleCnt = _TargetDic.Count;

        var normal = _logic.transform.position - rolelogic.transform.position;
        normal = normal.normalized * -1.0f;
        //normal.x = 0.0f;

        if (Mathf.Abs(normal.y) <= 1.0f)
        {
            if (normal.y > 0)
            {
                normal.y = 1.0f;
            }
            else if (normal.y < 0)
            {
                normal.y = -1.0f;
            }
            else
            {
                var random = Random.Range(1, 100);

                if(random < 50)
                {
                    normal.y = -1.0f;
                }
                else
                {
                    normal.y = 1.0f;
                }
            }
        }

        rolelogic.Paichili(normal * PaichiValue);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if (!IsTagReady(other.gameObject))
        //{
        //    return;
        //}

        var senserBody = other.gameObject.GetComponent<SenserBodyAddin>();
        if (!senserBody)
        {
            return;
        }

        var roleLogic = senserBody.GetRoleLogic();
        _TargetDic.Remove(roleLogic.GetRoleId());

        curRoleCnt = _TargetDic.Count;
    }

    public bool IsTagReady(GameObject obj)
    {
        var myTag = gameObject.tag;

        if (myTag == "Paichi-My")
        {
            return obj.tag == "Camp-My";
        }
        else if (myTag == "Paichi-Target")
        {
            return obj.tag == "Camp-Target";
        }

        return false;
    }
}
