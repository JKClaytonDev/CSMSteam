using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBallSpawner : MonoBehaviour
{
    public GameObject ball;
    public float ballTime;
    public float Vel = 25;
    public float RandStart = 0;
    public int maxTime = 6;
    public float destTime = 6;
    // Start is called before the first frame update

    private void OnEnable()
    {
        ballTime = Time.realtimeSinceStartup + Random.Range(4, maxTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > ballTime)
        {
            ballTime = Time.realtimeSinceStartup + Random.Range(4, maxTime);
            GameObject b = Instantiate(ball);
            Vector3 dir = -transform.forward;
            b.transform.position = transform.position + dir * Vel*10/25;
            b.transform.position -= transform.forward * Random.Range(0, RandStart);
            b.GetComponent<Rigidbody>().velocity = dir * Vel;
            b.gameObject.SetActive(true);
            Destroy(b, destTime);
        }
    }
}
