using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;
    public static int curHealth = 100;
    public static int maxHealth = 100;

    public AudioClip cakeSE;
    public AudioClip bombSE;
    public AudioClip monSE;
    AudioSource aud;

    Vector3 moveVec;
    public Animator anim;

    void Start()
    {
        this.aud = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // 이동
        transform.position += moveVec * speed * Time.deltaTime;

        // 애니메이션 설정
        anim.SetBool("isRun", moveVec != Vector3.zero);

        // 움직일 때만 LookAt 호출
        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cake"))
        {
            this.aud.PlayOneShot(this.cakeSE);
            if (curHealth < maxHealth)
            {
                curHealth += 5;
            }
        }

        if (other.gameObject.CompareTag("monster"))
        {
            this.aud.PlayOneShot(this.monSE);
            curHealth -= 5;
        }

        if (other.gameObject.CompareTag("Bomb"))
        {
            this.aud.PlayOneShot(this.bombSE);
            curHealth -= 10;
        }
    }
}