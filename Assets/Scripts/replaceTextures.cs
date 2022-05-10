using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class replaceTextures : MonoBehaviour
{
    MeshRenderer mesh;
    public Material replaceMat;
    string s = "";
    // Start is called before the first frame update
    void Start()
    {


        s = PlayerPrefs.GetString("GivenDirectory");
        if (s.Substring(s.Length - "Default".Length, "Default".Length) != "Default") {
        mesh = GetComponent<MeshRenderer>();
        foreach (Material m in mesh.materials)
        {
            if (m.mainTexture)
            {
                string mName = m.name.Substring(0, m.name.Length - 11);
                string baseFileName = Application.dataPath + "\\Addons\\Template\\" + mName + ".png";
                    /*
                if (!System.IO.File.Exists(baseFileName))
                {
                    m.mainTexture = DeCompress((Texture2D)m.mainTexture);
                    if (m.mainTexture.isReadable)
                    {
                        byte[] bytes = ((Texture2D)m.mainTexture).EncodeToPNG();
                        File.WriteAllBytes(baseFileName, bytes);
                    }
                }
                    */
                string fileName = s + "\\" + m.mainTexture.name + ".png";

                //Debug.Log("THE FILE IS " + fileName);
                if (System.IO.File.Exists(fileName))
                {
                    //Debug.Log("FOUNDTEX" + fileName);
                    byte[] bytes = File.ReadAllBytes(fileName);
                    Texture2D LoadedImage = new Texture2D(2, 2);
                    if (LoadedImage.LoadImage(bytes))
                    {
                        m.mainTexture = LoadedImage;
                        m.name = "REPLACED";
                    }

                }
            }
        }
        }
        

    }

    public Texture2D DeCompress(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

}
