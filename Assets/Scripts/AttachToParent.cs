using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToParent : MonoBehaviour
{
    GameObject parent;
    public Vector3 offset;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        parent = transform.parent.gameObject;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.transform.position + offset;
        transform.LookAt(player.transform);
        transform.Rotate(90, 0, 0);
    }
}
