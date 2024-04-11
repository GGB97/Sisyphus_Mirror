using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance; // 필요하지 않게될 수 있음.
    Transform target; // 나중에 GameManager 같은데서 들고있으면 거기서 가져오게 하면 될듯

    // 등장 가능 몬스터 설정해야함.
    [SerializeField] WaveSO waveData; // 장비 아이템에 의해 몬스터 수량같은게 조절될 가능성도 있을 수 있음
    WaveSO stageModifier;                   // 그럼 enemy스탯 관리하듯이 하면 될듯 

    // 최대 몬스터 수?
    [SerializeField] int maxEnemyCnt;
    [SerializeField] int currentEnemyCnt;
    [SerializeField] int numPerSpawn;

    [SerializeField] Transform plane;
    Vector3 bottomLeft;
    Vector3 topRight;

    private struct size
    {
        public float radius;
        public float height;

        public void Init(float radius, float height)
        {
            this.radius = radius;
            this.height = height;
        }
    }
    size[] _size = new size[(int)EnemySize.Boss + 1];
    LayerMask _targetLayer;

    private void Awake()
    {
        Instance = this;

        stageModifier = new();

        EnemyPooler.Instance.SetPool(waveData);

        target = GameManager.Instance.Player.transform;
        _targetLayer = LayerData.Player | LayerData.Enemy;

        _size[(int)EnemySize.Small].Init(0.5f, 1.4f);
        _size[(int)EnemySize.Medium].Init(1f, 2f);
        _size[(int)EnemySize.Large].Init(1f, 2.5f);
        _size[(int)EnemySize.Boss].Init(2f, 4f);

        Init();
    }

    private void Start()
    {
        target = GameManager.Instance.Player.transform;
    }

    void Init()
    {
        stageModifier.ModifierInit();

        maxEnemyCnt = waveData.maxEnemyCnt + stageModifier.maxEnemyCnt;
        if (maxEnemyCnt > 100)
        {
            maxEnemyCnt = 100;
        }

        numPerSpawn = waveData.numPerSpawn + stageModifier.numPerSpawn;
        if (numPerSpawn > (int)(maxEnemyCnt * 0.2f))
        {
            numPerSpawn = (int)(maxEnemyCnt * 0.2f);
        }

        currentEnemyCnt = 0;
    }

    void SetSpawnPos()
    {
        // Plane의 스케일을 기준으로 실제 크기 계산 Plane은 기본 10x10 크기
        float width = 45f;
        float length = 45f;

        // 좌측 하단과 우측 상단 좌표 계산
        bottomLeft = new Vector3(-width / 2, 0, -length / 2);
        topRight = new Vector3(width / 2, 0, length / 2);
    }

    IEnumerator SpawnStart()
    {
        Init();

        WaitForSeconds delay = new(waveData.spawnDelay);

        SetSpawnPos();

        yield return delay;

        // 보스 스테이지 일때는 시작시 보스 스폰하고 시작하면 될듯
        if (DungeonManager.Instance.currnetstage % 1 == 0)
            SpawnEnemy(waveData.boss);
        // --

        while (DungeonManager.Instance.gameState == GameState.Playing)
        {
            if (currentEnemyCnt < maxEnemyCnt)
            {
                for (int i = 0; i < numPerSpawn; i++)
                {
                    // 특정 확률에 의해 Normal/Elite 결정
                    float randomValue = Random.Range(0f, 100f);
                    if (randomValue < waveData.eliteSpawnChance)
                        SpawnEnemy(waveData.elite);
                    else
                        SpawnEnemy(waveData.normal);

                    if (currentEnemyCnt >= maxEnemyCnt)
                        break;
                }
            }
            yield return delay;
        }
    }

    bool GetSpawnPos(int id, ref Vector3 randomPos)
    {
        int cnt = 0;
        while (true)
        {
            if (cnt >= 100) // 무한루프 방지
            {
                Debug.Log("Can't Spawn");
                return false;
            }

            randomPos = new Vector3(Random.Range(bottomLeft.x, topRight.x), 0, Random.Range(bottomLeft.z, topRight.z));
            //유효성 검사
            EnemySize size = DataBase.EnemyStats.Get(id).size;
            Vector3 yVector = new(0, _size[(int)size].height / 2, 0);

            Collider[] colliders = Physics.OverlapSphere(randomPos + yVector, _size[(int)size].radius, _targetLayer);
            if (colliders.Length == 0)
            {
                return true;
            }
            else
            {
                //Debug.Log("Can't use this Position");
            }
            ++cnt;
        }
    }

    void SpawnEnemy(int[] enemyID)
    {
        int rand = Random.Range(0, enemyID.Length);
        Vector3 pos = Vector3.zero;
        bool isSpawn = GetSpawnPos(enemyID[rand], ref pos);

        if (isSpawn)
        {
            EnemyPooler.Instance.SpawnFromPool(enemyID[rand], pos, Quaternion.identity);
            currentEnemyCnt++;
        }
    }

    public void SpawnStop() // 스테이지 종료될때 호출하면 될듯
    {
        // 스테이지 종료 시 Enemy가 다 사라져야 한다면?
        // Envent 하나 만들고 Enemy Awake시 비활성화 되는 함수를 구독 -> 이벤트 실행 하면 될듯
        StopCoroutine(SpawnStart());
    }

    public void IncrementEnemyCnt()
    {
        currentEnemyCnt++;
    }
    public void DecrementEnemyCnt()
    {
        currentEnemyCnt--;
    }

    public void FindAllEnemiesDeSpawn() // Test용
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.DeSpawn();
        }
    }
    public void GameStart()
    {
        StartCoroutine(SpawnStart());
    }
}
