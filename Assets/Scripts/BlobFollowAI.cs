using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobFollowAI : MonoBehaviour
{
    closePlayer closePlayerObject;
    int stage;
    public Animator anim;
    GameObject player;
    Rigidbody rb;
    public bool active;
    Vector3 startRot;
    float startTime;
    Transform myTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        startTime = Time.realtimeSinceStartup;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        myTransform.LookAt(player.transform);
        startRot = myTransform.eulerAngles;
        AnimationEvent evt;
        evt = new AnimationEvent();
        evt.functionName = "blobBite";
        rb = GetComponent<Rigidbody>();
        
        stage = 0;
        anim.SetInteger("Stage", 0);
        if (active)
        {
            anim.Play("BlobWalk");
            anim.speed = 4;
        }
    }
    public void blobBite()
    {
        if (Vector3.Distance(myTransform.position, player.transform.position) < 6)
        {
            FindObjectOfType<UniversalAudio>().bite();
            FindObjectOfType<PlayerVoices>().Pain();
            stunTime = Time.realtimeSinceStartup + 1;
            closePlayerObject.attachedPlayer.GetComponent<attachedHealth>().p.health -= 20;
            
            
            if (active)
                Destroy(gameObject);
        }
    }
    float stunTime;

    void Update()
    {
        if (!player)
        {
            player = closePlayerObject.attachedPlayer;
            return;
        }
        Vector3 angles = myTransform.eulerAngles;
        myTransform.eulerAngles = startRot;
        if (active)
        {
            if (Time.realtimeSinceStartup < startTime + 1)
            {
                Vector3 vel = myTransform.forward * 12;
                vel.y = rb.velocity.y;
                rb.velocity = vel;
            }
        }
        myTransform.eulerAngles = angles;
        if (Time.realtimeSinceStartup < stunTime)
            return;
        if (stage != 0)
        {
            
            if (stage == 1)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlobWalk"))
                {
                    float y = rb.velocity.y;
                    Vector3 v = ((((Vector3.MoveTowards(myTransform.position, player.transform.position, 6)) - myTransform.position)));
                    v.y = y;
                    rb.velocity = v;
                    if (Vector3.Distance(myTransform.position, player.transform.position) < 6)
                    {
                        anim.Play("BlobAttack");
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(myTransform.position, player.transform.position) < 25)
            {
                stage = 1;
                anim.SetInteger("Stage", 1);
            }
        }
        Vector3 angles2 = myTransform.localEulerAngles;
        angles2.x = 0;
        angles2.z = 0;
        myTransform.localEulerAngles = angles2;
        
    }
}
