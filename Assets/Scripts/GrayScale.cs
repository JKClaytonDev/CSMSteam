using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GrayScale : MonoBehaviour
{
    public bool flip;
    public Material defaultWep;
    public Material grayScale;
    Image i;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().material = defaultWep;
            return;

        GetComponent<Image>().material = grayScale;
        if (flip)
            transform.localEulerAngles = new Vector3();
    }

    
}
