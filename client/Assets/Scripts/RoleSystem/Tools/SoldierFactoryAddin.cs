using sky_mirror;
using UnityEngine;

public class SoldierFactoryAddin : MonoBehaviour
{
    [SerializeField, Tooltip("**工厂阵营**")]
    sky_mirror.Camp _curCamp = Camp.My;//阵营

    //[SerializeField, Tooltip("**士兵出生位置**")]
    //Transform initPos;

    //[SerializeField, Tooltip("**士兵父节点**")]
    //Transform parentTrans;

    [SerializeField, Tooltip("**时间创建**")]
    float createTime = 0.0f;

    //[SerializeField, Tooltip("**士兵模板**")]
    //GameObject SoldierObj;

    float curTime = 0.0f;

    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStart)
        {
            return;
        }

        if(curTime > 0.0f)
        {
            curTime -= Time.deltaTime;

            if(curTime <= 0.0f)
            {
                CreateSoldier();
                curTime = createTime;
            }
        }
    }

    public void CreateSoldier()
    {
        if(FightSceneAddin.Instance().IsOver())
        {
            return;
        }

        RoleMgrAddin.Instance().CreateRole(1, _curCamp);
        //var newRole = GameObject.Instantiate(SoldierObj, initPos.position, Quaternion.identity, parentTrans);
        //newRole.SetActive(true);

    }

    public void StartFactory(float setTime = 0.0f)
    {
        if(setTime > 0)
        {
            createTime = setTime;
        }
        
        isStart = true;
        curTime = createTime;
    }

    public void StopFactory()
    {
        isStart = false;
    }

    //public void 
}
