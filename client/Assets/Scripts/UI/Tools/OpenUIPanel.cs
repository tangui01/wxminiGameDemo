using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class OpenUIPanel : MonoBehaviour
{
    [SerializeField]
    AssetReference UIPrefabs;

    //[SerializeField]
    GameObject _parent;
    // Start is called before the first frame update
    void Start()
    {
        _parent = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOpen()
    {
        //看是否已经弹了
        var IsHave = GameObject.Find("Canvas/" + UIPrefabs.AssetGUID);
        if(IsHave)
        {
            return;
        }

        var handle = UIPrefabs.InstantiateAsync(Vector3.zero, Quaternion.identity, _parent.transform);

        handle.Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                var uipanel = obj.Result;

                var IsHave = GameObject.Find("Canvas/" + UIPrefabs.AssetGUID);
                if (IsHave)
                {
                    GameObject.Destroy(uipanel);
                    return;
                }

                var rect = uipanel.GetComponent<RectTransform>();
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;

                uipanel.name = UIPrefabs.AssetGUID;
            }
        };
    }


}
