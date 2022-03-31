using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzerScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject player;
    public bool foundPlayer;
    Rigidbody rb;
    public float checkTime;
    public Vector3 setVel;
    public Animator anim;
    public GameObject[] warts;
    public AudioClip fleshSound;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        rb = GetComponent<Rigidbody>();
        Destroy(warts[Random.Range(0, warts.Length)]);
        Destroy(warts[Random.Range(0, warts.Length)]);

        
    }

    // Update is called once per frame
    void Update()
    {
        bool destroy = true;
        foreach (GameObject g in warts)
        {
            if (g != null)
            {
                destroy = false;
            }
        }
        if (destroy)
        {
            Destroy(gameObject);
        }
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
                        GetComponent<AudioSource>().PlayOneShot(fleshSound);
                        checkTime = 1;
                    }
                }
                return;
            }
            else
            {
                anim.speed = 1;
                setVel = Vector3.MoveTowards(transform.position, player.transform.position, 10)-transform.position;
            }
            
        }
        rb.velocity = setVel;
    }
}
