using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBatScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    bool run;
    float pauseTime;
    GameObject player;
    Animator magicAnim;
    Animator anim;
    int index;
    Rigidbody rb;
    public AudioClip stunSound;
    public AudioClip hitSound;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        magicAnim = FindObjectOfType<PlayerAnimations>().magicAnim;
        run = false;
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    float stunTime;
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (Time.realtimeSinceStartup < stunTime)
            return;
        anim.SetInteger("BatIndex", index);
        if (Time.realtimeSinceStartup < pauseTime)
            return;
        index = 1;
        rb.velocity = new Vector3();
        if (Vector3.Distance(transform.position, player.transform.position) < 50)
        {
            index = 2;
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.MoveTowards(transform.position, player.transform.position, 1) - transform.position, out hit);
            if (hit.transform.gameObject.GetComponent<PlayerMovement>())
            {
                run = true;
            }
        }
        if (run && Vector3.Distance(transform.position, player.transform.position) > 5)
            rb.velocity = Vector3.MoveTowards(transform.position, player.transform.position, 30)-transform.position;
        else if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            FindObjectOfType<UniversalAudio>().playSound(hitSound);
            FindObjectOfType<UniversalAudio>().bite();
            FindObjectOfType<playerHealth>().health -= 10;
            stunTime = Time.realtimeSinceStartup + 1;
        }
        if (magicAnim.GetCurrentAnimatorStateInfo(0).IsName("FlashHold") && Mathf.Abs((Camera.main.WorldToScreenPoint(transform.position).x - Screen.width / 2)) < 100)
        {
            FindObjectOfType<UniversalAudio>().playSound(stunSound);
            index = 3;
            rb.velocity = Vector3.MoveTowards(transform.position, player.transform.position, -3) - transform.position;
            pauseTime = Time.realtimeSinceStartup + 3;
        }
    }
}
