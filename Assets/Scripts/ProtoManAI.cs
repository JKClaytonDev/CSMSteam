using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ProtoManAI : MonoBehaviour
{
    closePlayer closePlayerObject;
    public AudioClip fire;
    NavMeshAgent a;
    public GameObject[] destination;
    float swapTime;
    Animator anim;
    public GameObject leavesExplode;
    GameObject player;
    float attackTime;
    bool start;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        anim = GetComponent<Animator>();
        start = false;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        a = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (Time.realtimeSinceStartup > swapTime)
        {
            a.SetDestination(destination[Random.Range(0, destination.Length - 1)].transform.position);
            swapTime = Time.realtimeSinceStartup + Random.Range(0.1f, 2f);
            if (!start)
            {
                if (Vector3.Distance(transform.position, player.transform.position) > 15)
                {
                    start = true;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 13)
                {
                    anim.Play("AxeThrow");
                    GameObject l = Instantiate(leavesExplode);
                    l.transform.position = transform.position-Vector3.up;
                    Destroy(l, 0.3f);
                }
            }
            if (Vector3.Distance(transform.position, player.transform.position) > 17)
            {
                GetComponent<AudioSource>().PlayOneShot(fire);
                anim.Play("FireShotgun");
                FindObjectOfType<playerHealth>().health -= 5;
            }
        }
    }
}
