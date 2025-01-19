using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeGenerator : MonoBehaviour
{
    public GameObject cakePrefab; // 케이크 프리팹
    public float spawnInterval = 5f; // 케이크 생성 간격
    public float spawnRadius = 50f; // 생성 위치 반경
    public float cakeDuration = 10f; // 케이크 지속 시간
    private bool isSpawning = true;

    void Start()
    {
        // 코루틴을 시작하여 케이크 생성을 주기적으로 호출합니다.
        StartCoroutine(SpawnCakeRoutine());
    }

    IEnumerator SpawnCakeRoutine()
    {
        while (isSpawning)
        {
            // 랜덤한 위치 계산
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y);

            // 케이크 생성
            GameObject cake = Instantiate(cakePrefab, spawnPosition, Quaternion.identity);

            // 케이크 삭제를 위한 코루틴 시작
            StartCoroutine(DestroyCakeAfterDuration(cake));

            // 지정된 간격만큼 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator DestroyCakeAfterDuration(GameObject cake)
    {
        // 케이크가 생성된 후 5초가 지날 때까지 대기
        yield return new WaitForSeconds(5f);

        // 케이크가 여전히 존재하는지 확인 후 삭제
        if (cake != null)
        {
            Destroy(cake);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false; // 케이크 생성 중단
    }
}
