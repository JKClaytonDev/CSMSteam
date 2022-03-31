using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseNull : MonoBehaviour
{
    public GameObject check;
    
    // Update is called once per frame
    void Update()
    {
        if (check == null)
        {
            transform.position -= Vector3.up * Time.deltaTime * 3;
        }
    }
}
