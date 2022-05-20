using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMeshObject : MonoBehaviour
{
    public string n;
    public string v;
    public string u;
    public string t;
    public string fullString;
    public void GenMesh()
    {
        string build = "";
        for (int i = 1; i < fullString.Length; i++)
        {
            char c = fullString[i];
            if (c == 'n') {
                v = build;
                build = "";
            }
            else if (c == 'u') {
                n = build;
                build = "";
            }
            else if (c == 't') {
            u = build;
            build = "";
            }
            else
            build += c;
        }
        if (v.Length == 0)
            return;
        t = build;
        FindObjectOfType<FaceExtrusion>().normalsString = n;
        FindObjectOfType<FaceExtrusion>().uvsString = u;
        FindObjectOfType<FaceExtrusion>().verticesString = v;
        FindObjectOfType<FaceExtrusion>().trianglesString = t;
        FindObjectOfType<FaceExtrusion>().hitTransform = transform;
        FindObjectOfType<FaceExtrusion>().loadMesh();
        FindObjectOfType<FaceExtrusion>().reloadMesh(GetComponent<MeshFilter>());
    }
}
