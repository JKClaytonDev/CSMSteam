using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisabel : MonoBehaviour
{
    public GameObject[] disable;
    public bool sound;
    public GameObject[] enable;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            foreach (GameObject o in disable)
                o.SetActive(false);
            foreach (GameObject o in enable)
                o.SetActive(true);
            if (sound || gameObject.name.Contains("Switch"))
                FindObjectOfType<PlayerVoices>().FoundSwitch();
        }
    }
}
