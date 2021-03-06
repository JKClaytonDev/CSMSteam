using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoRotate : MonoBehaviour
{
    public Vector3 targetRot = new Vector3(0, 0, 0);
    public float spinTime = 0;
    public Vector3 goalRot;
    GameObject player;
    Vector3 lastPlayerRot;
    bool doubleTake;
    bool set = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        spinTime = Time.realtimeSinceStartup;
        foreach (EnemyHealth h in FindObjectsOfType<EnemyHealth>())
            h.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (goalRot == transform.localEulerAngles && Input.GetKeyDown(PlayerPrefs.GetString("FlashlightKeybind")))
        {
            Vector3 lastTargetRot = targetRot;
            set = false;
            doubleTake = !doubleTake;
            spinTime = Time.realtimeSinceStartup + 15;
            while (targetRot == lastTargetRot)
            {
                if (Random.Range(1, 3) == 1)
                    targetRot = new Vector3(90, 0, 0);
                else if (Random.Range(1, 2) == 1)
                    targetRot = new Vector3(0, 90, 0);
                else
                    targetRot = new Vector3(0, 0, 90);
            }
            transform.RotateAround(FindObjectOfType<PlayerMovement>().transform.position, targetRot, 90);
            goalRot = transform.localEulerAngles;
            transform.RotateAround(FindObjectOfType<PlayerMovement>().transform.position, targetRot, -90);

        }
        if (Vector3.Distance(transform.localEulerAngles, goalRot) < 5)
        {
            transform.localEulerAngles = goalRot;
            
            set = true;
        }
        else
            transform.RotateAround(FindObjectOfType<PlayerMovement>().transform.position, targetRot, Time.deltaTime * 45);

    }
}
