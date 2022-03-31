using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class YouDied : MonoBehaviour
{
    public Canvas attached;
    public GameObject sceneImage;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Random.insideUnitSphere;
        GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere);
    }
    private void OnEnable()
    {
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c!= attached)
            {
                c.enabled = false;
            }
        }
        foreach (billboardOBJ k in FindObjectsOfType<billboardOBJ>())
            k.enabled = false;
        foreach (enemyHealth k in FindObjectsOfType<enemyHealth>())
            k.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("Mission") == 0)
        {
            PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Lives") == 0)
        {
            PlayerPrefs.SetInt("Lives", 5);
            if (PlayerPrefs.GetInt("Missions") != 1)
            {
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameOver");
            }

        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (PlayerPrefs.GetInt("Mission") == 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                sceneImage.SetActive(true);
                this.enabled = false;
            }
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            if (PlayerPrefs.GetInt("Lives") <= 0)
            {
                PlayerPrefs.SetInt("Lives", 5);
                if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();
                    SceneManager.LoadScene("GameOver");
                }
                
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            sceneImage.SetActive(true);
            this.enabled = false;
        }
    }
}
