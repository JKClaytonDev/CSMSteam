using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlayerLookAt : MonoBehaviour
{
    closePlayer closePlayerObject;
    Vector3 lastPlayerPos;
    GameObject player;
    void OnEnable()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPlayerPos == new Vector3())
            lastPlayerPos = FindObjectOfType<PlayerMovement>().gameObject.transform.position;
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (!player)
            player = FindObjectOfType<PlayerMovement>().gameObject;
        player.transform.position = lastPlayerPos;
        player.transform.LookAt(transform.position);
        player.GetComponent<PlayerMovement>().MX = player.transform.localEulerAngles.y;
        player.GetComponent<PlayerMovement>().MY = 0;
        
    }
}
