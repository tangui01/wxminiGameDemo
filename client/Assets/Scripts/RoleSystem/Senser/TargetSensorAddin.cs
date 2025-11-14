using sky_mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetSensorAddin : MonoBehaviour
{
    [SerializeField]
    private RoleLogicAddin parent;

    private Dictionary<string, RoleLogicAddin> _TargetDic = new Dictionary<string, RoleLogicAddin>();

    private void Start()
    {
        var camp = parent.GetCamp();
        switch (camp)
        {
            case Camp.My:
                gameObject.layer = LayerMask.NameToLayer("Senser-My");
                break;
            case Camp.Target:
                gameObject.layer = LayerMask.NameToLayer("Senser-Target");
                break;
            default:
                break;
        }
        //监听角色死亡，方便移除
        EventManager.Instance().AddEventListener(SM_EventType.RoleDead, RoleDead);
    }

    public void RoleDead(string roleid, string info2 = "", string info3 = "")
    {
        _TargetDic.Remove(roleid);

        //if (parent.IsSamp(name))
        //{
        //    //死的是自己
        //    Destroy(gameObject);
        //}
    }

    public bool IsHaveRoleFormRoleId(string RoleId)
    {
        return _TargetDic.ContainsKey(RoleId);
    }

    public void SensorClear(string isShow, string info2 = "", string info3 = "")
    {
        Clear();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        ////判断是否指定tag
        //var curTag = gameObject.tag;

        //if(!IsTagReady(other.gameObject))
        //{
        //    return;
        //}

        //Debug.Log("OnTriggerEnter2D:" + other.gameObject.name);
        //先判断自己是不是死了
        if (parent.IsDead()) { return; }

        //检查是否是SenserBody
        var senserBody = other.gameObject.GetComponent<SenserBodyAddin>();
        if(!senserBody)
        {
            return;
        }

        //插入
        var roleLogic = senserBody.GetRoleLogic();
        if (roleLogic && roleLogic.IsDead()) { return; }

        var roleid = roleLogic.GetRoleId();
        if (!_TargetDic.ContainsKey(roleid))
        {
            _TargetDic.Add(roleid, roleLogic);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsTagReady(other.gameObject))
        {
            return;
        }

        var senserBody = other.gameObject.GetComponent<SenserBodyAddin>();
        if (!senserBody)
        {
            return;
        }

        var roleLogic = senserBody.GetRoleLogic();
        _TargetDic.Remove(roleLogic.GetRoleId());
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter:" + other.gameObject.name);
        //先判断自己是不是死了
        //if (parent.IsDead()) { return; }
        ////插入
        //var roleLogic = other.gameObject.GetComponent<RoleLogicAddin>();
        //if (roleLogic && roleLogic.IsDead()) { return; }

        //if(!_TargetDic.ContainsKey(other.gameObject.name))
        //{
        //    _TargetDic.Add(other.gameObject.name, roleLogic);
        //}
        
    }
    private void OnTriggerExit(Collider other)
    {
        //_TargetDic.Remove(other.gameObject.name);
    }


    public RoleLogicAddin GetSortFireTarget()
    {
        if (parent.IsDead())
        {
            return null;
        }

        var len = 10000.0f;
        var CurPos = transform.position;

        var name = "";

        foreach (var item in _TargetDic)
        {
            //是否已经挂了
            if (!item.Value.IsDead())
            {
                var pos = item.Value.transform.position;
                var curDis = Vector3.Distance(CurPos, pos);
                if (curDis < len)
                {
                    name = item.Key;
                    len = curDis;
                }
            }
        }

        if (name != "")
        {
            return _TargetDic[name];
        }
        else
        {
            if (_TargetDic.Count == 0)
            {
                if (!parent.IsDead())
                {
                    //parent.OkHelp();
                }
            }

            return null;
        }
    }
   
    public void CheckClear()
    {
        List<string> keys = new List<string>();

        foreach (var item in _TargetDic)
        {
            //是否已经挂了
            if (item.Value.IsDead())
            {
                keys.Add(item.Key);
            }
        }

        foreach (var key in keys)
        {
            _TargetDic.Remove(key);
        }
    }

    void OnDestroy()
    {
        //移除
        EventManager.Instance().RemoveEventListener(SM_EventType.RoleDead, RoleDead);
    }

    public void Clear()
    {
        _TargetDic.Clear();
    }

    public bool IsTagReady(GameObject obj)
    {
        var myTag = gameObject.tag;

        if(myTag == "Camp-Target")
        {
            return obj.tag == "Camp-My";
        }
        else if(myTag == "Camp-My")
        {
            return obj.tag == "Camp-Target";
        }

        return false;
    }
}
