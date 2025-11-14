using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StartGame : MonoBehaviour
{
    public void OnStartGameClick()
    {
        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        var handle = Addressables.LoadSceneAsync("GameScene", UnityEngine.SceneManagement.LoadSceneMode.Single, true);
        yield return null;
    }
}
