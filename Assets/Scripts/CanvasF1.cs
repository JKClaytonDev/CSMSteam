using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasF1 : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().enabled = false;

        }
    }
}
