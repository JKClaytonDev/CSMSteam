using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportBack : MonoBehaviour
{
    float moveTime;
    public GameObject BackTo;
    public GameObject enable;
    float d = 9.982018f;
    private void Start()
    {
        //Debug.Log("DIFFERENCE IS " + (BackTo.transform.position.y - transform.position.y));
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (Time.realtimeSinceStartup > moveTime)
            moveTime = Time.realtimeSinceStartup + 0.5f;
        else
            return;
        Debug.Log("PLAYER HIT");
        if (BackTo)
        {
            if (enable && Random.Range(1, 10) == 1)
                enable.SetActive(true);
            FindObjectOfType<PlayerMovement>().transform.position += Vector3.up * d;
            Debug.Log("TELEPORTED");
        }
    }

}
