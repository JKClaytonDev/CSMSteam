using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioSource : MonoBehaviour
{

    public int randomChance = 1;
    public AudioClip clip;
    public BlobFollowAI blob;
    // Start is called before the first frame update
    void Start()
    {
        AnimationEvent evt;
        evt = new AnimationEvent();
        evt.functionName = "sound";

        AnimationEvent evt2;
        evt = new AnimationEvent();
        evt.functionName = "attack";
    }

    // Update is called once per frame
    public void sound()
    {
        if (Random.Range(1, randomChance) != 1)
            return;
        //Debug.Log("PLAYED");
        FindObjectOfType<UniversalAudio>().playSound(clip);
    }
    public void attack()
    {
        blob.blobBite();
    }
    }
