using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeController : MonoBehaviour
{
    private ParticleSystem cakeParticleSystem; // ��ƼŬ �ý��� ������Ʈ�� ������ ����

    private void Start()
    {
        // ��ƼŬ �ý��� ������Ʈ�� ������
        cakeParticleSystem = GetComponent<ParticleSystem>();

        // ��ƼŬ �ý��� ������Ʈ�� ã�� ������ �� ���� �޽��� ���
        if (cakeParticleSystem == null)
        {
            Debug.LogError("ParticleSystem component not found on " + gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ��ƼŬ �ý����� null�� �ƴ��� Ȯ�� �� ��ƼŬ ���
            if (cakeParticleSystem != null)
            {
                // ����ũ ������Ʈ�� ��Ȱ��ȭ�Ͽ� �ð������� ������� ��
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                // ��ƼŬ ���
                cakeParticleSystem.Play();

                // ��ƼŬ ����� ���� �� ������Ʈ ����
                StartCoroutine(DestroyAfterParticles());
                // ����ũ ���� ����
                GameDirector.IncreaseCnt();
            }
        }
    }

    IEnumerator DestroyAfterParticles()
    {
        // ��ƼŬ ��� �ð��� ���� ������ ���
        yield return new WaitForSeconds(cakeParticleSystem.main.duration);

        // ������Ʈ ����
        Destroy(gameObject);
    }
}
