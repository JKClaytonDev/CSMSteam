using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShardSelectHover : MonoBehaviour, IPointerEnterHandler
{
    public ButtonShardSelect b;
    public int index;
    public Text t;
    public string scene;
    public GameObject crystal;
    public GameObject collectedShard;
    public Material m;

    // Start is called before the first frame update
    private void Start()
    {
        crystal.transform.parent = null;
        if (PlayerPrefs.GetInt(PlayerPrefs.GetString("CurrentWorld") + index) == 1)
        {
            crystal.GetComponent<MeshRenderer>().material = m;
            
        }
        if (!(index == 1 || PlayerPrefs.GetInt("PlayedLevel" + PlayerPrefs.GetString("CurrentWorld") + (index - 1)) == 1))
            {
            GameObject k = Instantiate(collectedShard);
            k.transform.position = crystal.transform.position;
            crystal.transform.parent = null;
            k.transform.localScale = crystal.transform.localScale;
            Destroy(crystal);
            Destroy(gameObject);
            }
            if (!Application.CanStreamedLevelBeLoaded(PlayerPrefs.GetString("CurrentWorld") + index)){
            Destroy(gameObject);
            Destroy(crystal);
            
        }
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        scene = (PlayerPrefs.GetString("CurrentWorld") + index);
        int sceneIndex = 0;
        for (int i = 0; i<b.levelNames.Length; i++)
        {
            if (b.levelNames[i].Equals(PlayerPrefs.GetString("CurrentWorld") + index))
                sceneIndex = i;
        }
        PlayerPrefs.SetString("MenuLevelTitle", b.levelTitles[sceneIndex]);
        t.text = b.levelTitles[sceneIndex];
    }
}


