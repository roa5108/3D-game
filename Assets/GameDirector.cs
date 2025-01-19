using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;
    public static double cnt = 0;

    public RectTransform HpbarGroup;
    public RectTransform Hpbar;
    public GameObject Hp;
    public GameObject GameOver;
    GameObject timerText;
    GameObject cakeNumText;
    GameObject GameOverText;
    GameObject EatenCakeText;

    GameObject bombGen;
    GameObject cakeGen;
    GameObject monGen;

    public AudioClip backgroundSE;
    AudioSource aud;

    float time = 90.0f; // 초기 시간 설정 (초 단위)
    bool isGameStarted = false;

    void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.aud.PlayOneShot(this.backgroundSE);

        this.timerText = GameObject.Find("Time");
        this.cakeNumText = GameObject.Find("CakeNum");
        this.Hpbar = GameObject.Find("Hpbar").GetComponent<RectTransform>();
        this.GameOverText = GameObject.Find("GameOver");
        this.EatenCakeText = GameObject.Find("EatenCake");

        this.bombGen = GameObject.Find("BombGenerator");
        this.cakeGen = GameObject.Find("CakeGenerator");
        this.monGen = GameObject.Find("MonsterGenerator");
        GameOver.SetActive(false);
        // 게임 시작 전까지 UI 비활성화
        this.timerText.SetActive(false);
        this.cakeNumText.SetActive(false);
        this.Hp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            this.timerText.GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
            this.cakeNumText.GetComponent<TMPro.TextMeshProUGUI>().text = "Cake : " + cnt;

            this.time -= Time.deltaTime;
            DecreaseHp();
            if (time <= 0 || Hpbar.localScale.x <= 0)
            {
                this.time = 0;
                Vector3 currentScale = Hpbar.localScale;
                currentScale.x = 0;
                Hpbar.localScale = currentScale;

                this.bombGen.GetComponent<BombGenerator>().SetParameter(10000.0f, 0);
                this.cakeGen.GetComponent<CakeGenerator>().StopSpawning();
                this.monGen.GetComponent<MonsterGenerator>().StopSpawning();

                GameOver.SetActive(true);
                this.GameOverText.GetComponent<TMPro.TextMeshProUGUI>().text = "Game Over ";
                this.EatenCakeText.GetComponent<TMPro.TextMeshProUGUI>().text = "You ate " + cnt + " cake";

                if (this.backgroundSE != null && this.aud.isPlaying)
                {
                    this.aud.Stop();
                }
            }
            else if (0 <= this.time && this.time < 30)
            {
                this.bombGen.GetComponent<BombGenerator>().SetParameter(4f, -0.06f);
            }
            else if (30 <= this.time && this.time < 60)
            {
                this.bombGen.GetComponent<BombGenerator>().SetParameter(3f, -0.07f);
            }
            else if (60 <= this.time && this.time < 90)
            {
                this.bombGen.GetComponent<BombGenerator>().SetParameter(5.0f, -0.05f);
            }
        }
    }

    public void StartGame()
    {
        isGameStarted = true; // 게임 시작

        // 게임 시작 시 UI 활성화
        this.timerText.SetActive(true);
        this.cakeNumText.SetActive(true);
        this.Hp.SetActive(true);
    }

    public static void IncreaseCnt()
    {
        cnt += 0.5;
    }

    public void DecreaseHp()
    {
        if (instance != null && instance.Hpbar != null)
        {
            Hpbar.localScale = new Vector3((float)PlayerController.curHealth / PlayerController.maxHealth, 1, 1);
        }
    }
}

