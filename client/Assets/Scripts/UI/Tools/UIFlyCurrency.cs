using DG.Tweening;
using sky_mirror;
using UnityEngine;


public class UIFlyCurrency : MonoBehaviour
{
    private GameObject flyTarget;

    private CurrencyEnum _enum;
    private int _cnt;

    float delay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        var sound = transform.Find("Sound");
        if (sound)
        {
            var isSound = PlayerData.GetDataForId<Setup>(DataEnum.Setup).IsSound();
            if (isSound)
            {
                sound.GetComponent<AudioSource>().Play();
            }
        }

        delay = Random.Range(1, 10) * 0.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(delay > 0.0f)
        {
            delay -= Time.deltaTime;

            if(delay <= 0.0f)
            {
                FlyTo();
            }
        }
        
    }

    public void FlyTo()
    {
        if (flyTarget == null)
        {
            return;
        }

        var targetRect = flyTarget.GetComponent<RectTransform>();
        var myPos = GetComponent<RectTransform>();

        //Vector3[] array = new Vector3[2];
        //array[0] = new Vector3(myPos.position.x + Random.Range(-10, 10) * 0.2f, myPos.position.y + Random.Range(-10, 10) * 0.2f);
        //array[1] = targetRect.position;


        //transform.DOPath(array, 1.5f, PathType.CatmullRom).SetEase(Ease.InOutCubic).OnComplete(() =>
        //{
        //    //飞到了,就加上去
        //    PlayerData.GetCurrency().AddValue(_enum, _cnt, true);

        //    GameObject.Destroy(gameObject);
        //});
        transform.DOMove(targetRect.position, 1.0f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            //飞到了,就加上去
            PlayerData.GetCurrency().AddValue(_enum, _cnt, true);

            GameObject.Destroy(gameObject);
        });
    }

    public void FiyInitPos(Vector2 pos)
    {
        transform.DOMove(pos, 0.3f).SetEase(Ease.InOutCubic);
    }

    public void SetFlyTarget(GameObject target, CurrencyEnum em, int cnt)
    {
        _enum = em;
        _cnt = cnt;
        flyTarget = target;
    }
}
