using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeOtherParent : MonoBehaviour
{
    public GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        other.transform.parent = null;
    }

}
