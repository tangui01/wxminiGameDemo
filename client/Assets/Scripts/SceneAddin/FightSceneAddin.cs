using sky_mirror;
//using Spine;
using System;

using UnityEngine;
using UnityEngine.Events;

public class FightSceneAddin : MonoBehaviour
{
    [SerializeField]
    string OverPanelName = "";

    [SerializeField]
    GameObject _HpPanel;

    [SerializeField]
    GameObject _FightUI;

    [SerializeField, Tooltip("**我方士兵工厂**")]
    SoldierFactoryAddin _mySoldierFactory;

    [SerializeField, Tooltip("**敌方士兵工厂**")]
    SoldierFactoryAddin _targetSoldierFactory;

    [SerializeField, Tooltip("**我方基地对象**")]
    RoleLogicAddin _myJidiRole;

    [SerializeField, Tooltip("**敌方基地对象**")]
    RoleLogicAddin _targetJidiRole;

    [SerializeField, Tooltip("**战力品**")]
    FightAddBox _fightAddBox;

    [SerializeField, Tooltip("**完成战后回调**")]
    UnityEvent _finishCall;

    [SerializeField]
    int ShidaiId = 1;

    [SerializeField]
    GameObject _showFightTips;

    float delayStart = 0.0f;

    bool isWin = false;

    FightSceneState _curFightSceneState = FightSceneState.Ready;

    private void OnEnable()
    {
        ShowFightTips();

        InitJidi();
        InitFactory();

        _HpPanel.SetActive(true);
        _FightUI.SetActive(true);

        EventManager.Instance().AddEventListener(sky_mirror.SM_EventType.FightPauseClickOver, FightPauseClickOver);

        delayStart = 0.5f;
        
    }

    private void OnDisable()
    {

        EventManager.Instance().RemoveEventListener(sky_mirror.SM_EventType.FightPauseClickOver, FightPauseClickOver);
    }
    // Start is called before the first frame update
    void Start()
    {

        

    }

    private void OnDestroy()
    {

    }

    void InitJidi()
    {
        //先初始化敌人基地
        var id = ShidaiId;

        RoleMgrAddin.Instance().CreateJidi(id);
    }

    public void SetMyjidi(RoleLogicAddin jidiRole)
    {
        _myJidiRole = jidiRole;
    }

    public void SetTargetjidi(RoleLogicAddin jidiRole)
    {
        _targetJidiRole = jidiRole;
    }

    void InitFactory()
    {


    }

    void StopFactory()
    {
        //开始敌人工厂
        _targetSoldierFactory.StopFactory();
        _mySoldierFactory.StopFactory();

    }

    // Update is called once per frame
    void Update()
    {
        if(delayStart > 0.0f)
        {
            delayStart -= Time.deltaTime;

            if(delayStart <= 0.0f)
            {
                StartFight();
            }
        }
    }

    public static FightSceneAddin Instance()
    {
        var obj = GameObject.Find("FightSceneAddin");

        if (obj)
        {
            return obj.GetComponent<FightSceneAddin>();
        }

        return null;
    }

    public void StartFight()
    {
        _curFightSceneState = FightSceneState.Start;

        EventManager.Instance().EventTrigger(SM_EventType.FightStart);

        //开始敌人工厂
        _targetSoldierFactory.StartFactory();
        _mySoldierFactory.StartFactory();

        
        
    }

    void ShowFightTips()
    {
        var newTips = GameObject.Instantiate(_showFightTips, _fightAddBox.transform.parent);
        newTips.SetActive(true);
    }

    public void Over(bool isWin)
    {
        _curFightSceneState = FightSceneState.Over;

        //停止工厂
        StopFactory();

        EventManager.Instance().EventTrigger(SM_EventType.FightOver);

        GlobalFunc.Log("FightScene Over, IsWin:" + isWin);

        Daojidi(isWin);
    }

    public bool IsOver()
    {
        return _curFightSceneState == FightSceneState.Over;
    }

    //检测阵营角色
    public void CheckRoleCnt(Camp camp, int cnt)
    {
        switch (camp)
        {
            case Camp.My:
                {
                    if (cnt == 0)
                    {
                        Over(false);
                    }
                }
                break;
            case Camp.Target:
                {
                    if (cnt == 0)
                    {
                        Over(true);
                    }
                }
                break;
            default:
                break;
        }
    }

    //检查阵营胜负条件
    public bool CheckWinOrLose(string roleid)
    {
        if (_myJidiRole.GetRoleId() == roleid)
        {
            isWin = false;
            Over(false);
            //ShowOverPanel(false);
            //输了
            return true;
        }

        if (_targetJidiRole.GetRoleId() == roleid)
        {
            isWin = true;
            Over(true);
            //ShowOverPanel(true);
            //赢了
            return true;
        }

        return false;
    }


    public SoldierFactoryAddin GetMySoldierFactory()
    {
        return _mySoldierFactory;
    }

    public void FightPauseClickOver(string info = "", string info2 = "", string info3 = "")
    {
        isWin = false;
        Over(false);
    }

    public void ResetAllConfig()
    {
        _HpPanel.SetActive(false);
        _FightUI.SetActive(false);

        gameObject.SetActive(false);
    }

    public void ShowOverPanel(bool isWin)
    {
    
        //清空所有角色
        RoleMgrAddin.Instance().Clear();

        var cnt = _fightAddBox.GetValue();
        //这里要判断一下, 是否需要弹这个窗
        if (cnt <= 0)
        {
            FinishFight();
        }
        else
        {
            GlobalFunc.ShowUIPanel(OverPanelName);//"OverFightPanel"
        }
    }

    public FightAddBox GetFightAddBox()
    {
        return _fightAddBox;
    }

    public void FinishFight()
    {
        ResetAllConfig();
        _finishCall.Invoke();
    }

    public void ClearHpPanel()
    {
        _HpPanel.SetActive(false);
        _HpPanel.GetComponent<UIHpPanel>().Clear();
    }

    public void Daojidi(bool isWin)
    {
        Action call = () => {
            ShowOverPanel(isWin);
        };

        if(!isWin)
        {
            _myJidiRole.GetComponent<JidiDeadAnima>().ShowAnima(call);
        }
        else
        {
            _targetJidiRole.GetComponent<JidiDeadAnima>().ShowAnima(call);
        }
    }

    public bool IsWin()
    {
        return isWin;
    }
}
