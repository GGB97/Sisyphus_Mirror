using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance; // 필요하지 않게될 수 있음.
    public Transform target; // 나중에 GameManager 같은데서 들고있으면 거기서 가져오게 하면 될듯

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
    }

    private void Start()
    {
        StartCoroutine(SpawnStart());
    }

    void SetSpawnPos()
    {
        // Plane의 스케일을 기준으로 실제 크기 계산 Plane은 기본 10x10 크기
        float width = 10f * plane.transform.localScale.x;
        float length = 10f * plane.transform.localScale.z;

        // 좌측 하단과 우측 상단 좌표 계산
        bottomLeft = plane.transform.position + new Vector3(-width / 2, 0, -length / 2);
        topRight = plane.transform.position + new Vector3(width / 2, 0, length / 2);
    }

    IEnumerator SpawnStart()
    {
        int spawnCnt = waveData.numPerSpawn;
        WaitForSeconds delay = new WaitForSeconds(waveData.spawnDelay);

        SetSpawnPos();

        yield return delay;

        // 보스 스테이지 일때는 시작시 보스 스폰하고 시작하면 될듯
        // --

        while (true) // 게임 종료 검사로 변경 필요함
        {
            if (currentEnemyCnt < maxEnemyCnt)
            {
                for (int i = 0; i < spawnCnt; i++)
                {
                    // 이곳에서 조건검사로 Normal/Elite 나누면 될듯 확률로 해가지고
                    // --
                    int rand = Random.Range(0, waveData.normal.Length);
                    Vector3 pos = new Vector3(
                        Random.Range(bottomLeft.x, topRight.x),
                        0,
                        Random.Range(bottomLeft.z, topRight.z)
                        );
                    // 여기서 pos의 위치에 Spawn이 가능한지 검사 필요성이 생긴다면 추가 예정
                    // --

                    EnemyPooler.Instance.SpawnFromPool(waveData.normal[rand], pos, Quaternion.identity);
                    currentEnemyCnt++;

                    if (currentEnemyCnt >= maxEnemyCnt)
                        break;
                }
            }
            yield return delay;
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

    public void FindAllEnemiesAndDie() // Test용
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.isDie = true;
        }
    }
}
