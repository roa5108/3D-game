using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int monsterCount = 0;
    public int maxMonsterCount = 10; // �ִ� ���� ��
    public float spawnInterval = 10.0f; // ���� ����
    private bool isSpawning = true;

    void Start()
    {
        // �ʱ� ���� ����
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (monsterCount < maxMonsterCount)
        {
            GenerateMonster();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void GenerateMonster()
    {
        if (isSpawning && monsterCount < maxMonsterCount)
        {
            if (monsterPrefab != null)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                monsterCount++;
                Debug.Log("Monster spawned at position: " + monster.transform.position);
            }
            else
            {
                Debug.LogError("MonsterPrefab is not set.");
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * 30.0f; // �������� 10�� �� ������ ������ ��
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y);
        return randomPosition;
    }

    public void DecreaseMonsterCount()
    {
        monsterCount--;
    }

    public void StopSpawning()
    {
        isSpawning = false; // ���� ���� �ߴ�
    }
}
