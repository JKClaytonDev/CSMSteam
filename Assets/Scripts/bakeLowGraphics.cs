using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class bakeLowGraphics : MonoBehaviour
{
    public Camera cam2;
    VisualEnvironment visualEnvironment;
    GradientSky gradientSky;
    public GameObject motionBlur;
    public Color TargetC;
    int w = 300;
    public Shader unlit;
    public Shader lit;
    public Texture defaultTex;
    public GameObject LQvolume;
    float updateTime;
    // Start is called before the first frame update

    private void OnEnable()
    {
        //GetComponent<Camera>().GetComponent<HDAdditionalCameraData>().renderingPathCustomFrameSettingsOverrideMask = cam2.GetComponent<HDAdditionalCameraData>().renderingPathCustomFrameSettingsOverrideMask;
        if (PlayerPrefs.GetInt("MotionBlur") == 1)
            Instantiate(motionBlur);
        if (PlayerPrefs.GetInt("DisableLighting") == 1)
        {
            Time.fixedDeltaTime = 0.02f;
            foreach (ParticleSystem m in FindObjectsOfType<ParticleSystem>())
            {
                m.enableEmission = false;
            }


            StartCoroutine(HoldPause());
            
        }
    }
    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();
    IEnumerator Snap()
    {
        
        Camera.main.transform.localPosition += new Vector3(0, 0, 100);
        yield return 0;
        Camera.main.Render();
        yield return frameEnd;
        Texture2D texture = new Texture2D(w, w, TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(0, 0, w, w), 0, 0);
        texture.LoadRawTextureData(texture.GetRawTextureData());
        texture.Apply();

        TargetC = texture.GetPixel(texture.width / 3, texture.height / 3);
        // new Color(1, 1, 1) * ((Mathf.Sin((player.transform.localEulerAngles.y) / 90) * 0.2f) + 0.8f) + new Color(0, 0, 0, 1));

        yield return 0;
        Camera.main.transform.localPosition -= new Vector3(0, 0, 100);
        // gameObject.renderer.material.mainTexture = TakeSnapshot;
    }
    IEnumerator HoldPause()
    {
        Camera.main.nearClipPlane = 0.1f;
        Camera.main.farClipPlane = 0.3f;

        bakeColors();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Snap());
        yield return new WaitForSeconds(0.1f);
        addLighting();

        

    }
    void bakeColors()
    {
        foreach (MeshRenderer m in FindObjectsOfType<MeshRenderer>())
        {
            foreach (Material m2 in m.materials)
            {
                if (m2.shader == lit)
                {
                    Vector2 scale = m2.mainTextureScale;
                    Texture mainTex = m2.mainTexture;
                    Color c = m2.color;
                    m2.shader = unlit;

                    if (mainTex == null)
                    {
                        mainTex = defaultTex;
                        scale = new Vector3(1, 1);
                    }
                    m2.mainTexture = mainTex;
                    m2.mainTextureScale = new Vector2(scale.x, scale.y);
                    m2.color = c;
                }
            }
        }
    }
    void addLighting()
    {
        
        foreach (MeshRenderer m in FindObjectsOfType<MeshRenderer>())
        {
            foreach (Material m2 in m.materials)
            {
                Color c = (m2.color / 5 + TargetC / 2);
                c.a = 1;
                m2.color = c;
            }
        }
        Camera.main.farClipPlane = 100;

        foreach (Volume v in FindObjectsOfType<Volume>())
            Destroy(v);
        GameObject newOBJ = Instantiate(LQvolume);
        RenderSettings.ambientIntensity = 0;
        RenderSettings.ambientLight = Color.white;
        GradientSky sky;
        newOBJ.GetComponent<Volume>().profile.TryGet(out sky);
        sky.top.value = TargetC;
        sky.middle.value = TargetC;
        sky.bottom.value = TargetC;
    }
}
