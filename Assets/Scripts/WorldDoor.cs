using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WorldDoor : MonoBehaviour
{
    public GameObject enable;
    public bool save;
    public bool setPos;
    public bool intro;
    public bool autoGo;
    public string worldName;
    public GameObject spawnPos;
    public MeshRenderer m;
    public Material closed;
    public Material open;
    public GameObject portal;
    public GameObject player;
    public int shardCountLimit;
    public bool showText;
    private void Start()
    {
        m.material = open;
        if (PlayerPrefs.GetInt("ShardCount") < shardCountLimit)
            m.material = closed;
        else
            portal.SetActive(true);
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 10 && m.isVisible)
        {
            
                string doorText = worldName;
                if (PlayerPrefs.GetInt("ShardCount") < shardCountLimit)
                    doorText = "Need " + shardCountLimit + " Shards ";
            if (showText)
            {
                FindObjectOfType<WorldNameText>().setText(doorText);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (save)
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        if (!intro && PlayerPrefs.GetInt("ShardCount") < shardCountLimit)
            return;
        if (other.gameObject.GetComponent<PlayerMovement>() || other.gameObject.name.Contains("Player"))
        {
            if (enable)
                enable.SetActive(true);
            PlayerPrefs.SetFloat("Money", FindObjectOfType<PlayerMovement>().money);
            if (setPos && spawnPos)
            {
                PlayerPrefs.SetFloat("SpawnX", spawnPos.transform.position.x);
                PlayerPrefs.SetFloat("SpawnY", spawnPos.transform.position.y);
                PlayerPrefs.SetFloat("SpawnZ", spawnPos.transform.position.z);
            }
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            if (autoGo)
            {
                SceneManager.LoadScene(worldName);
                return;
            }
            PlayerPrefs.SetString("CurrentWorld", worldName);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            SceneManager.LoadScene("ShardSelect");
        }
    }
}
