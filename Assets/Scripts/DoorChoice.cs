using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChoice : MonoBehaviour
{
    closePlayer closePlayerObject;
    GameObject player;
    public GameObject canvas;
    public int side;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        canvas.SetActive(false);
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (PlayerPrefs.GetInt("PickedDoor") == 0)
            return;
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        canvas.SetActive(Vector3.Distance(transform.position, player.transform.position) < 10 && PlayerPrefs.GetInt("PickedDoor") == 0);
        if (canvas.activeInHierarchy && Input.GetKey(PlayerPrefs.GetString("FlashlightKeybind")) && PlayerPrefs.GetInt("PickedDoor") == 0)
        {
            PlayerPrefs.SetInt("PickedDoor", side);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
        }
        if (PlayerPrefs.GetInt("PickedDoor") == side)
            transform.position -= Vector3.up * 10 * Time.deltaTime;
    }
}
