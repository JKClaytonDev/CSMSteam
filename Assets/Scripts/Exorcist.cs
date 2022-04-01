using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exorcist : MonoBehaviour
{
    Vector3 oldPos;
    public GameObject image;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Exorcist f in GameObject.FindObjectsOfType<Exorcist>())
        {
            if (f != this)
                Destroy(f.gameObject);
        }
        player = FindObjectOfType<PlayerMovement>().gameObject;
        image.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (oldPos != new Vector3())
            player.transform.position = oldPos;
        image.GetComponent<RectTransform>().localScale -= new Vector3(0.2f*Time.deltaTime, 0, 0);
        if (image.GetComponent<RectTransform>().localScale.x < 0.1f)
            image.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 1, 1);
        if (Input.GetKeyDown((PlayerPrefs.GetString("GhostKeybind"))) || Input.GetKeyDown("joystick button 4"))
        {
            image.GetComponent<RectTransform>().localScale += new Vector3(0.1f, 0, 0);
        }
        if (image.GetComponent<RectTransform>().localScale.x > 1.2f)
            Destroy(gameObject);
        oldPos = player.transform.position;
    }
}
