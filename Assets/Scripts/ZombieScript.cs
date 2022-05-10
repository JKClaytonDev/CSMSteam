using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject skullFrag;
    public float distanceMultiplier = 1;
    public float speed = 1;
    public Animator anim;
    public AudioClip[] sounds;
    GameObject player;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        if (distanceMultiplier == 0)
            distanceMultiplier = 1;
        GetComponent<enemyHealth>().enabled = false;
        rb = GetComponent<Rigidbody>();
        try
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }
        catch
        {

        }

    }

    float stunTime;
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        Vector3 angles = transform.eulerAngles;
        angles.x = 0;
        angles.z = 0;
        transform.eulerAngles = angles;
        if (Time.realtimeSinceStartup < stunTime)
            return;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ZombieWalk"))
        {
            GetComponent<enemyHealth>().enabled = true;
            Vector3 vel = ((Vector3.MoveTowards(transform.position, player.transform.position, 2 * speed)) - transform.position);
            vel *= 3;
            vel.y = rb.velocity.y;

            rb.velocity = vel;
            if (Random.Range(1, 350*10/sounds.Length) == 1)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(1, 4)]);
            }
        }
        try
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 3 && Time.timeScale > 0.5f)
            {
                FindObjectOfType<UniversalAudio>().scratch();
                FindObjectOfType<PlayerVoices>().Pain();
                stunTime = Time.realtimeSinceStartup + 1;
                if (closePlayerObject.attachedPlayer.GetComponent<attachedHealth>())
                {
                    //Debug.Log("FOUND ATTACHED HEALTH");
                    if (closePlayerObject.attachedPlayer.GetComponent<attachedHealth>().p)
                        closePlayerObject.attachedPlayer.GetComponent<attachedHealth>().p.health -= 30;
                }

            }
        }
        catch
        {
            return;
        }
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position) * distanceMultiplier);
    }
}
