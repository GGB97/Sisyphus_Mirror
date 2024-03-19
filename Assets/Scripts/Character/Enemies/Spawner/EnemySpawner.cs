using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance; // 필요하지 않게될 수 있음.
    public Transform target; // 나중에 GameManager 같은데서 들고있으면 거기서 가져오게 하면 될듯

    // 등장 가능 몬스터 설정해야함.
    [SerializeField] WaveSO waveData;

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
        // Plane 오브젝트의 Transform 컴포넌트
        Transform planeTransform = plane.transform;

        // Plane의 스케일을 기준으로 실제 크기 계산
        float width = 10f * planeTransform.localScale.x;
        float length = 10f * planeTransform.localScale.z;

        // 좌측 하단과 우측 상단 좌표 계산
        bottomLeft = planeTransform.position + new Vector3(-width / 2, 0, -length / 2);
        topRight = planeTransform.position + new Vector3(width / 2, 0, length / 2);

        Debug.Log("Bottom Left: " + bottomLeft);
        Debug.Log("Top Right: " + topRight);

        StartCoroutine(SpawnStart());
    }

    IEnumerator SpawnStart()
    {
        int spawnCnt = waveData.numPerSpawn;
        WaitForSeconds delay = new WaitForSeconds(waveData.spawnDelay);

        // 보스 스테이지 일때는 시작시 보스 한마리 스폰하고 시작하면 될듯
        //

        while (true) // 게임 종료 검사로 변경 필요함
        {
            if (currentEnemyCnt < maxEnemyCnt)
            {
                // 한번에 스폰할 양 추후 WaveData에 넣을지 고민좀 해야함
                for (int i = 0; i < spawnCnt; i++)
                {
                    int rand = Random.Range(0, waveData.normal.Length);
                    Vector3 pos = new Vector3(
                        Random.Range(bottomLeft.x, topRight.x),
                        0,
                        Random.Range(bottomLeft.z, topRight.z)
                        );
                    // 여기서 pos의 위치에 Spawn이 가능한지 검사 필요성이 생긴다면 추가 예정

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
        StopCoroutine(SpawnStart());
    }

    public void DecrementEnemyCnt()
    {
        currentEnemyCnt--;
    }
}
