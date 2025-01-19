using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeGenerator : MonoBehaviour
{
    public GameObject cakePrefab; // ����ũ ������
    public float spawnInterval = 5f; // ����ũ ���� ����
    public float spawnRadius = 50f; // ���� ��ġ �ݰ�
    public float cakeDuration = 10f; // ����ũ ���� �ð�
    private bool isSpawning = true;

    void Start()
    {
        // �ڷ�ƾ�� �����Ͽ� ����ũ ������ �ֱ������� ȣ���մϴ�.
        StartCoroutine(SpawnCakeRoutine());
    }

    IEnumerator SpawnCakeRoutine()
    {
        while (isSpawning)
        {
            // ������ ��ġ ���
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y);

            // ����ũ ����
            GameObject cake = Instantiate(cakePrefab, spawnPosition, Quaternion.identity);

            // ����ũ ������ ���� �ڷ�ƾ ����
            StartCoroutine(DestroyCakeAfterDuration(cake));

            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator DestroyCakeAfterDuration(GameObject cake)
    {
        // ����ũ�� ������ �� 5�ʰ� ���� ������ ���
        yield return new WaitForSeconds(5f);

        // ����ũ�� ������ �����ϴ��� Ȯ�� �� ����
        if (cake != null)
        {
            Destroy(cake);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false; // ����ũ ���� �ߴ�
    }
}
