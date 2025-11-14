using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

public enum TeamType
{ 
    Red,
    Blue
}
public class GameController : MonoBehaviour
{
    public TeamGenerator redGenerator;
    public Transform playerLocation;
    public UIStateController stateController;

    private void Start()
    {
        if(stateController == null)
        {
            Debug.LogError("获取UI状态管理失败");
            return;
        }

        //生成主角
        Action<GameObject> callback = (obj) => { 
            var playerPref = obj;
            var player = GameObject.Instantiate<GameObject>(playerPref);
            player.transform.position = playerLocation.position;
            player.GetComponent<PlayerController>().Init(this);
            player.SetActive(true);

            //生成红队
            redGenerator.Init(this);
            redGenerator.GenerateTeam(stateController);
        };
        
        PlatformMgr.Instance().LoadPrefab("Player", callback);

    }
    /// <summary>
    /// 某个队伍全死亡
    /// </summary>
    /// <param name="teamType"></param>
    public void OnTeamDead(TeamType teamType)
    {
        if (teamType == TeamType.Red)
            GameOver(true);
    }
    private void GameOver(bool isWin)
    {
        StartCoroutine(LoadStartScene());
    }
    private IEnumerator LoadStartScene()
    {
        yield return new WaitForSeconds(3f);

        var handle = Addressables.LoadSceneAsync("StartScene", UnityEngine.SceneManagement.LoadSceneMode.Single, true);
    }

    internal void OnPlayerDead()
    {
        GameOver(false);
    }
}
