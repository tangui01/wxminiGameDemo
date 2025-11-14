using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MajiangMeshItem : MonoBehaviour
{
    [SerializeField]
    int id = 0;

    [SerializeField]
    SpriteRenderer show;

    // Start is called before the first frame update
    void Start()
    {
        if(id == 0)
        {
            id = UnityEngine.Random.Range(11, 19);
        }

        var path = "Altas/Majiang.spriteatlas" + "[" + id + "]";
        //º”‘ÿÕº
        GlobalFunc.LoadSpriteToSpriteRenderer(path, show);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetMJId()
    {
        return id;
    }

    public void SetMJId(int setid)
    {
        id = setid;
    }
}
