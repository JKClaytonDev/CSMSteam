using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MaxHealthIncrease : MonoBehaviour
{
    public GameObject door;
    public AudioClip sound;
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Missions") == 1)
        {
            if (door)
                door.SetActive(true);
            Destroy(gameObject);
        }
        if (PlayerPrefs.GetInt("FoundHeart" + SceneManager.GetActiveScene().name) == 1){
            if (door)
            door.SetActive(true);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.localEulerAngles += new Vector3(0, 90 * Time.deltaTime, 0);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT HEART2 ");
        if (other.name.Contains("Player"))
        {

            FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().PlayOneShot(sound);
            Debug.Log("HIT HEART");
            PlayerPrefs.SetInt("FoundHeart"+SceneManager.GetActiveScene().name, 1);
            PlayerPrefs.SetFloat("MaxHealth", PlayerPrefs.GetFloat("MaxHealth")+10);
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            FindObjectOfType<playerHealth>().ReRender();
            if (door)
                door.SetActive(true);
            Destroy(gameObject);
        }
    }
}
