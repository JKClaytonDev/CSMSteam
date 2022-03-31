using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class canvasRemoveParent : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + player.transform.forward*0.6f;
        transform.rotation = player.transform.rotation;
        transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    }
}
