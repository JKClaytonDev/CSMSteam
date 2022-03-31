using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameSettings : MonoBehaviour
{
    public Slider s;
    public Text SliderText;
    public Slider GraphicsSlider;
    public Text GraphicsSliderText;
    UnityEngine.Resolution startRes;
    public Slider VolumeSlider;
    public Text VolumeSliderText;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Dropdown textureDropdown;
    public Dropdown aaDropdown;
    public Slider volumeSlider;
    float currentVolume;
    public UnityEngine.Resolution[] resolutions;
    public void setFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    // Start is called before the first frame update
    private void Start()
    {
        qualityDropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        if (!PlayerPrefs.HasKey("MouseSens"))
            PlayerPrefs.SetFloat("MouseSens", 10);
        s.value = PlayerPrefs.GetFloat("MouseSens");
        VolumeSlider.value = AudioListener.volume * 100;
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> resOptions = new List<string>();
        resolutions = Screen.resolutions;
        // Print the resolutions
        foreach (var res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
            string option = (res.width + "x" + res.height + " : " + res.refreshRate);
            resOptions.Add(option);

        }
        resolutionDropdown.AddOptions(resOptions);

        bool fullScreen = Screen.fullScreen;
        Screen.fullScreen = false;
        Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        Screen.fullScreen = true;
        s.value = PlayerPrefs.GetFloat("MouseSens");
        startRes = Screen.currentResolution;
        Screen.fullScreen = fullScreen;

        resolutionDropdown.captionText.text = Screen.currentResolution.ToString();
        qualityDropdown.captionText.text = qualityDropdown.options[QualitySettings.GetQualityLevel()].text + "";
    }
    void OnEnable()
    {
        
        resolutionDropdown.captionText.text = Screen.currentResolution.ToString();
        qualityDropdown.captionText.text = qualityDropdown.options[ QualitySettings.GetQualityLevel() ].text+"";
    }
    public void SetResolution(int resolutionIndex)
    {
        UnityEngine.Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,
                  resolution.height, Screen.fullScreen);
    }
    public void apply()
    {
        Screen.SetResolution((int)(startRes.width * GraphicsSlider.value), (int)(startRes.height * GraphicsSlider.value), Screen.fullScreen);
    }
    public void SetSens(float f)
    {

    }
    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = VolumeSlider.value/100;
        VolumeSliderText.text = Mathf.Round(VolumeSlider.value)+"";
        SliderText.text = Mathf.Round(s.value) + "";
        PlayerPrefs.SetFloat("MouseSens", s.value);
        PlayerPrefs.Save();
    }
    public void SetQuality(int qualityIndex)
{
        Debug.Log("Graphics Index is " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
        if (qualityIndex != 6) // if the user is not using 
                               //any of the presets
		
	switch (qualityIndex)
	{
		case 0: // quality level - very low
                textureDropdown.value = 3;
			aaDropdown.value = 0;
			break;
		case 1: // quality level - low
                textureDropdown.value = 2;
			aaDropdown.value = 0;
			break;
		case 2: // quality level - medium
                textureDropdown.value = 1;
			aaDropdown.value = 0;
			break;
		case 3: // quality level - high
                textureDropdown.value = 0;
			aaDropdown.value = 0;
			break;
		case 4: // quality level - very high
                textureDropdown.value = 0;
			aaDropdown.value = 1;
			break;
		case 5: // quality level - ultra
                textureDropdown.value = 0;
			aaDropdown.value = 2;
			break;
	}

        qualityDropdown.value = qualityIndex;
    }
    public void setGraphics(int g)
    {
        QualitySettings.SetQualityLevel(g);
    }
    public void changeResolution(float multi)
    {
        Screen.SetResolution((int)(Screen.width * multi), (int)(Screen.height * multi), Screen.fullScreen);
    }
    public void toggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
