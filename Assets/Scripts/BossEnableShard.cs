using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnableShard : MonoBehaviour
{
    public GameObject attached;
    public GameObject door;
    Animator a;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        door.SetActive(attached == null);
    }
}
