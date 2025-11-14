using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayDelete : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        delayTime -= Time.deltaTime;
        if(delayTime<=0.0f)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
