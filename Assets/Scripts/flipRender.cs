using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;


public class flipRender : MonoBehaviour
{
    public Camera c;
    
    public RenderTexture r;
    private void OnEnable()
    {
        FindObjectOfType<PlayerMovement>().flipped = -1;
        c.targetTexture = r;
        c.forceIntoRenderTexture = true;
    }
}

