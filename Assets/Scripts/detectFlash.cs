using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectFlash : MonoBehaviour
{
    public HandsMonsterCode parent;
    Animator magicAnim;
    AudioSource asc;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        asc = GetComponent<AudioSource>();
        magicAnim = FindObjectOfType<PlayerAnimations>().magicAnim;
    }

    // Update is called once per frame
    void Update()
    {
        if (magicAnim.GetCurrentAnimatorStateInfo(0).IsName("FlashHold") && Mathf.Abs((Camera.main.WorldToScreenPoint(transform.position).x - Screen.width / 2)) < 10)
        {
            if (Time.realtimeSinceStartup > parent.flashTime)
                asc.PlayOneShot(sound);
            parent.flashTime = Time.realtimeSinceStartup + 3;
        }
    }
}
