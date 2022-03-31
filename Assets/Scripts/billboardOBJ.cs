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
    GameObject player;
    bool canTurn;
    Vector3 startPos;
    public bool lockX;
    public bool lockY;
    public bool lockZ;
    public bool mmc;
    // Start is called before the first frame update
    void Start()
    {
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
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (mmc)
        {
            player = Camera.main.gameObject;
        }
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (!player)
            return;
        
        startPos = transform.position;
        if (framePriority)
        {
            transform.LookAt(player.transform);
            transform.Rotate(offset);
            Vector3 angles = transform.localEulerAngles;
            if (lockX)
                angles.x = startRot.x;
            if (lockY)
                angles.y = startRot.y;
            if (lockZ)
                angles.z = startRot.z;
            transform.localEulerAngles = angles;
            return;
        }
        if (Time.frameCount % customUpdate != 0)
            return;
        Vector3 pos = player.transform.position - player.transform.forward * 3;
        transform.LookAt(pos);
        if (customMap)
            transform.LookAt(Camera.main.transform);
        transform.Rotate(offset);
        Vector3 rot = transform.localEulerAngles;
        rot.x = startRot.x;
        rot.z = startRot.z;
        transform.localEulerAngles = rot + (autoRotate * Time.realtimeSinceStartup);
        transform.position = startPos;
        
    }
}
