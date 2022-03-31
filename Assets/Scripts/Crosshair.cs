using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().enabled = Input.GetMouseButton(1);
    }
}
