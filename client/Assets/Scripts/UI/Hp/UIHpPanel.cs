using sky_mirror;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class UIHpPanel : MonoBehaviour
{
    [SerializeField]
    GameObject cloneHpBox;

    private List<string> _hpBoxNameList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance().AddEventListener(SM_EventType.RoleDamage, EventCall);
        EventManager.Instance().AddEventListener(SM_EventType.RemoveHPBox, RemoveBox);

        EventManager.Instance().AddEventListener(SM_EventType.RoleDead, RemoveBox);
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in _hpBoxNameList)
        {
            ResetPos(item);
        }
    }

    public void EventCall(string roleid, string hp = "", string hpMax = "")
    {
        var hpValue = float.Parse(hp);
        var hpMaxValue = float.Parse(hpMax);
        var CurHp = hpValue / hpMaxValue;

        //先查询是否已经有缓存了
        //Debug.Log(roleid + " " + Lv + " " + hpbfb);
        if (_hpBoxNameList.Contains(roleid))
        {
            var hpBox = transform.Find(roleid).gameObject;
            //如果是满的，就不用显示了

            
            if (CurHp > 1.01f)
            {
                CurHp = 0.998f;
            }

            if (CurHp >= 0.999f)
            {
                RemoveBox(roleid);
                return;
            }
            else
            {
                hpBox.SetActive(true);
            }

            var roleObj = RoleMgrAddin.Instance().GetRoleById(roleid);
            if (roleObj == null) {
                GameObject.Destroy(hpBox);
                //如果是空的，就删除了
                _hpBoxNameList.Remove(roleid);
                return; 
            }

            ConfigHpBox(hpBox, hpValue, hpMaxValue, roleObj._isShowHpValue);
        }
        else
        {
            if (CurHp >= 0.999f)
            {
                return;
            }

            //从角色管理器那边获取坐标
            var roleObj = RoleMgrAddin.Instance().GetRoleById(roleid);
            if (roleObj == null) { return; }

            //创建新的
            var hpBox = GameObject.Instantiate(cloneHpBox, transform);
            hpBox.name = roleid;
            //hpBox.transform.localScale = Vector3.one;
            //hpBox.transform.localEulerAngles = Vector3.zero;

            var hpBoxAddin = hpBox.GetComponent<UIHpBox>();
            hpBoxAddin.SetCamp(roleObj.GetCamp() == Camp.My);

            ConfigHpBox(hpBox, hpValue, hpMaxValue, roleObj._isShowHpValue);
            _hpBoxNameList.Add(roleid);
        }

        ResetPos(roleid);
    }

    private void ResetPos(string roleid)
    {
        if (_hpBoxNameList.Contains(roleid))
        {
            //Debug.Log("dead:" + roleid);
            var hpBox = transform.Find(roleid).gameObject;

            //从角色管理器那边获取坐标
            var roleObj = RoleMgrAddin.Instance().GetRoleById(roleid);

            if (roleObj != null)
            {
                
                var objHpPos = roleObj.transform.Find("hpPos");

                if (objHpPos != null)
                {
                    hpBox.transform.position = objHpPos.transform.position;
                    //hpBox.GetComponent<RectTransform>().anchoredPosition = GlobalFunc.PositionToUGUI(objHpPos.position);
                }
                else
                {
                    hpBox.transform.position = roleObj.transform.position;
                    //var pos = GlobalFunc.PositionToUGUI(roleObj.gameObject.transform.position);
                    //pos += new Vector2(0, 110.0f);
                    //hpBox.GetComponent<RectTransform>().anchoredPosition = pos;
                }
            }
            else
            {
                GameObject.Destroy(hpBox);
                //如果是空的，就删除了
                _hpBoxNameList.Remove(roleid);
            }
        }
    }

    public void RemoveBox(string roleid, string Lv = "", string hpbfb = "")
    {
        //先查询是否已经有缓存了
        if (_hpBoxNameList.Contains(roleid))
        {
            var hpBox = transform.Find(roleid).gameObject;
            hpBox.SetActive(false);
            GameObject.Destroy(hpBox);
            _hpBoxNameList.Remove(roleid);
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance().RemoveEventListener(SM_EventType.RemoveHPBox, RemoveBox);
        EventManager.Instance().RemoveEventListener(SM_EventType.RoleDead, RemoveBox);
        EventManager.Instance().RemoveEventListener(SM_EventType.RoleDamage, EventCall);
        _hpBoxNameList.Clear();
    }

    private void ConfigHpBox(GameObject hpBox, float hp, float max, bool isShowValue)
    {
        var hpBoxAddin = hpBox.GetComponent<UIHpBox>();
        hpBoxAddin.SetValueShow(isShowValue);
        hpBoxAddin.Reset(hp, max);

    }

    public void Clear()
    {
        foreach (var item in _hpBoxNameList)
        {
            var hpBox = transform.Find(item).gameObject;
            if(hpBox)
            {
                Destroy(hpBox);
            }
        }

        _hpBoxNameList.Clear();
    }
}
