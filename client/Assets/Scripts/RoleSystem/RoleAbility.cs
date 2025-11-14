
using UnityEngine;
using System;

[Serializable]
public class Ability
{
    public int lv = 1;

    [HideInInspector]
    public int hp = 10;
    [HideInInspector]
    public float atk = 1;
    [HideInInspector]
    public float def = 1;

    [HideInInspector]
    public int MaxHp = 10;

    public int SrcHp = 10;
    public float SrcAtk = 10;
    public float SrcDef = 10;

    public float atkLen = 100;
    public float atkTime = 0.00f;
    public float moveSpeed= 0.5f;

    [HideInInspector]
    public float _curCDAtkTime = 0.00f;

    public Ability(Ability b)
    {
        lv = b.lv;
        SrcHp = b.SrcHp;
        SrcAtk = b.SrcAtk;
        SrcDef = b.SrcDef;

        Math();
    }

    public Ability()
    {
        lv = 1;
        SrcHp = 10;
        SrcAtk = 1;
        SrcDef = 1;

        Math();
    }
    public void Math()
    {
        hp = SrcHp * lv;
        atk = SrcAtk * lv;
        def = SrcDef * lv;

        MaxHp = hp;
    }

    public bool Injured(float damage)
    {
        //º∆À„’Ê µ…À∫¶
        var trueDamage = damage - (damage * def) / (1200 + def);
        trueDamage = Mathf.Floor(trueDamage);

        if (trueDamage <= 0.0f)
        {
            trueDamage = 1.0f;
        }

        hp -= (int)trueDamage;

        //GlobalFunc.Log("hp: " + hp + " trueDamage:" + trueDamage);

        if (hp <= 0)
        {
            hp = 0;
            return true;
        }

        return false;
    }
}


public class RoleAbility : MonoBehaviour
{
    [SerializeField]
    public Ability myAbility;

    private void Awake()
    {
        myAbility.Math();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myAbility._curCDAtkTime > 0.0f)
        {
            myAbility._curCDAtkTime -= Time.deltaTime;

            if(myAbility._curCDAtkTime <= 0.0f)
            {
                myAbility._curCDAtkTime = 0.0f;
            }
        }
    }

    public float GetATkLen()
    {
        return myAbility.atkLen;
    }

    public float GetMoveSpeed()
    {
        return myAbility.moveSpeed;
    }

    public bool IsATKCD()
    {
        //GlobalFunc.Log("is ATK CD:" + myAbility._curCDAtkTime);
        return myAbility._curCDAtkTime <=0.0f;
    }

    public void RunATKCD()
    {
        myAbility._curCDAtkTime = myAbility.atkTime;
    }

    public bool Injured(float damage)
    {
        return myAbility.Injured(damage);
    }

    public bool IsDead()
    {
        return myAbility.hp <= 0;
    }
}
