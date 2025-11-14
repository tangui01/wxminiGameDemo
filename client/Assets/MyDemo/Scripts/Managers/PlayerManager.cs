using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyDemo 
{ 
public class PlayerManager : SingletonMonoBase<PlayerManager>
{
    [SerializeField]private Player player;
    [SerializeField] private GameObject playerPrefab;
    
    public Player Player => player;

    public void InitPlayer()
    {
        player = Instantiate(playerPrefab).GetComponent<Player>();
        player.Init();
    }
    public void GameOver()
    {
        player = null;
    }
}
}
