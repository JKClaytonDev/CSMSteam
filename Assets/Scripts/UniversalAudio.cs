using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalAudio : MonoBehaviour
{
    public Animator anim;
    public AudioClip hitSound;
    public void bite()
    {
        Debug.Log("BIT THE PLAYER");
        anim.Play("PlayerBite");
    }
    public void scratch()
    {
        Debug.Log("SCRATCHEd THE PLAYER");
        anim.Play("PlayerHit");
    }
    public void getHit()
    {
        GetComponent<AudioSource>().PlayOneShot(hitSound);
    }
    public void playSound(AudioClip cl)
    {
        GetComponent<AudioSource>().clip = (cl);
        GetComponent<AudioSource>().PlayOneShot(cl);
    }
}
