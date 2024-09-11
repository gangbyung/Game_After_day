using System.Collections;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // 생성할 프리팹
    public float spawnInterval = 10f; // 생성 간격 (초 단위)
    private float nextSpawnY = -10f; // 다음 프리팹의 Y 위치


    void Start()
    {
        StartCoroutine(SpawnPrefabRoutine());
    }
    IEnumerator SpawnPrefabRoutine()
    {
        while (true)
        {
            // 프리팹 생성 위치 설정
            Vector3 spawnPosition = new Vector3(-85, nextSpawnY, 0); // x와 z는 0, y는 현재의 nextSpawnY 값

            // 프리팹 생성
            Instantiate(prefab, spawnPosition, Quaternion.identity);

            // 다음 생성할 프리팹의 Y 위치 증가
            nextSpawnY += 5f;

            // 다음 생성까지 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
