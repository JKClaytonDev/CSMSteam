using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpit : MonoBehaviour
{
    AnimationEvent evt;
    public GameObject blob;
    public bool fireBall;
    // Start is called before the first frame update
    void Start()
    {
        evt = new AnimationEvent();
        evt.functionName = "SpitBlob";
    }

    void SpitBlob()
    {
        GameObject c = Instantiate(blob);
        c.transform.position = transform.position + transform.forward + transform.up * 3;
        if (fireBall) {
            c.GetComponent<Rigidbody>().velocity = c.transform.forward*20;
    }
        Destroy(c, 1.5f);
    }
}
