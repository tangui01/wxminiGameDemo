using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadShowAddin : MonoBehaviour
{
    [SerializeField]
    string path = string.Empty;

    [SerializeField]
    Vector3 off;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNow()
    {
        EffectPrefabsAddin.Instance().CreateEffect(path, transform.position + off);
    }
}
