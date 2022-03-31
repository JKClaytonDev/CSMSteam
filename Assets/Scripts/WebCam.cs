using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        WebCamTexture webcamTexture = new WebCamTexture();
        webcamTexture.Stop();
        WebCamDevice[] devices = WebCamTexture.devices;
        int index = 0;
        Debug.Log("PICKED " + devices[index].name);
        webcamTexture.deviceName = devices[index].name;
        Renderer renderer = GetComponent<Renderer>();
        webcamTexture.Play();
        renderer.material.mainTexture = webcamTexture;
    }
}
