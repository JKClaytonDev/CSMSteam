using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SageFence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool a = PlayerPrefs.GetInt("Sage1") == 1;
        bool b = PlayerPrefs.GetInt("Sage2") == 1;
        bool c = PlayerPrefs.GetInt("Sage3") == 1;
        if (a && b && c)
            SceneManager.LoadScene("FreedAllSages");
    }


}
