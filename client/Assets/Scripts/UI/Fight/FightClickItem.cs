using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightClickItem : MonoBehaviour
{
    [SerializeField]
    Image btn;

    [SerializeField]
    Image icon;

    int _roleid = 1;

    FightBox _fightBox= null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (FightSceneAddin.Instance().IsOver())
        {
            return;
        }

        //先看看是否有钱
        var need = 3;

        var isOK = _fightBox.CheckCnt(need);

        if(isOK)
        {
            btn.GetComponent<ScaleOut>().StartAnima();
            _fightBox.SubCnt(need);


            //创建
            FightSceneAddin.Instance().GetMySoldierFactory().CreateSoldier();
        }
    }

    public void ResetConfig(int roleid, FightBox fightbox)
    {
        _roleid = roleid;
        _fightBox = fightbox;
    }

    public void SetClickState(int curCnt)
    {
        var need = 3;
        bool state = curCnt >= need;

        if (state)
        {
            //可以点
            btn.color = Color.white;
            icon.color = Color.white;
        }
        else
        {
            //不可以点
            btn.color = Color.gray;
            icon.color = Color.gray;
        }
    }
}

