using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBallSpawner : MonoBehaviour
{
    public GameObject ball;
    public float ballTime;
    // Start is called before the first frame update
    void Start()
    {
        ballTime = Time.realtimeSinceStartup + Random.Range(7, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > ballTime)
        {
            ballTime = Time.realtimeSinceStartup + Random.Range(6, 4);
            GameObject b = Instantiate(ball);
            Vector3 dir = -transform.forward;
            b.transform.position = transform.position + dir * 10;
            b.GetComponent<Rigidbody>().velocity = dir * 25;
            b.gameObject.SetActive(true);
            Destroy(b, 6);
        }
    }
}
