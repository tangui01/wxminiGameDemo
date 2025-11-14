using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnimationEventAndCall : MonoBehaviour
{
    [SerializeField]
    private UnityEvent finishCall;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EventCall()
    {
        finishCall.Invoke();
    }
}
