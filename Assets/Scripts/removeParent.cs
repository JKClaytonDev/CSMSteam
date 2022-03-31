using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeParent : MonoBehaviour
{
    public bool defaultMove;
    GameObject player;
    public Vector3 setScale;
    public bool moon;
    public bool skip;
    // Start is called before the first frame update
    void Start()
    {
        if (defaultMove)
        {
            transform.parent = null;
            this.enabled = false;
            return;
        }
        if (moon)
        {
            float Shardcount = 5+PlayerPrefs.GetInt("ShardCount");

            transform.localPosition += (1920 - (Shardcount * 75/1920)) * Vector3.up;
            transform.parent = null;
            transform.localScale = new Vector3(1, 1, 1) * Shardcount * 4;

        }
        player = transform.parent.gameObject;
        transform.parent = null;
        if (setScale != new Vector3())
            transform.localScale = setScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (skip)
            return;
        if (!moon)
        transform.position = player.transform.position;
        if (setScale != new Vector3())
            transform.localScale = setScale;
    }
}
