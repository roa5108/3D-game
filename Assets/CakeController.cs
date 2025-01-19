using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeController : MonoBehaviour
{
    private ParticleSystem cakeParticleSystem; // 파티클 시스템 컴포넌트를 저장할 변수

    private void Start()
    {
        // 파티클 시스템 컴포넌트를 가져옴
        cakeParticleSystem = GetComponent<ParticleSystem>();

        // 파티클 시스템 컴포넌트를 찾지 못했을 때 오류 메시지 출력
        if (cakeParticleSystem == null)
        {
            Debug.LogError("ParticleSystem component not found on " + gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 파티클 시스템이 null이 아닌지 확인 후 파티클 재생
            if (cakeParticleSystem != null)
            {
                // 케이크 오브젝트를 비활성화하여 시각적으로 사라지게 함
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                // 파티클 재생
                cakeParticleSystem.Play();

                // 파티클 재생이 끝난 후 오브젝트 삭제
                StartCoroutine(DestroyAfterParticles());
                // 케이크 개수 증가
                GameDirector.IncreaseCnt();
            }
        }
    }

    IEnumerator DestroyAfterParticles()
    {
        // 파티클 재생 시간이 끝날 때까지 대기
        yield return new WaitForSeconds(cakeParticleSystem.main.duration);

        // 오브젝트 삭제
        Destroy(gameObject);
    }
}
