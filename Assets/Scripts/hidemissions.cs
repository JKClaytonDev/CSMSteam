using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidemissions : MonoBehaviour
{
    public bool destroy;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("Missions") != 1);
        if (destroy && PlayerPrefs.GetInt("Missions") == 1)
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("Missions") != 1);
        if (destroy && PlayerPrefs.GetInt("Missions") == 1)
            Destroy(gameObject);
    }

}
