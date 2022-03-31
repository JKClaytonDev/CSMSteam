using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeNighttime : MonoBehaviour
{
    public GameObject volume;
    public GameObject sunsetAnim;
    GameObject s;
    private void Start()
    {
        if (PlayerPrefs.GetInt("ShardCount") < 7)
            this.enabled = false;
        s = Instantiate(volume);
    }
    // Update is called once per frame
    void Update()
    {

        string minute = (System.DateTime.Now.Minute + "");
        char[] mins = minute.ToCharArray();
        string c = ""+mins[minute.Length - 1];
        int min = int.Parse(c); ;
        Debug.Log("MINUTE IS " + min);
        bool lastActive = s.activeInHierarchy;
        s.SetActive(min == 0);
        GetComponent<PlayerMovement>().nightTime = (min == 0);
        if (s.activeInHierarchy && !lastActive)
        {
            GameObject f = Instantiate(sunsetAnim);
            Destroy(f, 4);
        }
    }
}
