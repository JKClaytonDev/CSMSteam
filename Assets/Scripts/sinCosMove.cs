using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinCosMove : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 lPos = transform.localPosition;
        lPos.z = (Mathf.Sin(Time.realtimeSinceStartup)+1) * -2;
        transform.localPosition = lPos;
    }
}
