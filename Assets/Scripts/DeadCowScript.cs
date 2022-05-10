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
        try
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }
        catch
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            try
            {
                player = FindObjectOfType<PlayerMovement>().gameObject;
            }
            catch
            {
                return;
            }
        }
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            guts.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }
}
