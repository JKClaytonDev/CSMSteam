using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyShardCount : MonoBehaviour
{
    public float shardCountLimit;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ShardCount") > shardCountLimit)
            Destroy(gameObject);
    }


}
