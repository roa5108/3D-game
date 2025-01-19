using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public Animator anim;
    public int health = 3;
    public float dyingAnimationDuration = 2f;

    private bool isDying = false;
    private Transform playerTransform;

    private UnityEngine.AI.NavMeshAgent nmAgent;
    private MonsterGenerator generator;

    public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        nmAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        generator = FindObjectOfType<MonsterGenerator>();
        hpSlider.value = hpSlider.maxValue; ;

        // 플레이어 객체를 태그로 찾습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying && playerTransform != null)
        {
            if (nmAgent.isOnNavMesh)
            {
                nmAgent.SetDestination(playerTransform.position);
            }
            else
            {
                Debug.LogError("NavMeshAgent is not on a NavMesh!");
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cake"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDying)
        {
            health -= damage;
            hpSlider.value = health;
            if (health <= 0)
            {
                isDying = true;
                anim.SetTrigger("Dying");
                //generator.DecreaseMonsterCount();
                StartCoroutine(DestroyAfterAnimation(dyingAnimationDuration));
            }
        }
    }

    IEnumerator DestroyAfterAnimation(float duration)
    {
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
