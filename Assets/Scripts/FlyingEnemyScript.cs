using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject disable;
    public GameObject dust;
    public GameObject player;
    public float Distance;
    Rigidbody rb;
    bool active;

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        active = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = FindObjectOfType<PlayerMovement>().gameObject;
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (disable)
            disable.SetActive(active);
        if (!active && Vector3.Distance(transform.position, player.transform.position) < Distance)
        {
            GameObject k = Instantiate(dust);
            k.transform.position = transform.position;
            active = true;
        }
        if (active)
        {
            Vector3 angles = transform.localEulerAngles;
            transform.LookAt(player.transform);
            rb.velocity = transform.forward * 10;
            transform.localEulerAngles = angles;
        }
    }
}
