using sky_mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenserBodyAddin : MonoBehaviour
{
    [SerializeField]
    RoleLogicAddin _logic;

    // Start is called before the first frame update
    void Start()
    {
        //根据阵营,修改自己layer
        var camp = _logic.GetCamp();
        switch (camp)
        {
            case Camp.My:
                gameObject.layer = LayerMask.NameToLayer("Camp-My");
                break;
            case Camp.Target:
                gameObject.layer = LayerMask.NameToLayer("Camp-Target");
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RoleLogicAddin GetRoleLogic()
    {
        return _logic;
    }
}
