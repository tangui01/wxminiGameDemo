using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MajiangChoosePanel : MonoBehaviour
{
    [SerializeField]
    GameObject[] Child;

    [SerializeField]
    GameObject losePanel;

    List<MajiangItem> _list = new List<MajiangItem>();

    bool isCanClick = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushMj(MajiangItem mj)
    {
        _list.Add(mj);

        isCanClick = false;
    }

    public void MoveToOK()
    {
        isCanClick = true;

        //检查是否消除
        Dictionary<int, int> CheckDict = new Dictionary<int, int>();

        foreach (var item in _list)
        {
            var id = item.GetMJId();

            if (CheckDict.ContainsKey(id))
            {
                CheckDict[id]++;
            }
            else
            {
                CheckDict[id] = 1;
            }
        }

        //消除3个数量一样的
        foreach (var item in CheckDict)
        {
            if(item.Value == 3)
            {
                for (int i = _list.Count-1; i >= 0; i--)
                {
                    var mj = _list[i];
                    if (mj.GetMJId() == item.Key)
                    {
                        mj.Clear();
                        _list.Remove(mj);
                    }
                }

                //重排
                for (int i = _list.Count - 1; i >= 0; i--)
                {
                    var mj = _list[i];
                    mj.transform.position = Child[i].transform.position;
                }
            }
        }

        if (IsOver())
        {
            Lose();
        }
    }

    public bool IsCanClick()
    {
        return isCanClick;
    }

    public GameObject GetNextPosTarget()
    {
        var len = _list.Count;
        if(len >= Child.Length)
        {
            return null; 
        }

        return Child[len];
    }

    public bool IsOver()
    {
        return _list.Count >= Child.Length;
    }

    public void Lose()
    {
        losePanel.SetActive(true);
    }

}
