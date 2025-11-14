using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalLangreset : MonoBehaviour
{
    //[SerializeField]
    Text txtObj = null;

    //[SerializeField]
    TMP_Text tmptxtObj = null;

    TextMesh tmtxtObj = null;

    // Start is called before the first frame update
    void Start()
    {
        // 获取当前系统的本地化设置
        var lan = Application.systemLanguage;

        ResetText(lan);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetText(SystemLanguage lan)
    {
        if(txtObj == null)
        {
            txtObj = GetComponent<Text>();
        }

        if(txtObj)
        {
            var key = txtObj.text;
            var newtext = LangConfig.Instance().GetLand(key, lan);
            txtObj.text= newtext;
        }

        if (tmptxtObj == null)
        {
            tmptxtObj = GetComponent<TMP_Text>();
        }

        if (tmptxtObj)
        {
            var key = tmptxtObj.text;
            var newtext = LangConfig.Instance().GetLand(key, lan);
            tmptxtObj.text = newtext;
        }

        if (tmtxtObj == null)
        {
            tmtxtObj = GetComponent<TextMesh>();
        }

        if (tmtxtObj)
        {
            var key = tmtxtObj.text;
            var newtext = LangConfig.Instance().GetLand(key, lan);
            tmtxtObj.text = newtext;
        }
        
    }
}
