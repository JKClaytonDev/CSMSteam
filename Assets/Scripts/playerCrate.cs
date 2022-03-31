using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCrate : MonoBehaviour
{
    public float healthCount;
    public float ammoCount;
    bool set = false;
    public AudioClip sound;
    public AudioClip denySound;
    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<WolfMovement>())
        {
            WolfMovement mvmt = collision.gameObject.GetComponent<WolfMovement>();
            if (mvmt.health < healthCount)
            {
                mvmt.health = healthCount;
                set = true;
            }
            if (mvmt.ammo < ammoCount)
            {
                mvmt.ammo = ammoCount;
                set = true;
            }
            if (set)
            {
                if (collision.gameObject.GetComponent<AudioSource>())
                    collision.gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
                Destroy(gameObject);
            }
            else
            {
                if (GetComponent<AudioSource>())
                    GetComponent<AudioSource>().PlayOneShot(denySound);
            }
        }
    }
}
