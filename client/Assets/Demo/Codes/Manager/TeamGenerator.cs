using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamGenerator : MonoBehaviour
{
    public TeamType teamType;
    private Transform[] teamPoints;
    private List<BaseController> teamList;
    private string prefName;

    private int teamMemberCount;
    private GameController owner;
    private UIStateController UIState;
    private GameObject teamPref;
    private void Awake()
    {
        //teamPoints = GetComponentsInChildren<Transform>();
        prefName = teamType == TeamType.Red ? "RedTeam" : "BlueTeam";

    }
    public void Init(GameController gameController)
    {
        owner = gameController;
        teamMemberCount = 10;
    }
    public void GenerateTeam(UIStateController stateController)
    {
        UIState = stateController;
        if (teamList == null) teamList = new List<BaseController>();
        StartCoroutine(RealGenerate());

    }

    private IEnumerator RealGenerate()
    {
        var wait = new WaitForSeconds(3f);
        //读取配置表
        var config = HelperMgr.Instance().GetHelper<EnemyHelper>();
        EnemyData enemyUnit;
        GameObject team;
        int count = teamMemberCount;
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < count; i++)
        {
            //随机配置
            var id = UnityEngine.Random.Range(1, 3);
            enemyUnit = config.GetEnemy(id);

            //初始化生成的AI对象信息
            var posX = UnityEngine.Random.Range(5, 10);
            var posZ = UnityEngine.Random.Range(5, 10);
            team = PoolMgr.Instance.Get(prefName,transform); // GameObject.Instantiate<GameObject>(teamPref, transform);
            team.transform.SetParent(transform);
            team.transform.position = new Vector3(player.position.x + posX,player.position.y,player.position.z + posZ);
            var controller = team.GetComponent<AIController>();
            controller.Init(this, enemyUnit, teamType);
            controller.ResetState();

            //初始化血条
            var state = PoolMgr.Instance.Get(AppConst.HpState,UIState.transform);//GameObject.Instantiate<GameObject>(hpStatePref, UIState.transform).GetComponent<HpState>();
            state.GetComponent<HpState>().Init(controller);
            state.SetActive(true);
            
            team.SetActive(true);

            teamList.Add(team.GetComponent<BaseController>());
            yield return wait;
        }

    }

    public void OnActorDead()
    {
        teamMemberCount--;
        if (teamMemberCount <= 0)
            owner.OnTeamDead(teamType);
    }
}
