using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCrosshair : MonoBehaviour
{
    public GameObject CHx1;
    public GameObject CHy1;
    public GameObject CHx2;
    public GameObject CHy2;
    Vector3 lastPos;
    GameObject player;
    float distance;
    public float crosshairModifier = 55;
    // Start is called before the first frame update
    void Start()
    {
        crosshairModifier = 55;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (player.GetComponent<PlayerMovement>().hasSpeedBoots)
            crosshairModifier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPos != player.transform.position)
        distance = 755*Vector3.Distance(player.transform.position, lastPos);
        CHy1.GetComponent<RectTransform>().localPosition = new Vector2(1, 0)*crosshairModifier/755;
        CHx1.GetComponent<RectTransform>().localPosition= new Vector2(0, 1) * crosshairModifier / 755;
        CHy2.GetComponent<RectTransform>().localPosition = new Vector2(-1, 0) * crosshairModifier / 755;
        CHx2.GetComponent<RectTransform>().localPosition = new Vector2(0, -1) * crosshairModifier / 755;
        lastPos = player.transform.position;
    }
}
