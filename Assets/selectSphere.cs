using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectSphere : MonoBehaviour
{
    public Camera c;
    bool mouseDown;
    GameObject selectedObject;
    Vector3 lastPos;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            

            //if the mouse hits the MeshCollider of the cube
            if (!selectedObject)
            {
                RaycastHit hit;
                Ray ray = c.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.GetComponent<CustomMapObject>() && !hit.transform.gameObject.GetComponent<CustomMeshObject>())
                    {
                        mouseDown = true;
                        selectedObject = hit.transform.gameObject;

                    }

                }
            }
            if (selectedObject)
            {
                if (Input.GetKey(KeyCode.Backspace))
                {
                    Destroy(selectedObject);
                    return;
                }
                FindObjectOfType<hintText>().SetText("Mouse: Move\nQ: Vertical");
                Vector3 move;
                move = c.transform.forward * Input.GetAxis("Mouse Y") + c.transform.right * Input.GetAxis("Mouse X");
                move.y = 0;
                if (Input.GetKey(KeyCode.Q))
                    move = Vector3.up * Input.GetAxis("Mouse Y") + c.transform.right * Input.GetAxis("Mouse X");
                selectedObject.transform.position += move;
                transform.position = selectedObject.transform.position;
            }
        }
        else
        {
            selectedObject = null;
            transform.position = Vector3.up * 455;
        }
    }
    }
