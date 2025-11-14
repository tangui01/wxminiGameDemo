using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCurrency : MonoBehaviour
{
    [SerializeField]
    sky_mirror.CurrencyEnum _enum;

    [SerializeField]
    int cnt = 0;

    [SerializeField]
    GameObject target;

    [SerializeField]
    float speed = 3.0f;

    [SerializeField]
    FightAddBox fightAdd;

    // Start is called before the first frame update
    void Start()
    {
        MoveToTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToTarget()
    {
        if (target == null)
        {
            return;
        }

        var targetRect = target.transform;
        var myPos = transform;

        Vector3[] array = new Vector3[2];
        array[0] = new Vector3(myPos.position.x + Random.Range(-10, 10) * 0.2f, myPos.position.y + Random.Range(-10, 10) * 0.2f);
        array[1] = targetRect.position;

        transform.DOPath(array, speed, PathType.CatmullRom).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            MoveToOk();
            GameObject.Destroy(gameObject);
        });
    }

    public void MoveToOk()
    {
        fightAdd.AddValue(cnt, true);
    }
}
