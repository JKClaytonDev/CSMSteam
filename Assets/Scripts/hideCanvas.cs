using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideCanvas : MonoBehaviour
{
    public Canvas c;
    // Start is called before the first frame update
    void OnTriggerExit(Collider other)
    {
        FindObjectOfType<PlayerAnimations>().equip1();
        c.enabled = true;
        
        Destroy(gameObject);
    }
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("INSIDE");
        c.enabled = false;
    }

}
