using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParadox : MonoBehaviour
{
    public string DestroySetString;
    public int DestroySetValue;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(DestroySetString) == DestroySetValue)
            Destroy(gameObject);
    }
}
