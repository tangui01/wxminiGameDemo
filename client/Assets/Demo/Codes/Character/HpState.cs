using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpState : MonoBehaviour,IPoolItem
{
    private BaseController owner;
    public Image imgBlood;
    private Vector3 scPoint;
    public void Init(BaseController controller)
    {
        owner = controller;
        controller.RegisterHpUpdate(Refresh);
        Refresh();
    }

    
    private void Refresh()
    {
        if (owner.Hp <= 0)
        {
            owner.RemoveHpUpdate(Refresh);
            PoolMgr.Instance.Push(AppConst.HpState, gameObject);
            return;
        }
        imgBlood.fillAmount = owner.Hp / owner.MaxHp;
    }
    private void Update()
    {
        scPoint = Camera.main.WorldToScreenPoint(owner.transform.position);
        scPoint.y += 100;
        transform.position = scPoint;
    }

    public void ResetState()
    {
        
    }
}
