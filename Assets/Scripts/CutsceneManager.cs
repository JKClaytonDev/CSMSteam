using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject vamp;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ShardCount") == 0)
            vamp.SetActive(true);
    }

}
