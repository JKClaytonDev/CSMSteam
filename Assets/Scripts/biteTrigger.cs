using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biteTrigger : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject player;
    float biteTime;
    public float biteSplit;
    public float distance;
    public int biteCount;
    // Start is called before the first frame update

    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = FindObjectOfType<PlayerMovement>().gameObject;
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            if (Time.realtimeSinceStartup > biteTime)
            {
                biteTime = Time.realtimeSinceStartup + biteSplit;
                for (int i = 0; i < biteCount; i++)
                {
                    FindObjectOfType<UniversalAudio>().bite();
                    FindObjectOfType<playerHealth>().health -= 10;
                }
            }
        }
    }
}
