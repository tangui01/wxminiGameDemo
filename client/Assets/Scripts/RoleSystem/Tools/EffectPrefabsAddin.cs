using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPrefabsAddin : MonoBehaviour
{
    [SerializeField]
    GameObject[] Prefabs;

    [SerializeField]
    GameObject Panel;

    Dictionary<string, GameObject> _preDict = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in Prefabs)
        {
            _preDict.Add(item.name, item);
        }
    }

    public static EffectPrefabsAddin Instance()
    {
        var obj = GameObject.Find("EffectPrefabsAddin");

        if (obj)
        {
            return obj.GetComponent<EffectPrefabsAddin>();
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEffect(string path, Vector3 pos)
    {
        if(!_preDict.ContainsKey(path))
        {
            return;
        }

        //先看看是否允许
        var prefabObj = _preDict[path];
        var prefab = GameObject.Instantiate(prefabObj, pos, Quaternion.identity, Panel.transform);
        prefab.SetActive(true);

    }
}
