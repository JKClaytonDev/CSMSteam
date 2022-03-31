using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    public int dirDifference;
    Vector3 defaultRot;
    public Material[] sprites;
    
    // Start is called before the first frame update
    void Start()
    {
        
        defaultRot = FindObjectOfType<PlayerMovement>().transform.localEulerAngles;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(Camera.main.transform);
        transform.Rotate(90, 0, 0);

        Vector3 angles = transform.eulerAngles;

        while (angles.y > 90)
            angles.y -= 90;
        while (angles.y < 0)
            angles.y += 90;
        angles.x = 0;
        angles.z = 0;
        transform.eulerAngles = angles;
    }
}
