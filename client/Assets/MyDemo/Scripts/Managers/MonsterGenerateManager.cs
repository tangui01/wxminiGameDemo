using UnityEngine;
using Random = UnityEngine.Random;
namespace MyDemo
{
    /// <summary>
    /// 怪物产生管理器
    /// </summary>
    public class MonsterGenerateManager : SingletonMonoBase<MonsterGenerateManager>
    {
        [SerializeField] private float monsterSpawnTimer;//怪物产生间隔时间
        [SerializeField] private GameObject monsterPrefab;

        [SerializeField] private float monsterMaxCount;
        private int monsterCount;

        private float currentSpawnTime;

        [SerializeField] private float spawnMinRange;
        [SerializeField] private float spawnMaxRange;

        private Player _player;

   

        public void Init()
        {
            _player = PlayerManager.Instance.Player;
           
        }

        private void Update()
        {
            if (GameManager.Instance.IsGameOver) return;
            currentSpawnTime += Time.deltaTime;
            if (currentSpawnTime >= monsterSpawnTimer)
            {
                currentSpawnTime = 0f;
                //生产一个怪物
                Monster monster = PoolManager.Instance.FromPoolGetGameObject("Monster", monsterPrefab).GetComponent<Monster>();
                monster.transform.position = GetRandomSpawnPoint() + _player.GetPosition();
                var data = HelperMgr.Instance().GetHelper<MonsterHelper>();
                monster.Init(data.GetMonster(1));
            }
        }

        private Vector3 GetRandomSpawnPoint()
        {
            float x = Random.Range(spawnMinRange, spawnMaxRange);
            float y = Random.Range(spawnMinRange, spawnMaxRange);
            if (Random.Range(0, 100) >= 50)
            {
                x = -x;
            }
            else if (Random.Range(0, 100) >= 50)
            {
                y = -y;
            }
            return new Vector3(x, y, 0);
        }

        public void Clear()
        {
            currentSpawnTime = 0;
            monsterCount = 0;
        }
    }
}