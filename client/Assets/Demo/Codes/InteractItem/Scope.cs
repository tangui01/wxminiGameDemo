using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : InteractItem
{
    private bool inScope = false;
    private bool initOnce = false;

    private void Update()
    {
        if (!CanInteract) return;
        if(initOnce && Input.GetMouseButtonUp(0))
        {
            inScope = !inScope;
            Camera.main.GetComponent<CameraController>().DoScopeCamera(inScope);
            player.CanControl = !inScope;
        }
        if(!initOnce)
        {
            Camera.main.GetComponent<CameraController>().DoScopeCamera(true);
            player.CanControl = false;
            inScope = true;
            initOnce = true;
        }   
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        inScope = false;
        initOnce = false;
    }
}
