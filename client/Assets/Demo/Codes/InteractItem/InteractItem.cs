using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    Scope,
    GreenDiamondAdditive,
    SilverCoinAdditive
}

public class InteractItem : MonoBehaviour
{
    [HideInInspector]
    public InteractType type;
    protected PlayerController player;
    protected bool CanInteract = false;
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.transform.tag != "Player") return;
        player = other.GetComponent<PlayerController>();
        CanInteract = player.CanInteract;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != "Player") return;
        CanInteract = false;
    }

}

