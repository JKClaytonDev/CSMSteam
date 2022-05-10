using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class loadingScreen : MonoBehaviour
{
    public GameObject loaded;
    public Text loadingPercentage;
    public Text necro;
    public Text world;
    public Text title;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return null;
        int max = 0;

        for (int page = 0; page < 100; page++)
        {
            if (PlayerPrefs.HasKey("PageText" + page))
                max = page;
        }
        int random = Random.Range(0, max);
        necro.text = PlayerPrefs.GetString("PageText" + random);

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LoadingScreenScene"));
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            loadingPercentage.text = (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                loadingPercentage.text = "Press space to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
                    loaded.SetActive(true);
                }
            }

            yield return null;
        }
    }
}
