using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardOBJ : MonoBehaviour
{
    closePlayer closePlayerObject;
    bool customMap;
    Vector3 startRot;
    public Vector3 autoRotate;
    public bool removeParent;
    public int customUpdate = 1;
    public bool framePriority;
    public Vector3 offset = new Vector3(0, -90, 0);
    public GameObject player;
    bool canTurn;
    Vector3 startPos;
    public bool lockX;
    public bool lockY;
    public bool lockZ;
    public bool mmc;
    bool classic;
    private Transform myTransform;
    public void initBBJ()
    {
        myTransform = transform;
        Debug.Log("INIT " + Time.frameCount);
        classic = FindObjectOfType<WolfMovement>();
        if (FindObjectOfType<MapMakerCamera>())
        {
            mmc = true;
            return;
        }
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        customUpdate = 1;
        customMap = FindObjectOfType<CustomMapMaker>();
        if (removeParent)
            transform.parent = null;
        startRot = transform.localEulerAngles;
        player = Camera.main.gameObject;
    }
    public void setPlayer(GameObject p)
    {
        player = p;
    }
    // Start is called before the first frame update
    void Start()
    {
        initBBJ();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = myTransform.position;
        if (framePriority)
        {
            try {
                myTransform.LookAt(player.transform);
            }
            catch
            {
                player = Camera.main.gameObject;
            }
            myTransform.Rotate(offset);
            Vector3 angles = transform.localEulerAngles;
            if (lockX)
                angles.x = startRot.x;
            if (lockY)
                angles.y = startRot.y;
            if (lockZ)
                angles.z = startRot.z;
            myTransform.localEulerAngles = angles;
            return;
            }
        if (Time.frameCount % customUpdate != 0)
            return;
        Vector3 pos = player.transform.position - player.transform.forward * 3;
        myTransform.LookAt(pos);
        if (customMap)
            myTransform.LookAt(Camera.main.transform);
        myTransform.Rotate(offset);
        Vector3 rot = myTransform.localEulerAngles;
        rot.x = startRot.x;
        rot.z = startRot.z;
        myTransform.localEulerAngles = rot + (autoRotate * Time.realtimeSinceStartup);
        myTransform.position = startPos;
        
    }
}
