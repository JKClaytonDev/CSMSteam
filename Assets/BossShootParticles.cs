using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootParticles : MonoBehaviour
{
    public GameObject shoot;
    float shootTime;
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > shootTime)
        {
            GameObject bullet = Instantiate(shoot);
            bullet.transform.position = transform.position;
            bullet.transform.LookAt(Camera.main.transform.position);
            shootTime = Time.realtimeSinceStartup + 5;
        }
    }
}
