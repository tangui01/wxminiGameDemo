using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class UIStateController : MonoBehaviour
{
    public void GenerateTeamHpState(List<BaseController> controllers, GameObject hpStatePref)
    {
        foreach(var target in controllers)
        {
            //var state = GameObject.Instantiate<GameObject>(hpStatePref, transform).GetComponent<HpState>();
            //state.Init(target);
        }

    }
}
