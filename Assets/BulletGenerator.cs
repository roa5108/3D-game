using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public float speed = 0.0001f; // 적절한 속도로 변경
    public GameObject BulletPrefab;
    public Transform FirePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 총알을 FirePos 위치와 회전으로 생성
            GameObject bullet = Instantiate(BulletPrefab, FirePos.position, FirePos.rotation);
            Debug.Log("총알이 생성되었습니다.");

            // 총알의 BulletController 컴포넌트를 가져옴
            BulletController bulletController = bullet.GetComponent<BulletController>();

            if (bulletController != null)
            {
                // Initialize 메서드를 호출하여 FirePos와 속도를 설정
                bulletController.Initialize(FirePos, speed);
                Debug.Log("총알이 초기화되었습니다.");
            }
            else
            {
                Debug.LogError("BulletController를 찾을 수 없습니다.");
            }
        }
    }
}
