using BTAI;
using DG.Tweening;
//using Mono.Cecil.Cil;
using sky_mirror;
//using Spine.Unity;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RoleLogicAddin : MonoBehaviour
{
    [SerializeField, Tooltip("**角色属性**")]
    RoleAbility _ability;

    [SerializeField, Tooltip("**角色状态管理器**")]
    StateManager _stateMgr;

    [SerializeField, Tooltip("**所属阵营**")]
    Camp _camp = Camp.My;

    [SerializeField, Tooltip("**AI树**")]
    AITreeEnum _aiEnum = AITreeEnum.Mutou;

    [SerializeField, Tooltip("**AI智能程度**")]
    float _aiStupid = 0.05f;

    [SerializeField, Tooltip("**敌人检测器**")]
    TargetSensorAddin _Senser;

    [SerializeField, Tooltip("**招式**")]
    GameObject _curATKBox;

    [SerializeField, Tooltip("**血条是否显示数值**")]
    public bool _isShowHpValue = false;

    [SerializeField, Tooltip("**血条是否显示**")]
    public bool _isShowHp = true;

    [SerializeField, Tooltip("**指定血条**只有是否显示血条为false才有用")]
    public UIHpBox _hpbox;

    [SerializeField, Tooltip("**受击闪白列表**")]
    public ShanBaiImage[] _shanbaiList;

    string _roleId = "";

    bool isReady = false;

    AIMgr _curAI;

    private void Awake()
    {
        
    }

    public void SetInit()
    {
        //上报,我出生了
        RoleMgrAddin.Instance().InputRole(this);

        _curAI = new AIMgr(_aiStupid, this);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInit();
        //刷一下层级
        ResetSortingOrder();

        isReady = true;

        StartAI();

        //如果有血条就初始化数值
        if(_hpbox)
        {
            _hpbox.SetValueShow(_isShowHpValue);
            _hpbox.Reset(_ability.myAbility.hp, _ability.myAbility.MaxHp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _curAI.Update(Time.deltaTime);
    }

    public void ResetSortingOrder()
    {
        //var canvas = GetStateMgr()._body.GetComponent<MeshRenderer>();
        //if (canvas)
        //{
        //    canvas.sortingOrder = 10000 - (int)transform.position.y;
        //}
    }

    public void SetPositionAndResetSorting(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, pos.y * 0.1f);
        ResetSortingOrder();
    }

    public Vector3 GetPositionAnchorPos()
    {
        return transform.position;
    }

    void StartAI()
    {
        _curAI.Run(_aiEnum);
    }

    public void StopAI()
    {
        _curAI.Stop();

    }

    public bool IsDead()
    {
        return _ability.IsDead();
    }

    public bool IsReady()
    {
        return isReady;
    }

    public bool IsSamp(string roleId)
    {
        return _roleId == roleId;
    }

    public Camp GetCamp()
    {
        return _camp;
    }

    public void SetCamp(Camp camp)
    {
        _camp = camp;

        //根据阵营切换body皮肤
        GetStateMgr()?._body.SetCamp(_camp);
    }

    public void SetHpBox(UIHpBox hpbox)
    {
        _hpbox= hpbox;

        //指定血条, 就不要生成自己的血条了
        _isShowHp = false;
    }

    public void SetRoleId(string roleId)
    {
        _roleId = roleId;

        gameObject.name = _roleId;
    }

    public string GetRoleId()
    {
        return _roleId;
    }

    public StateManager GetStateMgr()
    {
        return _stateMgr;
    }

    public RoleAbility GetAbility()
    { 
        return _ability;
    }

    public RoleLogicAddin GetTargetSenserTarget()
    {
        if(_Senser == null)
        {
            return null;
        }

        return _Senser.GetSortFireTarget();
    }

    public void LookAtTarget(GameObject target)
    {

        var isFile = target.transform.position.x > transform.position.x;

        var curScale = transform.localScale;
        var xFildx = curScale.x;

        if (isFile)
        {
            //需要镜像
            if (xFildx > 0)
            {
                xFildx *= -1;
            }
        }
        else
        {
            if (xFildx < 0)
            {
                xFildx *= -1;
            }
        }

        transform.localScale = new Vector3(xFildx, curScale.y, curScale.z);
    }

    public void Injured(float damage)
    {
        if(IsDead())
        {
            return;
        }

        _stateMgr?.Injured();

        var isDead = _ability.Injured(damage);

        //判断是否受伤掉落
        var InjuredDrop = GetComponent<InjuredDropAddin>();
        if(InjuredDrop && InjuredDrop.enabled)
        {
            InjuredDrop.Drop();
        }

        if (isDead)
        {
            if(_stateMgr)
            {
                _stateMgr.Dead();
            }
            else
            {
                DeadFinishCall();
            }
        }
        else
        {
            foreach (var item in _shanbaiList)
            {
                item.Begin();
            }

            if (_isShowHp)
            {
                //显示血条
                RoleMgrAddin.Instance().HpEventCall(this);
            }
            else
            {
                if(_hpbox)
                {
                    _hpbox.SetValueShow(_isShowHpValue);
                    _hpbox.Reset(_ability.myAbility.hp, _ability.myAbility.MaxHp);
                }
            }
        }


        //GlobalFunc.Log("被打了:" + damage);

        //扣血
    }

    public void DeadFinishCall()
    {
        //死了
        RoleMgrAddin.Instance().DeadRole(this);

        if (_hpbox)
        {
            _hpbox.SetValueShow(_isShowHpValue);
            _hpbox.Reset(_ability.myAbility.hp, _ability.myAbility.MaxHp);
        }
    }

    public void ATKCurAITarget()
    {
        //我自己死了也不用打人家了
        if(IsDead())
        {
            return;
        }

        //创建招式
        //var NewZhaoshi = Instantiate(_curATKBox, _curATKBox.transform.position, Quaternion.identity, transform);

        ////攻击当前指定的目标
        var aiTarget = _curAI.CurTempTarget();
        aiTarget?.Injured(_ability.myAbility.atk);

        
        //if(aiTarget)
        //{
        //    NewZhaoshi.GetComponent<ATKBoxAddin>().StartCheck(aiTarget);
        //}
    }

    public void Clear()
    {
        _stateMgr?.Clear();

        Destroy(gameObject);
    }

    public void Paichili(Vector3 paichiValue)
    {
        //看看有没有力学控件
        var speedMove = GetComponent<SpeedPlusMove>();

        if(speedMove)
        {
            speedMove.AddSpeedPlus(paichiValue);
        }
    }
}
