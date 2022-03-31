using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoicesReverb : MonoBehaviour
{
    public GameObject voices;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = FindObjectOfType<PlayerVoices>().gameObject.transform;
        transform.localPosition = new Vector3();
    }


}
