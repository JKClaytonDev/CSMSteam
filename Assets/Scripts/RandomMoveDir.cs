using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveDir : MonoBehaviour
{
    public float speed;
    public float changeIncrement;
    float changeVelTime;
    public Vector3 vel;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > changeVelTime)
        {
            int x = 1;
            int y = 1;
            
            vel = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));

            if (vel.x < 0)
                vel.x = -1;
            else
                vel.x = 1;

            if (vel.z < 0)
                vel.z = -1;
            else
                vel.z = 1;

            vel *= speed;

            changeVelTime = Time.realtimeSinceStartup + changeIncrement;
        }
        Vector3 fixedVel = vel;
        fixedVel.y = rb.velocity.y;
        rb.velocity = fixedVel;
    }
}
