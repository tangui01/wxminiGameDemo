using sky_mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FightAddBox : MonoBehaviour
{
    //[SerializeField]
    CurrencyEnum _enum = CurrencyEnum.Jin;

    [SerializeField]
    TMP_Text curr_cnt_show;

    [SerializeField]
    ScaleOut addAnima;

    int addValue = 0;

    private void OnEnable()
    {
        addValue = 0;
        ResetValue(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddValue(int add, bool isAnima)
    {
        addValue += add;

        ResetValue(isAnima);
    }

    public void ResetValue(bool isAnima)
    {
        curr_cnt_show.text = addValue.ToString();

        if(isAnima)
        {
            addAnima.StartAnima();
        }
    }

    public int GetValue()
    {
        return addValue;
    }
}
