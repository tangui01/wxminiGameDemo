using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trun : MonoBehaviour
{
    // public Image obj;
    [SerializeField]
    float speed = 20.0f;

    [SerializeField]
    Vector3 dir = Vector3.back;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var step = dir * speed * Time.deltaTime;
        this.transform.Rotate(step);
    }
}
