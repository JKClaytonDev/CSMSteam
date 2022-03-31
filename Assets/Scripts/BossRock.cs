using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : MonoBehaviour
{
    Animator a;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
    }

    public void throwRock()
    {
        a.Play("RockFly");
        Destroy(gameObject, 1f);
    }
}
