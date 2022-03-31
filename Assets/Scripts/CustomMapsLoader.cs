using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class CustomMapsLoader : MonoBehaviour
{
    public RawImage image;
    public GameObject button;
    public Text buttonText;
    string baseDir;
    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
        baseDir = Application.dataPath + "\\Addons\\";
        foreach (string file in Directory.GetDirectories(@baseDir))
        {
            string fileName = file.Substring(baseDir.Length, file.Length - baseDir.Length);
            buttonText.text = fileName;
            image.texture = null;
            if (fileName.Substring(0, 4) == "Map_")
            {
                if (System.IO.File.Exists(file + "\\thumbnail.jpg"))
                {
                    byte[] bytes = File.ReadAllBytes(file + "\\thumbnail.jpg");
                    Texture2D LoadedImage = new Texture2D(2, 2);
                    LoadedImage.LoadImage(bytes);
                    image.texture = LoadedImage;
                }
                
                GameObject k = Instantiate(button, button.transform.parent.transform);
                button.name = file;
                k.GetComponent<LoadIntoMap>().Directory = file;
                k.SetActive(true);
            }
        }
        foreach (string file in FindObjectOfType<SteamWorkshopDownload>().levels)
        {
            string fileName = file.Substring(baseDir.Length, file.Length - baseDir.Length);
            buttonText.text = fileName;
            image.texture = null;
            GameObject k = Instantiate(button, button.transform.parent.transform);
            button.name = file;
            k.GetComponent<LoadIntoMap>().Directory = file.Substring(0, file.Length-13);
            k.SetActive(true);
        }
    }

}
