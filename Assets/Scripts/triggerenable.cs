using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerenable : MonoBehaviour
{
    public GameObject g;
    public GameObject destroyObj;
    public bool destroy;
    public GameObject canvas;
    float CanvasTime;
    private void Update()
    {
        if (canvas)
        canvas.SetActive(Time.realtimeSinceStartup < CanvasTime);
    }
    private void OnTriggerStay(Collider other)
    {
        if (canvas && other.gameObject.name.Contains("Player"))
        {
            CanvasTime = Time.realtimeSinceStartup + 0.1f;
            if (!Input.GetKey(KeyCode.Tab))
                return;
        }
        if (other.gameObject.name.Contains("Player"))
        {
            if (canvas)
            {
                Destroy(canvas);
                g.SetActive(true);
                this.enabled = false;
            }
            if (destroyObj)
                Destroy(destroyObj);
            if (destroy)
            {
                g.transform.parent = null;
                g.SetActive(true);
                Destroy(gameObject);
                return;
            }
            
            g.transform.parent = null;
            g.SetActive(true);
            Destroy(g, 5);
        }
    }
}
