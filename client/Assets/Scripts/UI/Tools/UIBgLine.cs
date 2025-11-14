using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBgLine : MonoBehaviour
{
    [SerializeField]
    float moveTimeScale = 0.1f;

    private Material material;
    private Vector2 offset;
    private int mainTexProperty;
    // Start is called before the first frame update
    void Start()
    {
        mainTexProperty = Shader.PropertyToID("_MainTex");
        material = GetComponent<Image>().material;
        offset = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        offset += new Vector2(moveTimeScale * Time.deltaTime, moveTimeScale * Time.deltaTime);
        material.SetTextureOffset(mainTexProperty, offset);
    }
}
