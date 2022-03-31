using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPostClone : MonoBehaviour
{
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        if (transform.parent.gameObject.GetComponent<ShardScript>())
            transform.parent.gameObject.GetComponent<ShardScript>().linkedChild = gameObject;
        transform.parent = null;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.gameObject.transform.position;
    }
}
