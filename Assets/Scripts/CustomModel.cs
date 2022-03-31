using System.Collections;
using Dummiesman;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class CustomModel : MonoBehaviour
{
    public string directory;
    public void loadModel(string path)
    {

        directory = path;
        GetComponent<CustomMapObject>().info = directory;
        if (path == "")
            return;
        transform.GetComponent<MeshFilter>().mesh = new Mesh();


        
        for (int f = 0; f < transform.childCount; f ++)
            Destroy(transform.GetChild(f).gameObject);

        GameObject g = new OBJLoader().Load(path);
        g.transform.parent = transform;
        g.transform.localPosition = new Vector3();
        g.transform.localEulerAngles = new Vector3();
        g.transform.localScale = new Vector3(1, 1, 1);

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            meshFilters[i].mesh.name = "cloned mesh";
            for (int j = 0; j < meshFilters[i].GetComponent<MeshRenderer>().materials.Length; j++)
            {
                meshFilters[i].gameObject.GetComponent<MeshRenderer>().materials[j].name = "test";
                Material tex = meshFilters[i].gameObject.GetComponent<MeshRenderer>().materials[j];
                Texture2D mainTex = new Texture2D(2, 2);
                if (tex.mainTexture)
                {
                    mainTex = Instantiate((Texture2D)tex.mainTexture);
                }
                meshFilters[i].gameObject.GetComponent<MeshRenderer>().materials[j].shader = Shader.Find("HDRP/Lit");
                meshFilters[i].gameObject.GetComponent<MeshRenderer>().materials[j].mainTexture = mainTex;
            }
            meshFilters[i].gameObject.AddComponent<MeshCollider>().sharedMesh = meshFilters[i].sharedMesh;
            i++;

            
        }

        

    }
    }
