using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileMapEditor : MonoBehaviour
{
    public Button b;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -15; i<15; i++)
        {
            for (int j = -15; j < 15; i++)
            {
                GameObject newButton = Instantiate(b.gameObject);
                newButton.transform.parent = b.transform.parent;
                newButton.GetComponent<RectTransform>().localPosition = new Vector2(i * 10, j * 10);
                
            }
        }
    }
}
