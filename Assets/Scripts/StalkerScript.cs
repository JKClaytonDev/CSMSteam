using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StalkerScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    public AudioClip stinger;
    GameObject player;
    public Renderer r;
    float changeSpeedTime;
    bool visible = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (Time.realtimeSinceStartup > changeSpeedTime)
        {
            GetComponent<NavMeshAgent>().speed = Random.Range(3, 7);
            changeSpeedTime = Time.realtimeSinceStartup + Random.Range(5, 7);
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 7)
        {
            player.transform.LookAt(transform);
            FindObjectOfType<playerHealth>().health -= 150;
        }
        transform.LookAt(player.transform);
        if (r.isVisible && !visible)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit);
            if (hit.transform.gameObject.layer == 11 || hit.transform.gameObject.name.Contains("Player"))
            {
                GetComponent<NavMeshAgent>().speed = 9;
                GetComponent<AudioSource>().PlayOneShot(stinger);
                visible = true;
            }
        }
        
        transform.Rotate(90, 0, 0);
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
    }
}
