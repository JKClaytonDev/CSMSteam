using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    GameObject player;
    public float customSpeed = 1;
    public bool dontDestroy;
    public bool ThroughWalls;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (dontDestroy)
        {
            transform.position += transform.forward * Time.deltaTime * 5 * customSpeed;
            return;
        }
            transform.position += transform.forward*Time.deltaTime* 14 * customSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (dontDestroy && !ThroughWalls)
            return;
        if (Vector3.Distance(transform.position, player.transform.position) < 4)
        {
            FindObjectOfType<UniversalAudio>().getHit();
            FindObjectOfType<playerHealth>().health -= 20;
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
