using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeBillboard : MonoBehaviour
{
    private Camera theCam;
    // Start is called before the first frame update
    void Start()
    {
        theCam = FindObjectOfType<PlayerMovement>().mainCam;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(theCam.transform.position-theCam.transform.forward);
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
    }
}
