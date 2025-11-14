using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MajiangItem : MonoBehaviour
{
    [SerializeField]
    int id = 0;

    [SerializeField]
    Image show;

    // Start is called before the first frame update
    void Start()
    {
        var path = "Altas/Majiang.spriteatlas" + "[" + id + "]";
        //¼ÓÔØÍ¼
        GlobalFunc.LoadSpriteToImage(path, show);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMJId(int setid)
    {
        id = setid;
    }

    public int GetMJId()
    {
        return id;
    }

    public void FlyToTarget(MajiangChoosePanel choosePanel)
    {
        transform.DOMove(choosePanel.GetNextPosTarget().transform.position, 0.5f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            choosePanel.MoveToOK();

        });

        choosePanel.PushMj(this);
    }

    public void Clear()
    {
        Destroy(gameObject);
    }


}
