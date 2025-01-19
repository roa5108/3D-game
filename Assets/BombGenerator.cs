using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour
{
    public GameObject bombPrefab;
    float span = 5.0f;
    float delta = 0;
    float speed = -0.03f;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetParameter(float span, float speed)
    {
        this.span = span;
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject item;
            
            item = Instantiate(bombPrefab) as GameObject;
            
            float x = Random.Range(-30, 30);
            float z = Random.Range(-30, 30);
            item.transform.position = new Vector3(x, 20, z);
            item.GetComponent<BombController>().dropSpeed = this.speed;
        }
    }
}
