using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelComplete : MonoBehaviour
{
    public GameObject killsOBJ;
    public Text killsText;
    public GameObject cashOBJ;
    public Text cashText;
    public GameObject clockOBJ;
    public Text clockText;
    public GameObject pressKeyText;
    void Start()
    {
        killsText.gameObject.SetActive(false);
        cashText.gameObject.SetActive(false);
        clockText.gameObject.SetActive(false);
        killsOBJ.SetActive(false);
        killsText.text = "";
        cashOBJ.SetActive(false);
        cashText.text = "";
        clockOBJ.SetActive(false);
        clockText.text = "";
        pressKeyText.SetActive(false);
        StartCoroutine(DoEveryFiveSeconds());
    }

    IEnumerator DoEveryFiveSeconds()
    {

        yield return new WaitForSeconds(1.5f);
        killsOBJ.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        killsText.gameObject.SetActive(true);
        killsText.text = (int)PlayerPrefs.GetFloat("LevelKills")+"";
        yield return new WaitForSeconds(0.75f);
        cashOBJ.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        cashText.gameObject.SetActive(true);
        float money = PlayerPrefs.GetFloat("LevelStartCash");
        money = Mathf.Max(0, money);
        cashText.text = "$" +  money;
        yield return new WaitForSeconds(0.75f);
        clockOBJ.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clockText.gameObject.SetActive(true);
        float levelTime = Time.realtimeSinceStartup - PlayerPrefs.GetFloat("LevelStartTime");

        string minutes = Mathf.Floor(levelTime / 60).ToString("00");
        string seconds = (levelTime % 60).ToString("00");


        clockText.text = (string.Format("{0}:{1}", minutes, seconds)); ;
        yield return new WaitForSeconds(1.5f);
        pressKeyText.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown && pressKeyText.activeInHierarchy)
        {
            SceneManager.LoadScene("Hub1");
        }
    }
}
