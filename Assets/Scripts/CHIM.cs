using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHIM : MonoBehaviour
{
    public GameObject a;
    // Start is called before the first frame update
    float step;
    // Update is called once per frame
    void Update()
    {
        if (step == 0 && Input.GetKey(KeyCode.C))
            step = 1;
        if (step == 1 && Input.GetKey(KeyCode.H))
            step = 2;
        if (step == 2 && Input.GetKey(KeyCode.I))
            step = 3;
        if (step == 3 && Input.GetKey(KeyCode.M))
        {
            a.SetActive(true);
            PlayerPrefs.SetInt("Lives", 999);
            PlayerPrefs.Save();
            Destroy(a, 10);
            step = 4;
        }
    }
}
