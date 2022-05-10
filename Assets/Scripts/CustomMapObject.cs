using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMapObject : MonoBehaviour
{
    public string type;
    public string texture;
    public string info;
    public string objectString;
    Vector3 scale;
    protected void Awake()
    {
        if (GetComponents<CustomMapObject>().Length > 1)
        {
            Destroy(this);
        }
    }
    public void Declare(string typ, string tex, string inf)
    {
        //Debug.Log("DECLARED");
        type = typ;
        texture = tex;
        info = inf;
    }
    private void Update()
    {
        Vector3 rotation = new Vector3();
       
        if (type == "Model")
        {
            rotation = transform.localEulerAngles;
            scale = transform.localScale;
        }
        if (scale != new Vector3())
        transform.localScale = scale;
        objectString = "";
        objectString = "GameObject \"" + name + "\"";
        objectString += "\n";
        objectString += "[\n";
        objectString += "position:";
        objectString += transform.position.x + ",";
        objectString += transform.position.y + ",";
        objectString += transform.position.z;
        objectString += "\n";
        objectString += "rotation:";
        objectString += transform.localEulerAngles.x + ",";
        objectString += transform.localEulerAngles.y + ",";
        objectString += transform.localEulerAngles.z;
        objectString += "\n";
        objectString += "scale:";
        objectString += transform.localScale.x+",";
        objectString += transform.localScale.y+",";
        objectString += transform.localScale.z;
        objectString += "\n";
        objectString += "type:";
        objectString += type;
        objectString += "\n";
        objectString += "texture:";
        objectString += texture;
        objectString += "\n";
        objectString += "info:";
        objectString += info;
        objectString += "\n]";
        scale = transform.localScale;
    }


}
