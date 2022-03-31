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
    
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        startTime = Time.realtimeSinceStartup;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        transform.LookAt(player.transform);
        startRot = transform.eulerAngles;
        if (!anim)
            anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        AnimationEvent evt;
        evt = new AnimationEvent();
        evt.functionName = "blobBite";
        rb = GetComponent<Rigidbody>();
        
        stage = 0;
        if (active)
        {
            anim.Play("BlobWalk");
            anim.speed = 4;
        }
    }
    public void blobBite()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 6)
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
        if (Time.frameCount % 3 != 0)
            return;
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        Vector3 angles = transform.eulerAngles;
        transform.eulerAngles = startRot;
        if (active)
        {
            if (Time.realtimeSinceStartup < startTime + 1)
                transform.position += transform.forward * Time.deltaTime * 12;
        }
        transform.eulerAngles = angles;
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
        if (Time.realtimeSinceStartup < stunTime)
            return;
        transform.LookAt(player.transform);
        transform.Rotate(0, -90, 0);
        if (stage != 0)
        {
            
            if (stage == 1)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlobWalk"))
                {
                    float y = rb.velocity.y;
                    Vector3 v = ((((Vector3.MoveTowards(transform.position, player.transform.position, 6)) - transform.position)));
                    v.y = y;
                    rb.velocity = v;
                }
                if (Vector3.Distance(transform.position, player.transform.position) < 6)
                    stage = 0;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 25)
                return;
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.MoveTowards(transform.position, player.transform.position, 1)-transform.position, out hit);
            if (hit.transform.gameObject.GetComponent<PlayerMovement>())
                stage = 1;
        }
        Vector3 angles2 = transform.localEulerAngles;
        angles2.x = 0;
        angles2.z = 0;
        transform.localEulerAngles = angles2;
            anim.SetInteger("Stage", stage);
    }
}
