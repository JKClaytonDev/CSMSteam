using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class TextureModManager : MonoBehaviour
{
    public Camera playerCam;
    public Material mt;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.gameObject.GetComponent<HDAdditionalCameraData>().allowDynamicResolution = PlayerPrefs.GetInt("DisableDynamic") != 1;

        foreach (MeshRenderer m in FindObjectsOfType<MeshRenderer>())
        {
            m.gameObject.AddComponent<replaceTextures>();
            m.gameObject.GetComponent<replaceTextures>().replaceMat = mt;
        }
        if (QualitySettings.GetQualityLevel() == 0) {
        RenderSettings.ambientLight = Color.white;
        foreach (Light l in FindObjectsOfType<Light>())
        {
            Destroy(l.gameObject.GetComponent<HDAdditionalLightData>());
            Destroy(l);
        }
            foreach (UnityEngine.Rendering.Volume v in FindObjectsOfType<UnityEngine.Rendering.Volume>())
            {

                VolumeProfile profile = v.profile;
                if (!profile.TryGet<Fog>(out var fog))
                {
                    fog = profile.Add<Fog>(false);
                }

                fog.enabled.overrideState = false;
                fog.enabled.value = false;

                if (!profile.TryGet<IndirectLightingController>(out var ilc))
                {
                    ilc = profile.Add<IndirectLightingController>(false);
                }


                if (!profile.TryGet<Bloom>(out var bloom))
                {
                    bloom = profile.Add<Bloom>(false);
                }


            }
        }

        
    }

}
