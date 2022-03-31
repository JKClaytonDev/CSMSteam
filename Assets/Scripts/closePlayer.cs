using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closePlayer : MonoBehaviour
{
    public GameObject attachedPlayer;
    private void Start()
    {

        attachedPlayer = FindObjectOfType<PlayerAttachment>().gameObject;

        PlayerAttachment[] players = FindObjectsOfType<PlayerAttachment>();
        attachedPlayer = players[0].gameObject;
        float distance = Vector3.Distance(transform.position, attachedPlayer.transform.position);
        foreach (PlayerAttachment p in players)
        {
            if (Vector3.Distance(p.transform.position, transform.position) < distance)
            {
                distance = Vector3.Distance(p.transform.position, transform.position);
                attachedPlayer = p.transform.gameObject;
            }
        }
    }
}
