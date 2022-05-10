using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBillboard : MonoBehaviour
{
    private Camera theCam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private Camera cam;


    void LateUpdate()
    {
        if (cam != null)
        {
            var lookPos = transform.position - (cam.transform.position - cam.transform.forward*3);
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
            transform.Rotate(90, 180, 0);
        }
        else
        {
            cam = Camera.main;
        }
    }
    /*
    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.LookAt(theCam.transform.position - theCam.transform.forward);
        }
        catch
        {
            try
            {
                theCam = FindObjectOfType<PlayerMovement>().mainCam;
            }
            catch
            {
                return;
            }
        }
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
    }
    */
}
