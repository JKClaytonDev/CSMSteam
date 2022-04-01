using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFound : MonoBehaviour
{
    public string title;
    bool check;
    public float checkTimeChange;
    float checkTime;
    public string desc;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent) {
            if (transform.parent.gameObject.GetComponent<enemyHealth>())
                checkTimeChange = 1;
                }
        checkTime = Time.realtimeSinceStartup + checkTimeChange;
        check = false;
        int page = -1;
        while (!check)
        {
            page++;
            if (PlayerPrefs.GetString("PageTitle" + page) == title)
            {
                Destroy(gameObject);
                return;

            }
            check = (!PlayerPrefs.HasKey("PageTitle" + page));
        }
        check = false;

    }
    private void Update()
    {
        if (Time.realtimeSinceStartup > checkTime && !check)
        {
            findPage();
            check = true;
        }
        if (check && Input.GetKey(PlayerPrefs.GetString("FlashlightKeybind")))
        {
            Destroy(gameObject);
        }
    }
    public void findPage()
    {
        int page = -1;
        while (!check)
        {
            page++;
            if (PlayerPrefs.GetString("PageTitle" + page) == title)
            {
                Destroy(gameObject);
                return;

            }
            check = (!PlayerPrefs.HasKey("PageTitle" + page));
        }
        PlayerPrefs.SetString("PageTitle" + page, title);
        PlayerPrefs.SetString("PageText" + page, desc);
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
    }

}
