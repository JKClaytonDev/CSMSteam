using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{
    public GameObject toggle;
    private void OnEnable()
    {
        toggle.SetActive(true);
        Destroy(gameObject);
    }
}
