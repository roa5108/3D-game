using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameStart : MonoBehaviour
{
    public GameObject StartPanel; // ���� ���� Ÿ��Ʋ UI�� ����Ű�� ����

    public void ChangeSceneBtn()
    {
        StartPanel.SetActive(false);
        GameDirector.instance.StartGame();
    }
}
