using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBox : MonoBehaviour
{
    [SerializeField]
    Image _shadow;

    [SerializeField]
    Image _cur;

    [SerializeField]
    TMP_Text _value;

    bool isRunAnima = false;

    bool isShowValue = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunAnima)
        {
            var jg = _cur.fillAmount - _shadow.fillAmount;

            var len = MathF.Abs(jg);
            if (len >= 0.01f)
            {
                var step = ((jg / len) * jg) * Time.deltaTime * 5;

                if(step <= 0.008f)
                {
                    step = 0.008f;
                }

                _shadow.fillAmount -= step;
            }
            else
            {
                _shadow.fillAmount = _cur.fillAmount;
                isRunAnima = false;

                //if (_shadow.fillAmount <= 0.0f)
                //{
                //    °Ñ×Ô¼ºÒÆ³ý
                //    EventManager.Instance().EventTrigger(sky_mirror.SM_EventType.RemoveHPBox, gameObject.name);
                //}
            }    
        }
    }

    public void SetValueShow(bool isShow)
    {
        isShowValue = isShow;
    }

    public void Reset(float cur, float max)
    {
        var benfen = cur / max;

        _cur.fillAmount= benfen;

        if(isShowValue)
        {
            _value.gameObject.SetActive(true);
            _value.text = cur.ToString();
        }else
        {
            _value.gameObject.SetActive(false);
        }

        isRunAnima = true;

        gameObject.SetActive(true);
    }

    public void SetCamp(bool isMy)
    {
        if(isMy)
        {
            _cur.fillOrigin= (int)Image.OriginHorizontal.Left;
            _shadow.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
        else
        {
            _cur.fillOrigin = (int)Image.OriginHorizontal.Right;
            _shadow.fillOrigin = (int)Image.OriginHorizontal.Right;
        }
    }
}
