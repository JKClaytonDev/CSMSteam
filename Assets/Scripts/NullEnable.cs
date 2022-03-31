using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullEnable : MonoBehaviour
{
    public bool matchPos;
    public GameObject nullOBJ;
        public GameObject enableOBJ;


    // Update is called once per frame
    void Update()
    {
        enableOBJ.SetActive(nullOBJ == null);
        if (nullOBJ)
        {
            if (matchPos)
            enableOBJ.transform.position = nullOBJ.transform.position;
        }
        else
            this.enabled = false;
    }
}
