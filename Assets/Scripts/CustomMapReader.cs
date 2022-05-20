using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CustomMapReader : MonoBehaviour
{
    public GameObject baseObject;
    public string path = "";
    int lineIndex;
    StreamReader reader;
    public CustomMapObject[] objects;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        path = PlayerPrefs.GetString("CustomMapDirectory") + "\\mapdata.ffwt";
        reader = new StreamReader(path);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (line.Contains("GameObject"))
                addObject(readObject(line));
        }
        reader.Close();
        
    }
    public void addObject(CustomMapObject c)
    {
        CustomMapObject[] newObjects = new CustomMapObject[objects.Length + 1];
        for (int i = 0; i < objects.Length; i++)
            newObjects[i] = objects[i];
        newObjects[newObjects.Length - 1] = c;
        objects = newObjects;
    }
    public CustomMapObject readObject(string objectName)
    {
        string line;
        Vector3 position = new Vector3();
        Vector3 rotation = new Vector3();
        Vector3 scale = new Vector3();
        string type = "";
        string texture = "";
        string info = "";

        while ((line = reader.ReadLine()) != "]")
        {
            if (line.Contains("position:"))
                position = parseV3(line);
            if (line.Contains("rotation:"))
                rotation = parseV3(line);
            if (line.Contains("scale:"))
                scale = parseV3(line);
            if (line.Contains("type:"))
                type = readString(line);
            if (line.Contains("texture:"))
                texture = readString(line);
            if (line.Contains("info:"))
                info = readString(line);
        }
        InputPrefabs i = baseObject.GetComponent<InputPrefabs>();
        if (type == "Model")
        {
            return createObject(i.models[0], type, info, position, scale, rotation, texture, objectName).GetComponent<CustomMapObject>();
        }
        if (type == "Prop")
        {
            return createObject(i.props[int.Parse(info )], type, info, position, scale, rotation, texture, objectName).GetComponent<CustomMapObject>();
        }
        if (type == "Entity")
        {
            return createObject(i.entities[int.Parse(info)],type,info,position,scale,rotation,texture,objectName).GetComponent<CustomMapObject>();
        }
        if (type == "Mesh")
        {
            CustomMapObject c = createObject(cube, type, info, position, scale, rotation, texture, objectName).GetComponent<CustomMapObject>();
            c.GetComponent<CustomMeshObject>().fullString = info;
            c.GetComponent<CustomMeshObject>().GenMesh();
            return c;
        }
        return null;

    }


    public GameObject createObject(GameObject inObject, string type, string info, Vector3 position, Vector3 scale, Vector3 rotation, string texture, string objectName)
    {
        GameObject c = Instantiate(inObject);
        if (!c.GetComponent<CustomMapObject>())
        {
            c.AddComponent<CustomMapObject>();
        }
        c.GetComponent<CustomMapObject>().type = type;
        c.GetComponent<CustomMapObject>().info = info;
        if (type == "Model")
        {
            if (!c.GetComponent<CustomModel>())
                c.AddComponent<CustomModel>();
            c.GetComponent<CustomModel>().loadModel(info);
        }

        c.transform.position = position;
        c.transform.parent = null;
        c.transform.localScale = scale;
        c.transform.localEulerAngles = rotation;
        c.GetComponent<CustomMapObject>().Declare(type, texture, info);
        c.name = objectName.Substring(12, objectName.Length - 13);
        return c;
    }
    public string readString(string s)
    {

        string[] sArray = s.Split(':');

        return sArray[sArray.Length - 1];
    }
    public Vector3 parseV3(string s)
    {
        string [] sArray = s.Split(':');
        s = sArray[sArray.Length - 1];
        string[] vArray = s.Split(',');
        return new Vector3(
        float.Parse(vArray[0]),
        float.Parse(vArray[1]),
        float.Parse(vArray[2]));
    }
    public void SaveMap()
    {
        string finalString = "Castillo Custom Map Format\n\ngeometry\n{\n";
        foreach (CustomMapObject c in FindObjectsOfType<CustomMapObject>())
        {
            finalString += c.objectString + "\n";
        }
        finalString += "\n}";
    //Debug.Log("FINAL STRING " + finalString);
        File.WriteAllText(path, finalString);

    }
}
