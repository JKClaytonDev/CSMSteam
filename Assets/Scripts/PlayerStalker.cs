using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStalker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = Camera.main.transform.position;
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0, 0, 0);
    }
}
