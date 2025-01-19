using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    private Transform firePos;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void Initialize(Transform firePos, float speed)
    {
        this.firePos = firePos;
        this.speed = speed;

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("FirePos ����: " + firePos.forward);
            rb.AddForce(firePos.forward * speed, ForceMode.Impulse);
            Debug.Log("Force�� ���������� �߰��Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError("Rigidbody�� ã�� �� �����ϴ�.");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.5f);
        }

        if (other.gameObject.CompareTag("monster"))
        {
            GetComponent<Rigidbody>().isKinematic = false;

            MonsterController monster = other.gameObject.GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.TakeDamage(1);  // ü���� 1��ŭ ���ҽ�Ŵ
                Destroy(gameObject); // ���Ϳ� ������ �Ѿ��� �ı�
            }
        }
    }
}
