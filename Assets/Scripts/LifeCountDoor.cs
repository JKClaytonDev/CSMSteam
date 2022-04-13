using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LifeCountDoor : MonoBehaviour
{
    public GameObject openDoor;
    private void Start()
    {
        openDoor.SetActive(PlayerPrefs.GetInt("Lives") >= 25);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (PlayerPrefs.GetInt("Lives") >= 25)
            {
                SceneManager.LoadScene("FinalCastle1");
                this.enabled = false;
            }
        }
    }
}
