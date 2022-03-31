using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomAnimation : MonoBehaviour
{
    public Animator a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        a.SetInteger("Attack", Random.Range(0, 2));
    }
}
