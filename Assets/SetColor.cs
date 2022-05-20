using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetColor : MonoBehaviour
{
    public Material m;
    public Image i;
    float r;
    float g;
    float b;
    
    public void setR(float rIn)
    {
        r = rIn;
        UpdateColor();
    }
    public void setG(float gIn)
    {
        g = gIn;
        UpdateColor();
    }
    public void setB(float bIn)
    {
        b = bIn;
        UpdateColor();
    }
    // Update is called once per frame
    void UpdateColor()
    {
        i.color = new Color(r, g, b);
        m.color = i.color;
    }
}
