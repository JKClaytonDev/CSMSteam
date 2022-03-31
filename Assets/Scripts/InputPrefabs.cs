using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputPrefabs : MonoBehaviour
{
    public string type;
    public GameObject[] props;
    public GameObject[] entities;
    public GameObject[] models;
    string lastText;
    public Text t;
    public Text fillText;
    public int SelectedIndex;
    public GameObject spawnPos;
    // Start is called before the first frame update

    public void createObject()
    {
        if (type == "Prop")
            newObject(props[SelectedIndex], "Prop", ""+SelectedIndex);
        if (type == "Entity")
            newObject(entities[SelectedIndex], "Entity", "" + SelectedIndex);
        if (type == "Model")
            newObject(models[SelectedIndex], "Model", "");
        
    }
    public void newObject(GameObject clone, string type, string info)
    {
        GameObject k = Instantiate(clone);
        k.transform.position = spawnPos.transform.position;
        k.transform.parent = null;
        k.AddComponent<CustomMapObject>();
        k.GetComponent<CustomMapObject>().type = type;
        k.GetComponent<CustomMapObject>().info = info;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Animator anim in FindObjectsOfType<Animator>())
            Destroy(anim);
        foreach (ZombieScript anim in FindObjectsOfType<ZombieScript>())
            Destroy(anim);
        foreach (RandomMoveDir anim in FindObjectsOfType<RandomMoveDir>())
            Destroy(anim);
        foreach (Rigidbody anim in FindObjectsOfType<Rigidbody>())
            Destroy(anim);
        foreach (EnemyHealth anim in FindObjectsOfType<EnemyHealth>())
            Destroy(anim);
        foreach (ZombieScript anim in FindObjectsOfType<ZombieScript>())
            Destroy(anim);
        if (t.text != lastText)
        {
            
            fillText.text = "                                ";
            if (t.text != "")
            {
                for (int i = 0; i<props.Length; i++)
                {
                    GameObject g = props[i];
                    if (g.name.Contains(t.text))
                    {
                        if (g.name.Substring(0, t.text.Length) == t.text) {
                            if (g.name.Length < fillText.text.Length)
                            {
                                type = "Prop";
                                fillText.text = g.name;
                                SelectedIndex = i;
                            }
                        }
                    }
                }
                for (int i = 0; i < entities.Length; i++)
                {
                    GameObject g = entities[i];
                    if (g.name.Contains(t.text))
                    {
                        if (g.name.Substring(0, t.text.Length) == t.text)
                        {
                            if (g.name.Length < fillText.text.Length)
                            {
                                type = "Entity";
                                fillText.text = g.name;
                                SelectedIndex = i;
                            }
                        }
                    }
                }
                for (int i = 0; i < models.Length; i++)
                {
                    GameObject g = models[i];
                    if (g.name.Contains(t.text))
                    {
                        if (g.name.Substring(0, t.text.Length) == t.text)
                        {
                            if (g.name.Length < fillText.text.Length)
                            {
                                type = "Model";
                                fillText.text = g.name;
                                SelectedIndex = i;
                            }
                        }
                    }
                }
            }
            lastText = t.text;
        }
    }
}
