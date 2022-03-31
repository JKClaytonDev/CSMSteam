using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AnimationEvent evt;
        evt = new AnimationEvent();
        evt.functionName = "slowmagic";
    }

    public void slowMagic()
    {
        FindObjectOfType<PlayerAnimations>().slowMagic();
    }
    }
