using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardScript : MonoBehaviour
{
    public GameObject linkedChild;
    public AudioClip sound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(linkedChild);
            Destroy(gameObject);
        }
    }
}
