using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSound : MonoBehaviour
{
    public AudioClip[] sounds;
    private void Start()
    {
        gameObject.layer = 16;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0, sounds.Length - 1)], 3);
            Destroy(gameObject, 2);
            Destroy(this);
        }
    }
}
