using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteStare : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<WolfMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        transform.Rotate(-transform.rotation.x, 0, -transform.rotation.z);
        
    }
}
