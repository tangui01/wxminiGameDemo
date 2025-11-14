using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUMaterialBlock : MonoBehaviour
{
    Renderer render;
    private MaterialPropertyBlock prop;

    private bool isRun = true;

    void Start()
    {
        prop = new MaterialPropertyBlock();
        render = gameObject.GetComponent<Renderer>();
        render.GetPropertyBlock(prop);
        render.SetPropertyBlock(prop);
        isRun = true;
    }

    private void Update()
    {
        //if(isRun)
        //{
        //    render.SetPropertyBlock(prop);
        //    isRun = false;
        //}
    }
    
    
}
