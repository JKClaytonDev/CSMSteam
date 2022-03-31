using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsMonsterCode : MonoBehaviour
{
    closePlayer closePlayerObject;
    public Animator anim;
    public float flashTime;
    bool seen;
    GameObject player;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        GetComponent<EnemyHealth>().enabled = anim.GetCurrentAnimatorStateInfo(0).IsName("HandsSeeNothing");
        anim.SetInteger("Side", 1);
        if (Random.Range(1, 50) > 25)
            anim.SetInteger("Side", 2);
        RaycastHit hit;
        if (!seen)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 50)
            {
                Physics.Raycast(transform.position, Vector3.MoveTowards(transform.position, player.transform.position, 1) - transform.position, out hit);
                seen = (hit.transform.gameObject == player);
            }
        }
        if (seen)
        {
            anim.SetBool("SeePlayer", false);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("HandsScan") || anim.GetCurrentAnimatorStateInfo(0).IsName("HandsScan2"))
            {
                
                Physics.Raycast(transform.position, Vector3.MoveTowards(transform.position, player.transform.position, 1) - transform.position, out hit);
                bool set = (hit.transform.gameObject == player && Time.realtimeSinceStartup > flashTime);
                anim.SetBool("SeePlayer", set);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("HandsRun") || anim.GetCurrentAnimatorStateInfo(0).IsName("HandsRun 2"))
            {
                Vector3 vel = Vector3.MoveTowards(transform.position, player.transform.position, 10) - transform.position;
                vel.y = rb.velocity.y;
                rb.velocity = vel;
                if (Vector3.Distance(transform.position, player.transform.position) < 5)
                {
                    FindObjectOfType<UniversalAudio>().bite();
                    FindObjectOfType<playerHealth>().health -= 200;
                }
            }
        }
        else
            anim.Play("HandsSleep");
    }
}
