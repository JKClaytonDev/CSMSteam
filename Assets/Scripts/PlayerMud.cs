using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMud : MonoBehaviour
{
    closePlayer closePlayerObject;
    public bool ice;
    public GameObject particles;
    private void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("mudhit " + other.gameObject.name);
        if (other.gameObject.name.Contains("Player"))
        {
            if (closePlayerObject)
            {
                closePlayerObject.attachedPlayer.GetComponent<PlayerMovement>().inMud = !ice;
                closePlayerObject.attachedPlayer.GetComponent<PlayerMovement>().inIce = ice;
            }
            else
            {
                FindObjectOfType<PlayerMovement>().inMud = !ice;
                FindObjectOfType<PlayerMovement>().inIce = ice;
            }
            particles.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Vector3 pos = other.gameObject.transform.position;
            pos.y = particles.transform.position.y;
            particles.transform.position = pos;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            FindObjectOfType<PlayerMovement>().inMud = false;
            FindObjectOfType<PlayerMovement>().inIce = false;
            particles.SetActive(false);
        }
    }
}
