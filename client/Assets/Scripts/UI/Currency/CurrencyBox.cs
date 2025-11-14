using sky_mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyBox : MonoBehaviour
{
    [SerializeField]
    CurrencyEnum _enum = CurrencyEnum.Jin;

    [SerializeField]
    TMP_Text cntShow;

    [SerializeField]
    ScaleOut addAnima;

    // Start is called before the first frame update
    void Start()
    {
        ResetCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetCurrency(bool isAnima = false)
    {
        var cnt = PlayerData.GetCurrency().GetValue(_enum);
        cntShow.text = cnt.ToString();

        if (isAnima)
        {
            addAnima.StartAnima();
        }
    }


}
