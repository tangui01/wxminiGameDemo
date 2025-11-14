using sky_mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLock : MonoBehaviour
{
    [SerializeField]
    LockEnum _lockEnum;

    [SerializeField]
    UIMenuItem show;

    [SerializeField]
    GameObject _lockShow;

    // Start is called before the first frame update
    void Start()
    {
        CheckLockState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckLockState()
    {
        switch (_lockEnum)
        {
            case LockEnum.Fuben:
                {
                    show.LockItem();
                    _lockShow.SetActive(true);
                }
                break;
            case LockEnum.Tishen:
                break;
            case LockEnum.Fight:
                break;
            case LockEnum.Kapai:
                {
                    show.LockItem();
                    _lockShow.SetActive(true);
                }
                break;
            case LockEnum.Shop:
                {
                    show.LockItem();
                    _lockShow.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public void ShowLockTips()
    {

    }
    
}
