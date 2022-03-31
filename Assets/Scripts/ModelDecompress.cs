using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ModelDecompress : MonoBehaviour
{
    public List<Vector3> Verts;
    public List<Vector2> UV;
    public List<Vector3> Normals;
    string path = "";//"C:\\Users\\jacks\\Downloads\\doom-e1m1-hangar-map\\source\\doom_E1M1.obj";
    // Start is called before the first frame update
    void Start()
    {
        Verts = new List<Vector3>();
        UV = new List<Vector2>();
        Normals = new List<Vector3>();
        StreamReader reader = new StreamReader(path);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (line.ToCharArray().Length > 1)
            {
                if (line.Contains("v "))
                {
                    string[] s = line.Split(' ');
                    if (s.Length == 4)
                    {
                        Verts.Add(new Vector3(float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3])));
                    }
                }
                else if (line.Contains("vt"))
                {
                    string[] s = line.Split(' ');
                    if (s.Length == 3)
                    {
                        UV.Add(new Vector2(float.Parse(s[1]), float.Parse(s[2])));
                    }
                }
                else if (line.Contains("vn"))
                {
                    string[] s = line.Split(' ');
                    if (s.Length == 4)
                    {
                        Normals.Add(new Vector3(float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3])));
                    }
                }
            }
            
        }
        reader.Close();

        Mesh holderMesh = new Mesh();
        holderMesh.name = "jeff";
        holderMesh.vertices = Verts.ToArray();
        holderMesh.uv = UV.ToArray();
        holderMesh.normals = Normals.ToArray();
        GetComponent<MeshFilter>().mesh = holderMesh;
    }

}
