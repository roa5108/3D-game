using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public float speed = 0.0001f; // ������ �ӵ��� ����
    public GameObject BulletPrefab;
    public Transform FirePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �Ѿ��� FirePos ��ġ�� ȸ������ ����
            GameObject bullet = Instantiate(BulletPrefab, FirePos.position, FirePos.rotation);
            Debug.Log("�Ѿ��� �����Ǿ����ϴ�.");

            // �Ѿ��� BulletController ������Ʈ�� ������
            BulletController bulletController = bullet.GetComponent<BulletController>();

            if (bulletController != null)
            {
                // Initialize �޼��带 ȣ���Ͽ� FirePos�� �ӵ��� ����
                bulletController.Initialize(FirePos, speed);
                Debug.Log("�Ѿ��� �ʱ�ȭ�Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogError("BulletController�� ã�� �� �����ϴ�.");
            }
        }
    }
}
