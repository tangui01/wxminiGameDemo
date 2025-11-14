using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightBox : MonoBehaviour
{
    [SerializeField]
    Image jindu;

    [SerializeField]
    int curr_cnt = 0;

    [SerializeField]
    TMP_Text curr_cnt_show;

    [SerializeField]
    GameObject ClickItemPanel;

    [SerializeField]
    GameObject ClickItemTemplate;

    float timeScale = 2.0f;

    float curTime = 0.0f;
    float speed = 1.0f;

    bool isStart = false;

    List<FightClickItem> clickItems = new List<FightClickItem>();

    private void OnEnable()
    {
        clickItems.Clear();
        curTime = 0.0f;
        jindu.fillAmount = 0.0f;
        curr_cnt = 0;
        curr_cnt_show.text = curr_cnt.ToString();

        EventManager.Instance().AddEventListener(sky_mirror.SM_EventType.FightStart, StartFightBox);
        EventManager.Instance().AddEventListener(sky_mirror.SM_EventType.FightOver, StopFightBox);

        CreateClickItem();
    }

    private void OnDisable()
    {
        EventManager.Instance().RemoveEventListener(sky_mirror.SM_EventType.FightStart, StartFightBox);
        EventManager.Instance().RemoveEventListener(sky_mirror.SM_EventType.FightOver, StopFightBox);

        foreach (var item in clickItems)
        {
            Destroy(item.gameObject);
        }

        clickItems.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDestroy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            curTime += Time.deltaTime * speed;

            if(curTime >= timeScale)
            {
                curTime = 0.0f;
                AddCurrency();
            }

            jindu.fillAmount = curTime / timeScale;
        }
    }

    void StartFightBox(string info, string info2, string info3)
    {
        isStart = true;

        curTime = 0.0f;
    }

    void StopFightBox(string info, string info2, string info3)
    {
        isStart = false;
    }

    void AddCurrency()
    {
        curr_cnt++;
        curr_cnt_show.text = curr_cnt.ToString();

        foreach (var item in clickItems)
        {
            item.SetClickState(curr_cnt);
        }
    }

    public bool CheckCnt(int need)
    {
        return curr_cnt >= need;
    }

    public void SubCnt(int need)
    {
        curr_cnt-= need;
        curr_cnt_show.text = curr_cnt.ToString();

        foreach (var item in clickItems)
        {
            item.SetClickState(curr_cnt);
        }
    }

    public void CreateClickItem()
    {
        var item = GameObject.Instantiate(ClickItemTemplate, ClickItemPanel.transform);
        item.SetActive(true);

        var _clickItem = item.GetComponent<FightClickItem>();
        _clickItem.ResetConfig(1, this);

        _clickItem.SetClickState(curr_cnt);

        clickItems.Add(_clickItem);
    }
    

}
