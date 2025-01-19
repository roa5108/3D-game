using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameStart : MonoBehaviour
{
    public GameObject StartPanel; // 게임 시작 타이틀 UI를 가리키는 변수

    public void ChangeSceneBtn()
    {
        StartPanel.SetActive(false);
        GameDirector.instance.StartGame();
    }
}
