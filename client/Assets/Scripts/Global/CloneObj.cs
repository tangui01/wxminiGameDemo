using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CloneObj : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    public int x_max;
    public int y_max;

    public float jx = 1.0f;

    public string GameObjName = "obj_";

    void Start()
    {
    }

#if UNITY_EDITOR
    [MenuItem("CONTEXT/CloneObj/build")]
    public static void Build(MenuCommand menuCommand)
    {
        var class_obj = menuCommand.context as CloneObj;

        var intCount = 0;
        for (int x = 0; x < class_obj.x_max; x++)
        {
            for (int y = 0; y < class_obj.y_max; y++)
            {

                GameObject go = GameObject.Instantiate(class_obj.parent, class_obj.gameObject.transform);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * class_obj.jx, y * class_obj.jx);
                intCount++;
                go.name = class_obj.GameObjName + intCount;
            }
        }
    }
#endif


    // Update is called once per frame
    void Update()
    {
        
    }
}
