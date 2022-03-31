using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShading : MonoBehaviour
{
    public Color TargetC;
    int checks;
    public Color c;
    public GameObject player;
    float checkTime;
    public Material mat;
    public Material mat2;
    RenderTexture MyLowResTexture;
    private Texture2D destinationTexture;
    int w = 300;
    void Start()
    {
        checks = 0;
        w = Screen.height / 2;
        c = Color.white;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        destinationTexture = new Texture2D(w, w, TextureFormat.RGB24, false);
        c = Color.white;
        TargetC = Color.white/2;
        Color final = (c * 2) + (Color.white);

        checkTime = Time.realtimeSinceStartup + 0.5f;

        mat.SetColor("_Color", final);
        mat2.SetColor("_Color", final);
    }

    

    WaitForSeconds waitTime = new WaitForSeconds(0.1F);
    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    public void TakeSnapshot()
    {
        checks++;
        
        Texture2D texture = new Texture2D(w, w, TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(0, 0, w, w), 0, 0);
        texture.LoadRawTextureData(texture.GetRawTextureData());
        texture.Apply();

        TargetC = texture.GetPixel(texture.width / 3, texture.height / 3);
        TargetC.r *= 2;
        TargetC.g *= 2;
        TargetC.b *= 2;
        // new Color(1, 1, 1) * ((Mathf.Sin((player.transform.localEulerAngles.y) / 90) * 0.2f) + 0.8f) + new Color(0, 0, 0, 1));


        // gameObject.renderer.material.mainTexture = TakeSnapshot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.realtimeSinceStartup> checkTime)
        {
            TakeSnapshot();
            checkTime = Time.realtimeSinceStartup + 1.5f;
        }
        if (checks > 4)
            c = Color.Lerp(c, TargetC, 0.03f);
        else
            c = Color.Lerp(c, TargetC, 0.3f);
        Color final = (c) + (Color.white * 2);
        
        final = final / 3;
        final.r = Mathf.Min(0.9f, final.r);
        final.g = Mathf.Min(0.9f, final.g);
        final.b = Mathf.Min(0.9f, final.b);
        final.a = 1;
        //final = final * ((Mathf.Sin((player.transform.localEulerAngles.y) / 90) * 0.1f) + 0.9f) + new Color(0, 0, 0, 1);
        mat.SetColor("_Color", final);
        mat2.SetColor("_Color", final);
    }

    
}
