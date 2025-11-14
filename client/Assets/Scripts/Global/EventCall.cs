using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCall : MonoBehaviour
{
    [SerializeField]
    sky_mirror.SM_EventType eventType;

    [SerializeField]
    UnityEvent eventCall;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance().AddEventListener(eventType, ResetEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventManager.Instance().RemoveEventListener(eventType, ResetEvent);
    }

    public void ResetEvent(string info1, string info2, string info3)
    {
        eventCall.Invoke();
    }
}
