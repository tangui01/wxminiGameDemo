using DG.Tweening;
using System.Collections;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


public class Enter : MonoBehaviour
{
    [SerializeField]
    private string loadSceneName;

    [SerializeField]
    private UnityEngine.UI.Image _loading;

    private SceneInstance asyncOperation1callop1;

    private float targetFillAmount = 0.0f;

    void Start()
    {
        
        //
        Application.targetFrameRate = 60;
        Addressables.InitializeAsync();

        NextScene();

        Debug.Log("TestHelper:" + HelperMgr.Instance().GetHelper<TestHelper>().GetValue(2));
    }

    // Update is called once per frame
    void Update()
    {
        var curFill = _loading.fillAmount;
        if (curFill < targetFillAmount)
        {
            curFill += Time.deltaTime * 1.5f;

            if(curFill >= targetFillAmount)
            {
                curFill = targetFillAmount;

                if (targetFillAmount == 1.0f)
                {
                    asyncOperation1callop1.ActivateAsync();
                }
            }

            _loading.fillAmount = curFill;
        }
    }

    IEnumerator Load(string name)
    {
        //var handle = Addressables.LoadSceneAsync("Assets/AddressResources/Scenes/" + name + ".unity", LoadSceneMode.Single, false);
        var handle = Addressables.LoadSceneAsync(name, LoadSceneMode.Single, false);

        handle.Completed += (obj) =>
        {
            asyncOperation1callop1 = handle.Result;

            targetFillAmount = 1.0f;
        };

        yield return null;
    }

    public void LoadSceneAsync(string name)
    {
        StartCoroutine(Load(name));
    }

    public void NextScene()
    {
        LoadSceneAsync(loadSceneName);
    }
}
