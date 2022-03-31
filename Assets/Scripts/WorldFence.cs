using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WorldFence : MonoBehaviour
{
    public int StarThreshold;
    public string loadScene;
    GameObject player;
    public GameObject canvas;
    Text t;
    int count;
    public GameObject cameraObj;
    private void Start()
    {
        //FindObjectOfType<WeaponsAnim>().cVAS.enabled = false;
        count = PlayerPrefs.GetInt("ShardCount");
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (cameraObj && StarThreshold + 1 == count)
        {
            cameraObj.SetActive(true);
            cameraObj.transform.parent = null;
            Destroy(cameraObj, 3);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (t)
            t.enabled = (Vector3.Distance(transform.position, player.transform.position) < 5);
        else
            t = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        t.text = StarThreshold + " Shards to Unlock";
        if (StarThreshold <= count)
        {
            if (!PlayerPrefs.HasKey(loadScene) && loadScene != "")
                SceneManager.LoadScene(loadScene);
            Destroy(gameObject);
        }
        
    }
}
