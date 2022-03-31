using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resumeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(PlayerPrefs.HasKey("LockScene"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
