using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TakePicture : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public Material m;
    void Start()
    {
        webCamTexture = new WebCamTexture();
       m.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        webCamTexture.Play();
    }
    private void Update()
    {
        if (Time.realtimeSinceStartup > 5)
            webCamTexture.Stop();
    }
    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {

        // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        m.SetTexture("_MainTex", photo);
        
    }
}
