using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCowScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    public float distance = 20;
    GameObject player;
    public GameObject guts;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            guts.SetActive(true);
            gameObject.SetActive(false);
        }
        if (!player)
            player = FindObjectOfType<PlayerMovement>().gameObject;
    }
}
