using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShardCount : MonoBehaviour
{
    public Text shardText;
    // Start is called before the first frame update
    void Start()
    {
        shardText.text = PlayerPrefs.GetInt("ShardCount") + "";
        if (PlayerPrefs.GetInt("ShardCount") == 0 || !PlayerPrefs.HasKey("ShardCount"))
            Destroy(gameObject);
    }


}
