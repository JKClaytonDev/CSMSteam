using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject player;
    public Animator anim;
    Rigidbody rb;
    float biteTime;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        GetComponent<enemyHealth>().enabled = false;
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MantisAnim"))
        {
            GetComponent<enemyHealth>().enabled = true;
            Vector3 move = (Vector3.MoveTowards(transform.position, player.transform.position, 15) - transform.position);
            move.y = 0;
            rb.velocity = move+ Vector3.up*rb.velocity.y;
        }
        else
        {
            rb.velocity = Physics.gravity;
            biteTime = Time.realtimeSinceStartup + 1;
        }
        if (Time.realtimeSinceStartup > biteTime)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 10)
            FindObjectOfType<playerHealth>().health -= 30;
            biteTime = Time.realtimeSinceStartup + 0.6f;
        }
    }
}
