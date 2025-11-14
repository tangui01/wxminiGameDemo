//using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShanBaiSpine : MonoBehaviour
{
    //[SerializeField]
    //SkeletonAnimation spineAnimation;

    private bool isRun = false;
    private Material roleRenderMaterial;

    float fTime = 0.6f;

    //Color curColor = Color.white;

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
                //curColor = Color.white;

                //graphic.color = curColor;
                return;

            }


            //curColor.b += Time.deltaTime * 2;
            //curColor.g += Time.deltaTime * 2;

            //if (curColor.b >= 1.0f)
            //{
            //    curColor.b = 1.0f;
            //    curColor.g = 1.0f;
            //}

            //spineAnimation.color = curColor;

            roleRenderMaterial.SetFloat("_FillPhase", fTime);
        }
    }

    public void Begin()
    {
        //roleRenderMaterial = spineAnimation.GetComponent<Renderer>().material;
        isRun = true;
        //curColor = new Color(1.0f,0.5f,0.5f);
        fTime = 0.6f;

        roleRenderMaterial.SetFloat("_FillPhase", fTime);

        //spineAnimation.color = curColor;
        //roleRenderMaterial.SetColor("_Color", curColor);
    }
}
