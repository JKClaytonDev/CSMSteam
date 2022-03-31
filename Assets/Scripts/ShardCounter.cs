using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShardCounter : MonoBehaviour
{
    public GameObject shard;
    public Text text;
    int startCount;
    public int subtract = 0;
    // Start is called before the first frame update
    void Start()
    {
        subtract = 1;
        int setStartCount = Resources.FindObjectsOfTypeAll<ShardScript>().Length - 1;
        startCount = setStartCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.parent)
        {
            transform.parent = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }
        int currentCount = Resources.FindObjectsOfTypeAll<ShardScript>().Length - 1;
        text.text = (startCount-currentCount) + "/" + startCount;
        if ((startCount - currentCount) == startCount && !GetComponent<DoorLoadScene>())
        {
            GameObject k = Instantiate(shard);
            k.transform.position = Camera.main.transform.position;
            this.enabled = false;
        }
    }
}
