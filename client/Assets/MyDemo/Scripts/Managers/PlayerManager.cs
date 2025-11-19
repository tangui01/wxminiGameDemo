using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyDemo 
{ 
public class PlayerManager : SingletonMonoBase<PlayerManager>
{
    [SerializeField]private Player player;
    [SerializeField] private Vector3 playerSpawnPosition;
    public Player Player => player;

    public void InitPlayer()
    {
        PoolManager.Instance.FromPoolGetGameObject("Player", (obj) =>
        {
            player= obj.GetComponent<Player>();
            player.Init();
        });
        
    }
    public void GameOver()
    {
        player = null;
    }
}
}
