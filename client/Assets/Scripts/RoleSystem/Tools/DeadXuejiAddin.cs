using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadXuejiAddin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowXueji()
    {
        RoleMgrAddin.Instance().CreateXueji(gameObject.transform);
    }
}
