using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    Vector3 startAngles;
    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(-90, transform.localPosition.x * 15, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0, Time.deltaTime*90, 0);
    }
}
