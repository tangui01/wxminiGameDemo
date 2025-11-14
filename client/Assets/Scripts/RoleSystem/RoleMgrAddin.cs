using sky_mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class RoleMgrAddin : MonoBehaviour
{
    private int id_max = 0;
    private Dictionary<Camp, Dictionary<string, RoleLogicAddin>> _RoleDic = new Dictionary<Camp, Dictionary<string, RoleLogicAddin>>();

    [SerializeField]
    GameObject _RolePanel;

    [SerializeField]
    GameObject _myInitPos;

    [SerializeField]
    GameObject _targetInitPos;

    [SerializeField]
    GameObject _XuejiPanel;

    [SerializeField]
    GameObject _XuejiObj;

    [SerializeField, Tooltip("**掉落**")]
    DropCurrency _DropAddin;

    [SerializeField, Tooltip("**我方基地位置**")]
    GameObject _myJidiInitPos;
    [SerializeField, Tooltip("**敌方基地位置**")]
    GameObject _targetJidiInitPos;

    [SerializeField, Tooltip("**我方基地血条**")]
    UIHpBox _myJidiHp;
    [SerializeField, Tooltip("**敌方基地血条**")]
    UIHpBox _targetJidiHp;

    List<GameObject> _XuejiList = new List<GameObject>();


    public static RoleMgrAddin Instance()
    {
        var mgr = GameObject.Find("RoleMgrAddin").GetComponent<RoleMgrAddin>();
        return mgr;
    }

    private void Awake()
    {
        EventManager.Instance().AddEventListener(sky_mirror.SM_EventType.FightStart, StartFight);
        //EventManager.Instance().AddEventListener(sky_mirror.SM_EventType.FightOver, StopFight);
    }

    private void OnDestroy()
    {
        EventManager.Instance().RemoveEventListener(sky_mirror.SM_EventType.FightStart, StartFight);
        //EventManager.Instance().RemoveEventListener(sky_mirror.SM_EventType.FightOver, StopFight);
    }

    private void OnEnable()
    {
        id_max = 0;

        _RolePanel.SetActive(true);

        _RoleDic.Clear();
        _RoleDic.Add(Camp.My, new Dictionary<string, RoleLogicAddin>());
        _RoleDic.Add(Camp.Target, new Dictionary<string, RoleLogicAddin>());

    }

    private void OnDisable()
    {
        
    }

    public GameObject GetRolePanel() { return _RolePanel; }

    public void CreateXueji(Transform roleTrans)
    {
        var newObj = GameObject.Instantiate(_XuejiObj, roleTrans.transform.position, Quaternion.identity, _XuejiPanel.transform);
        newObj.SetActive(true);

        _XuejiList.Add(newObj);
    }

    public void DropProp(Transform roleTrans)
    {
        var newObj = GameObject.Instantiate(_DropAddin.gameObject, roleTrans.transform.position, Quaternion.identity, _DropAddin.transform.parent);
        newObj.SetActive(true);
    }

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGUID()
    {
        id_max++;
        return id_max;
    }

    public void InputRole(RoleLogicAddin roleAddin)
    {
        var roleid = GetGUID().ToString();

        //赋予他id
        roleAddin.SetRoleId(roleid);
        var camp = roleAddin.GetCamp();

        if (_RoleDic.ContainsKey(camp) == false)
        {
            var newDic = new Dictionary<string, RoleLogicAddin>();
            _RoleDic.Add(camp, newDic);
        }

        _RoleDic[camp].Add(roleid, roleAddin);

        //GlobalFunc.Log("InputRole:" + roleAddin.GetCamp() + " - roleid:" + roleid);
    }

    public void CreateRole(int roleConfigId, Camp camp)
    {
        //加载角色
        var key = "Assets/AddressResources/Prefabs/Role/Role" + roleConfigId + ".prefab";

        Action<GameObject> callback = (obj) =>
        {
            var position = Vector3.zero;

            switch (camp)
            {
                case Camp.My:
                    position = _myInitPos.transform.position;
                    break;
                case Camp.Target:
                    position = _targetInitPos.transform.position;
                    break;
                default:
                    break;
            }

            var parent = GetRolePanel().transform;

            var Obj = GameObject.Instantiate(obj, position, Quaternion.identity, parent);

            //初始化阵营
            var logic = Obj.GetComponent<RoleLogicAddin>();
            logic.SetCamp(camp);

            Obj.SetActive(true);

            

        };

        PlatformMgr.Instance().LoadPrefab(key, callback);
    }

    public void CreateJidi(int shidaiId)
    {
        //创建我方基地
        var key = "Assets/AddressResources/Prefabs/Role/RoleJidi" + shidaiId + ".prefab";

        Action<GameObject> callback = (obj) =>
        {
            var position = _myJidiInitPos.transform.position;

            var parent = GetRolePanel().transform;

            var Obj = GameObject.Instantiate(obj, position, Quaternion.identity, parent);

            //初始化阵营
            var logic = Obj.GetComponent<RoleLogicAddin>();
            logic.SetCamp(Camp.My);

            logic.SetHpBox(_myJidiHp);

            Obj.SetActive(true);

            FightSceneAddin.Instance().SetMyjidi(logic);
        };

        PlatformMgr.Instance().LoadPrefab(key, callback);

        //创建敌方方基地
        var key_difang = "Assets/AddressResources/Prefabs/Role/RoleJidi" + shidaiId + ".prefab";

        Action<GameObject> callbackDifang = (obj) =>
        {
            var position = _targetJidiInitPos.transform.position;

            var parent = GetRolePanel().transform;

            var Obj = GameObject.Instantiate(obj, position, Quaternion.identity, parent);

            var logic = Obj.GetComponent<RoleLogicAddin>();
            logic.SetCamp(Camp.Target);

            //初始化阵营
            logic.SetHpBox(_targetJidiHp);

            Obj.SetActive(true);

            Obj.gameObject.transform.localScale= new Vector3(-1,1,1);
            FightSceneAddin.Instance().SetTargetjidi(logic);

            var InjuredDrop = Obj.GetComponent<InjuredDropAddin>();
            InjuredDrop.enabled = true;
        };

        PlatformMgr.Instance().LoadPrefab(key_difang, callbackDifang);
    }

    public void StartFight(string info, string info2, string info3)
    {


    }

    public void StopFight(string info, string info2, string info3)
    {
        //暂停所有角色的AI
        foreach (var item in _RoleDic[Camp.My])
        {
            item.Value.StopAI();
        }

        foreach (var item in _RoleDic[Camp.Target])
        {
            item.Value.StopAI();
        }

    }

    public void DeadRole(RoleLogicAddin deadRole)
    {
        //如果是基地, 结束
        var roleid = deadRole.GetRoleId();
        var isOver = FightSceneAddin.Instance().CheckWinOrLose(roleid);

        if(isOver)
        {
            return;
        }

        //先分辨出阵营
        var camp = deadRole.GetCamp();

        //掉落
        if (camp == Camp.Target)
        {
            DropProp(deadRole.transform);
        }

        var nowCampCnt = 0;

        //移除角色容器
        if (_RoleDic.ContainsKey(camp))
        {
            if(_RoleDic[camp].ContainsKey(roleid))
            {
                //GlobalFunc.Log("DeadRole: " + roleid + " camp:" + camp);
                _RoleDic[camp].Remove(roleid);

                //通知删除血条
                EventManager.Instance().EventTrigger(SM_EventType.RoleDead, roleid);


                //是否完成了
            }

            nowCampCnt = _RoleDic[camp].Count;
        }
        deadRole.Clear();

        //上报双方存活人数
        //FightSceneAddin.Instance().CheckRoleCnt(camp, nowCampCnt);
        
    }

    public void HpEventCall(RoleLogicAddin deadRole)
    {
        var roleid = deadRole.GetRoleId();

        var hp = deadRole.GetAbility().myAbility.hp;
        var hpMax = deadRole.GetAbility().myAbility.MaxHp;
        EventManager.Instance().EventTrigger(SM_EventType.RoleDamage, roleid, hp.ToString(), hpMax.ToString());
    }

    public RoleLogicAddin GetRoleById(string roleid)
    {
        if(!_RoleDic.ContainsKey(Camp.My))
        {
            return null;
        }

        //先看我方
        if (_RoleDic[Camp.My].ContainsKey(roleid))
        {
            return _RoleDic[Camp.My][roleid];
        }

        if (_RoleDic[Camp.Target].ContainsKey(roleid))
        {
            return _RoleDic[Camp.Target][roleid];
        }

        return null;
    }

    public void Clear()
    {
        //先清空血条管理器
        FightSceneAddin.Instance().ClearHpPanel();

        //清空所有角色
        foreach (var item in _RoleDic[Camp.My])
        {
            item.Value.Clear();
        }

        foreach (var item in _RoleDic[Camp.Target])
        {
            item.Value.Clear();
        }

        _RoleDic.Clear();

        foreach (var item in _XuejiList)
        {
            GameObject.Destroy(item);
        }

        _XuejiList.Clear();

        _RolePanel.SetActive(false);
    }
}
