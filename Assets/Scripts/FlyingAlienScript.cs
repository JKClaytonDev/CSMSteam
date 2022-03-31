using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAlienScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject player;
    public bool foundPlayer;
    Rigidbody rb;
    public float checkTime;
    public Vector3 setVel;
    public Animator anim;
    public float upTime;

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        rb = GetComponent<Rigidbody>();



    }

    // Update is called once per frame
    void Update()
    {

        if (Time.realtimeSinceStartup > checkTime)
        {
            checkTime = Time.realtimeSinceStartup + 2;
            if (!player)
                player = FindObjectOfType<PlayerMovement>().gameObject;
            if (closePlayerObject)
                player = closePlayerObject.attachedPlayer;
            if (!foundPlayer)
            {
                anim.speed = 0;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.MoveTowards(transform.position, player.transform.position, 1) - transform.position, out hit))
                {
                    if (hit.transform.gameObject.name.Contains("Player"))
                    {
                        foundPlayer = true;
                        anim.enabled = true;
                        checkTime = 1;
                    }
                }
            }
            else
            {
                anim.speed = 1;
                setVel = Vector3.MoveTowards(transform.position, player.transform.position, 5) - transform.position;
            }

        }
        rb.velocity = setVel + (Vector3.up * upTime * Mathf.Sin(Time.realtimeSinceStartup));
    }
}
