using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Resolution : MonoBehaviour
{
    public gameSettings g;
    // Start is called before the first frame update
    void Update()
    {
        if (g.resolutionDropdown.captionText.text != Screen.width + "x" + Screen.height)
        {
            g.resolutionDropdown.captionText.text = Screen.width + "x" + Screen.height;
            g.qualityDropdown.captionText.text = g.qualityDropdown.options[QualitySettings.GetQualityLevel()].text + "";
        }
    }

}
