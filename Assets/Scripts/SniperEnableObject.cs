using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnableObject : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
    }

}
