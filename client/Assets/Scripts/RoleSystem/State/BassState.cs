using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BassState
{
    public StateManager _mgr;
    public bool isResetInit = false;
    public abstract void OnEnter();
    public abstract void OnExit();

    public abstract void OnUpdate();

    public abstract void ReseInit(BassState other);
    
}
