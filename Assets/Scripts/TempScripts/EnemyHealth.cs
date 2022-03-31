using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public AudioClip[] hitSounds;
    public AudioClip[] damageSounds;

    public float health;
    GameObject player;
    // Start is called before the first frame update

    void Start()
    {
        
         
        player = FindObjectOfType<WolfMovement>().gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            if (GetComponent<runAtPlayer>())
            {
                GetComponent<runAtPlayer>().PunchTime = Time.realtimeSinceStartup+0.3f;
            }
            if (!GetComponent<AudioSource>().isPlaying)
            {
                if (GetComponent<runAtPlayer>())
                {
                    Vector3 pos = (transform.forward * -3)/Time.deltaTime;
                    pos.y = transform.position.y;
                    GetComponent<Rigidbody>().velocity = pos;
                    GetComponent<runAtPlayer>().seenTime = Time.realtimeSinceStartup + 1;
                }
                Debug.Log(this.name + " " + health);
                if (health == 1)
                {
                    player.GetComponent<AudioSource>().PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
                    player.GetComponent<WolfMovement>().health -= 10;
                    Destroy(gameObject);
                    health--;
                }
                GetComponent<AudioSource>().PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
                player.GetComponent<WolfMovement>().health--;
                
            }
            Vector3 force = Vector3.MoveTowards(transform.position, player.transform.position, 15) - transform.position;
            force.y = 0;
            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().AddForce(-force, ForceMode.VelocityChange);
            player.GetComponent<Rigidbody>().AddForce(2*force, ForceMode.VelocityChange);
        }
        if (health < 0)
        {
            Debug.Log("ENEMY KILLS " + PlayerPrefs.GetFloat("EnemyKills"));
            float kills = PlayerPrefs.GetFloat("EnemyKills");
            float code = 0;
            Destroy(gameObject);
        }
    }
}
