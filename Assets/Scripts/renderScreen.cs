using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderScreen : MonoBehaviour
{
    public Camera playerCam;
    public Camera renderCam;
    public GameObject block;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        renderCam.transform.position = playerCam.transform.position;
        renderCam.transform.localEulerAngles = new Vector3(0, playerCam.transform.parent.localEulerAngles.y, 0);
        float px = playerCam.transform.parent.localEulerAngles.x;
        px /= 60;
        if (px >= 5)
            px = -6 + px;

        block.transform.localPosition = new Vector3(0, px, 1.34f);
        Vector3 ang = playerCam.transform.eulerAngles;
        ang.x = 0;
        playerCam.transform.eulerAngles = ang;
    }
}
