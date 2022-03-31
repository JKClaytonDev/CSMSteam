using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("CurrentWorld"));
        foreach (Transform t in transform)
        {
            if (t.gameObject.GetComponent<World>())
                t.gameObject.SetActive(t.name == PlayerPrefs.GetString("CurrentWorld"));
            if (t.name == PlayerPrefs.GetString("CurrentWorld"))
            {
                foreach (Transform g in t)
                {
                    t.parent = transform;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
