using System.Collections;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // ������ ������
    public float spawnInterval = 10f; // ���� ���� (�� ����)
    private float nextSpawnY = 0f; // ���� �������� Y ��ġ

    
    public void SpawnStart()
    {
        StartCoroutine(SpawnPrefabRoutine());
    }
    IEnumerator SpawnPrefabRoutine()
    {
        while (true)
        {
            // ������ ���� ��ġ ����
            Vector3 spawnPosition = new Vector3(0, nextSpawnY, 0); // x�� z�� 0, y�� ������ nextSpawnY ��

            // ������ ����
            Instantiate(prefab, spawnPosition, Quaternion.identity);

            // ���� ������ �������� Y ��ġ ����
            nextSpawnY += 5f;

            // ���� �������� ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
