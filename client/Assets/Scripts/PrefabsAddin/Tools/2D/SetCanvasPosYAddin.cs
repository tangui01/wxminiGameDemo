using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvasPosYAddin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.y * 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
