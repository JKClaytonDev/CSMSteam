using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTime : MonoBehaviour
{
    public float time = 0.1f;
    GameObject coldBreath;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > time)
            gameObject.SetActive(false);
    }
}
