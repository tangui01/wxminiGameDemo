using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlJidi : MonoBehaviour
{
    [SerializeField]
    Transform myJidiInitPos;

    [SerializeField]
    Transform targetJidiInitPos;

    private void OnEnable()
    {
        //先清理所有自介点
        ClearChilds(transform);

        CreateJidi(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 清除父物体下面的所有子物体
    /// </summary>
    /// <param name="parent"></param>
    private void ClearChilds(Transform parent)
    {
        if (parent.childCount > 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateJidi(int shidaiId)
    {
        //创建我方基地
        var key = "Assets/AddressResources/Prefabs/Role/RoleJidi" + shidaiId + ".prefab";

        Action<GameObject> callback = (obj) =>
        {
            var position = myJidiInitPos.transform.position;

            var parent = transform;

            var Obj = GameObject.Instantiate(obj, position, Quaternion.identity, parent);

            //初始化阵营
            var logic = Obj.GetComponent<RoleLogicAddin>();
            Destroy(logic);

            Obj.SetActive(true);
        };

        PlatformMgr.Instance().LoadPrefab(key, callback);

        //创建敌方方基地
        var key_difang = "Assets/AddressResources/Prefabs/Role/RoleJidi" + shidaiId + ".prefab";

        Action<GameObject> callbackDifang = (obj) =>
        {
            var position = targetJidiInitPos.transform.position;

            var parent = transform;

            var Obj = GameObject.Instantiate(obj, position, Quaternion.identity, parent);

            var logic = Obj.GetComponent<RoleLogicAddin>();;
            Destroy(logic);

            Obj.SetActive(true);

            Obj.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        };

        PlatformMgr.Instance().LoadPrefab(key_difang, callbackDifang);
    }
}
