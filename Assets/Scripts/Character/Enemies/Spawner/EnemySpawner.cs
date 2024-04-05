using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance; // 필요하지 않게될 수 있음.
    Transform target; // 나중에 GameManager 같은데서 들고있으면 거기서 가져오게 하면 될듯

    // 등장 가능 몬스터 설정해야함.
    [SerializeField] WaveSO waveData; // 장비 아이템에 의해 몬스터 수량같은게 조절될 가능성도 있을 수 있음
                                      // 그럼 enemy스탯 관리하듯이 하면 될듯 

    // 최대 몬스터 수?
    [SerializeField] int maxEnemyCnt;
    [SerializeField] int currentEnemyCnt;

    [SerializeField] Transform plane;
    Vector3 bottomLeft;
    Vector3 topRight;

    private void Awake()
    {
        Instance = this;

        maxEnemyCnt = waveData.maxEnemyCnt;
        currentEnemyCnt = 0;
        EnemyPooler.Instance.SetPool(waveData);

        target = GameManager.Instance.Player.transform;
    }

    private void Start()
    {
        target = GameManager.Instance.Player.transform;
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
        int spawnCnt = waveData.numPerSpawn;
        WaitForSeconds delay = new(waveData.spawnDelay);

        SetSpawnPos();

        yield return delay;

        // 보스 스테이지 일때는 시작시 보스 스폰하고 시작하면 될듯
        if(DungeonManager.Instance.currnetstage % 1 == 0)
            SpawnEnemy(new Vector3(0, 0, -10), waveData.boss);
        // --

        while (DungeonManager.Instance.isStarted) // 게임 종료 검사로 변경 필요함
        {
            if (currentEnemyCnt < maxEnemyCnt)
            {
                for (int i = 0; i < spawnCnt; i++)
                {
                    Vector3 pos = GetSpawnPos(); // Enemy가 생성될 위치

                    // 여기서 pos의 위치에 Spawn이 가능한지 검사 필요성이 생긴다면 추가 예정
                    // --

                    // 특정 확률에 의해 Normal/Elite 생성
                    float randomValue = Random.Range(0f, 100f);
                    if (randomValue < waveData.eliteSpawnChance)
                        SpawnEnemy(pos, waveData.elite);
                    else
                        SpawnEnemy(pos, waveData.normal);

                    if (currentEnemyCnt >= maxEnemyCnt)
                        break;
                }
            }
            yield return delay;
        }
    }

    Vector3 GetSpawnPos()
    {
        //NavMeshHit hit;
        //// NavMesh 상에서 무작위 위치를 가져옵니다.
        //NavMesh.SamplePosition(Vector3.zero, out hit, 45f, NavMesh.AllAreas);
        //return hit.position;

        return new Vector3(Random.Range(bottomLeft.x, topRight.x), 0, Random.Range(bottomLeft.z, topRight.z));
    }

    void SpawnEnemy(Vector3 pos , int[] enemyID)
    {
        int rand = Random.Range(0, enemyID.Length);
        EnemyPooler.Instance.SpawnFromPool(enemyID[rand], pos, Quaternion.identity);
        currentEnemyCnt++;
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
