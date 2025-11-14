using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartCreatorMajiang : MonoBehaviour
{
    [SerializeField]
    GameObject majiang;

    [SerializeField]
    int cnt = 10;

    int curIndex = 0;

    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 1000;
    }

    private void OnEnable()
    {
        StartMajiang();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            var mj = GameObject.Instantiate(majiang, transform);
            mj.GetComponent<MajiangMeshItem>().SetMJId(0);
            curIndex++;

            if(curIndex >= cnt)
            {
                curIndex = 0;
                isStart = false;
            }
        }
    }

    public void StartMajiang()
    {
        isStart = true;
    }
}
