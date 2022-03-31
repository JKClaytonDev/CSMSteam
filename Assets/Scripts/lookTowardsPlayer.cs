using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookTowardsPlayer : MonoBehaviour
{
    closePlayer closePlayerObject;
    Vector3 lastEU;
    public GameObject player;
    // Update is called once per frame
    private void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        lastEU = transform.eulerAngles;
        //transform.parent = null;
    }
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        else
            player = FindObjectOfType<PlayerMovement>().gameObject;
        GameObject parent = transform.parent.gameObject;
        transform.parent = null;
        transform.eulerAngles = lastEU;
        transform.LookAt(player.transform.position-Vector3.up);
        Vector3 targetRot = transform.eulerAngles;
        lastEU = Vector3.RotateTowards(lastEU, targetRot, Time.deltaTime * 25, Time.deltaTime * 25);
        transform.localEulerAngles = lastEU;
        transform.parent = parent.transform;
    }
}
