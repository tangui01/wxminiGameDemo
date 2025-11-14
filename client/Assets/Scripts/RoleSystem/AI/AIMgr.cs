using BTAI;
//using DragonBones;
using sky_mirror;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public struct RoleAITempData
{
    public RoleLogicAddin target;

}

public class AIMgr
{
    private RoleLogicAddin _Logic;
    private float _Stupid = 0.05f;//愚蠢度，越高越傻
    private float _CurCount = 0.0f;

    private BassNode rootNode;

    private bool isRun = false;

    RoleAITempData _RoleAITempData = new RoleAITempData();

    public AIMgr(float stupid, RoleLogicAddin logic)
    {
        _Stupid = stupid;
        rootNode = null;
        _Logic = logic;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    public void Update(float fdt)
    {
        if (FightSceneAddin.Instance().IsOver())
        {
            return;
        }

        if (!isRun)
        {
            return;
        }

        _CurCount += fdt;

        if(_CurCount >= _Stupid)
        {
            _CurCount = 0.0f;

            rootNode.OnEnter(Time.deltaTime);
        }
    }

    public void Run(AITreeEnum treeEnum)
    {
        if (rootNode != null)
        {
            Stop();
        }

        _CurCount = 0.0f;

        switch (treeEnum)
        {
            case AITreeEnum.Mutou:
                break;
            case AITreeEnum.ATK:
                {
                    rootNode = ATKTree.Create(this);
                    isRun = true;
                    
                }
                break;
            default:
                break;
        }
    }

    public void Stop()
    {
        isRun = false;
        rootNode?.Clear();

        _RoleAITempData.target = null;

        _Logic.GetStateMgr()?.Idle();
    }

    //----------------------------------------------API
    public bool IsNoTarget()
    {
        //就算有对象也要重新刷新
        SetTarget();

        return CurTempTarget() == null;
    }

    public void SetTarget()
    {
        //根据检测器, 获取对象
        var targetSenser = _Logic.GetTargetSenserTarget();
        _RoleAITempData.target = targetSenser;
    }

    public bool NoInAtkDistance()
    {
        var myPos = _Logic.GetPositionAnchorPos();
        var targetPos = _RoleAITempData.target.GetPositionAnchorPos();
        var len = Vector3.Distance(myPos, targetPos);
        return len > _Logic.GetAbility().GetATkLen();
    }

    public void MoveToTarget()
    {
        if (_Logic.IsDead() || _RoleAITempData.target == null)
        {
            return;
        }

        //先面向他
        _Logic.LookAtTarget(_RoleAITempData.target.gameObject);
        _Logic.GetStateMgr().MoveToTarget(_RoleAITempData.target.gameObject);
    }

    public bool IsAtkTime()
    {
        return _Logic.GetAbility().IsATKCD();
    }

    public bool IsNoAtk()
    {
        return _Logic.GetStateMgr().IsFight() == false;
    }

    public bool IsAtkState()
    {
        return _Logic.GetStateMgr().IsFight();
    }

    public void ActAtk()
    {
        if (_Logic.IsDead())
        {
            return;
        }

        _Logic.GetStateMgr().Fire();
    }

    public void ActIdle()
    {
        if(_Logic.IsDead())
        {
            return;
        }

        _Logic.GetStateMgr().Idle();
    }
    
    public RoleLogicAddin CurTempTarget()
    {
        if(_RoleAITempData.target && _RoleAITempData.target.IsDead() == false)
        {
            return _RoleAITempData.target;
        }

        return null;
    }
}
