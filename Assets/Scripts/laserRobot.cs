using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserRobot : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject laser;
    public GameObject player;
    public AudioSource a;
    private void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            laser.SetActive(true);
            a.volume = 5;
        }
        else
        {
            laser.SetActive(false);
            a.volume = 0;
        }
    }
}
