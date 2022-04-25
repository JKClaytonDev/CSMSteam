using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloseEnemyManager : MonoBehaviour
{
    public Color TargetC;
    int w = 300;
    public Shader unlit;
    public Shader lit;
    public Texture defaultTex;
    float updateTime;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > updateTime)
        {
            PlayerAttachment[] players = FindObjectsOfType<PlayerAttachment>();
            updateTime = Time.realtimeSinceStartup + 5;
            foreach (closePlayer e in FindObjectsOfType<closePlayer>())
            {
                PlayerAttachment selectedPlayer = players[0];
                float distance = Vector3.Distance(e.transform.position, players[0].transform.position);
                foreach (PlayerAttachment p in players)
                {
                    if (Vector3.Distance(p.transform.position, e.transform.position) < distance)
                    {
                        selectedPlayer = p;
                        distance = Vector3.Distance(p.transform.position, e.transform.position);
                    }
                }
                e.attachedPlayer = selectedPlayer.gameObject;
            }
        }
    }
}
