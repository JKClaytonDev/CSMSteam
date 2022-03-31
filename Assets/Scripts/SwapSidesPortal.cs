using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSidesPortal : MonoBehaviour
{
    public GameObject portal2;
    public float portalTime;
    bool side = true;
    float swapTime;
    Vector3 yPos;
    // Start is called before the first frame update
    private void Update()
    {
        return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.realtimeSinceStartup > swapTime)
            {
                swapTime = Time.realtimeSinceStartup + 0.1f;
                float val = -1;
                if (side)
                    val = 1;
                side = !side;
                FindObjectOfType<PlayerMovement>().gameObject.transform.position += 50 * val * Vector3.right;
                yPos.y *= -1;
                FindObjectOfType<PlayerMovement>().gameObject.transform.position = yPos;
            }
        }
        else
            yPos = FindObjectOfType<PlayerMovement>().gameObject.transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (Time.realtimeSinceStartup < portalTime)
            return;
        if (other.gameObject.name.Contains("Player"))
        {
            portalTime = Time.realtimeSinceStartup + 1;
            portal2.GetComponent<SwapSidesPortal>().portalTime = Time.realtimeSinceStartup + 4;
            FindObjectOfType<PlayerMovement>().gameObject.transform.position = portal2.transform.position;
            
        }
    }
}
