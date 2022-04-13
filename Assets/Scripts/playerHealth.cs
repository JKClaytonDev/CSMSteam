using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public GameObject dead;
    float startTime;

    public Sprite fullSprite;
    public Sprite emptySprite;

    public Image image;
    public float health = 100;
    float healthTime;
    float lastHealth = 100;
    public float maxHealth;
    public GameObject heartImage;
    public GameObject[] heartImages;
    float lastSavedHealth;
    float iframeTime;
    float lockhealth;
    private void Start()
    {
        startTime = Time.realtimeSinceStartup;

        if (!PlayerPrefs.HasKey("MaxHealth"))
            PlayerPrefs.SetFloat("MaxHealth", 50);
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}

        ReRender();
        health = 50;
    }

    public void ReRender()
    {
        maxHealth = PlayerPrefs.GetFloat("MaxHealth");
        if (PlayerPrefs.GetInt("Missions") == 1)
        {
            maxHealth = 70;
        }
        Vector3 pos = heartImage.GetComponent<RectTransform>().position;
        heartImage.SetActive(true);
        heartImages = new GameObject[(int)(maxHealth / 10)];
        for (int i = 0; i < maxHealth; i += 10)
        {
            Debug.Log("HEART IMAGE");

            GameObject f = Instantiate(heartImage);
            f.transform.SetParent(heartImage.transform.parent.transform, false);
            f.GetComponent<RectTransform>().position = pos;
            f.transform.localScale = heartImage.transform.localScale;
            heartImages[i / 10] = f;
            pos += new Vector3(100 * Screen.width/2560, 0, 0);
        }
        heartImage.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup < iframeTime)
        {
            health = lockhealth;
        }
        else if (Mathf.Abs(lastSavedHealth-health) > 30)
        {
            lastSavedHealth = health;
            iframeTime = Time.realtimeSinceStartup + 1;
            lockhealth = health;
        }
        for (int i = 0; i < maxHealth; i += 10)
        {
            heartImages[i / 10].GetComponent<Image>().sprite = fullSprite;
            if (i >= health)
                heartImages[i / 10].GetComponent<Image>().sprite = emptySprite;
        }
            if (health < lastHealth)
        {
            healthTime = Time.realtimeSinceStartup + 0.1f;
            lastHealth = health;
        }
        if (health > maxHealth)
        {
            health = (int)maxHealth;
        }
        image.enabled = (Time.realtimeSinceStartup < healthTime || health < 30) && Time.realtimeSinceStartup > (1+startTime);

        if (health < 0)
        {
            Vector3 pos = Camera.main.transform.position;
            GameObject d = Instantiate(dead);
            d.transform.parent = null;
            d.transform.position = pos;
            Destroy(FindObjectOfType<PlayerMovement>().gameObject);
        }
            
        GetComponent<Text>().text = (int)health+"";
    }
}
