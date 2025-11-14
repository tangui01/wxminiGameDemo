//using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShanBaiImage : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer show;

    private bool isRun = false;
    //private Material roleRenderMaterial;

    float fTime = 1.0f;

    Color curColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            fTime -= Time.deltaTime;

            if(fTime <= 0.0f)
            {
                fTime = 0.0f;
                isRun = false;
                curColor = Color.white;

                show.color = curColor;
                return;

            }

            curColor.b += Time.deltaTime * 2;
            curColor.g += Time.deltaTime * 2;
            if (curColor.b >= 1.0f)
            {
                curColor.b = 1.0f;
            }

            if (curColor.g >= 1.0f)
            {
                curColor.g = 1.0f;
            }

            show.color = curColor;
        }
    }

    public void Begin()
    {
        //roleRenderMaterial = graphic.material;
        isRun = true;
        curColor = Color.red;
        fTime = 1.0f;

        show.color = curColor;
        //roleRenderMaterial.SetColor("_Color", curColor);
    }
}
