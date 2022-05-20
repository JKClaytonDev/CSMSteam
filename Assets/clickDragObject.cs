using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clickDragObject : MonoBehaviour
{
    public string prefabName;
    public RawImage i;
    public RawImage reference;
    // Start is called before the first frame update
    public void createObject()
    {
        reference.texture = i.texture;
        FindObjectOfType<InputPrefabs>().setText(prefabName);
    }
}
