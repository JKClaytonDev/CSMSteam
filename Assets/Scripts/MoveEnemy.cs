using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    closePlayer closePlayerObject;
    PlayerMovement player;
    public MeshRenderer moveMesh;
    public AudioClip dogActivateSound;
    public Material[] mat;
    public bool contains;
    public float EnemySpeed = 2;
    public float distance;
    Rigidbody rb;
    public Animator anim;
    public int customDamage = 1;
    bool activated;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        anim.speed = 0;
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer.GetComponent<PlayerMovement>();
        if (!activated)
        {
            rb.velocity = new Vector3();
            if (Vector3.Distance(transform.position, player.transform.position) < distance)
            {
                if (dogActivateSound)
                GetComponent<AudioSource>().PlayOneShot(dogActivateSound);
                activated = true;
            }
            return;
        }
        anim.speed = 1;
        rb.velocity = Vector3.down * rb.velocity.y;
        contains = false;
        if (moveMesh)
        {
            
            foreach (Material m in mat)
            {
                Debug.Log("TESTING CONTAINS " + (m.name + " (Instance)") + " " + moveMesh.material.name);
                if ((m.name + "  (Instance)") == moveMesh.material.name)
                    contains = true;
            }
        }
        if (contains)
        {
            Debug.Log("CONTAINS");
            return;
        }
        Vector3 vel = (Vector3.MoveTowards(player.transform.position, transform.position, 5) - transform.position) * EnemySpeed;
        
        vel.y = Physics.gravity.y;
        rb.velocity += vel;
        if (Vector3.Distance(transform.position, player.transform.position) < 7)
        {
            FindObjectOfType<UniversalAudio>().bite();
            if (Random.Range(1, 4) == 1)
            FindObjectOfType<playerHealth>().health -= customDamage;
        }
    }
}
