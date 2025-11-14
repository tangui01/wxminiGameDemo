using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadDropPropAddin : MonoBehaviour
{
    [SerializeField]
    GameObject propObj;

    [SerializeField]
    GameObject _parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropPorp()
    {
        var newObj = GameObject.Instantiate(propObj, gameObject.transform.position, Quaternion.identity, _parent.transform);
        newObj.SetActive(true);
    }
}
