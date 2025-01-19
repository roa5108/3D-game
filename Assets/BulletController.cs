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
            Debug.Log("FirePos 방향: " + firePos.forward);
            rb.AddForce(firePos.forward * speed, ForceMode.Impulse);
            Debug.Log("Force가 성공적으로 추가되었습니다.");
        }
        else
        {
            Debug.LogError("Rigidbody를 찾을 수 없습니다.");
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
                monster.TakeDamage(1);  // 체력을 1만큼 감소시킴
                Destroy(gameObject); // 몬스터에 맞으면 총알을 파괴
            }
        }
    }
}
