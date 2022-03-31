using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    public Image i;
    public Text t1;
    public Text t2;
    bool found;
    // Start is called before the first frame update
    void Start()
    {
        found = GameObject.Find("BossTitle");
        i.enabled = found;
        t1.enabled = found;
        t2.enabled = found;
    }


}
