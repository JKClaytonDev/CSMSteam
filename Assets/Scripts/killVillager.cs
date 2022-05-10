using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killVillager : MonoBehaviour
{
    public MeshRenderer m;
    public enemyHealth e;
    public GameObject destroy;
    // Start is called before the first frame update
    void OnEnable()
    {
        //Debug.Log("MATERIAL NAME IS " + m.material.name + "CONTAINS" + m.material.name.Contains("Village"));
        if (!(m.material.name.Contains("Village")))
        {
            Destroy(e);
            Destroy(destroy);
        }
    }


}
