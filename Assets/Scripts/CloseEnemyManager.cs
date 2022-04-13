using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloseEnemyManager : MonoBehaviour
{
    public Shader unlit;
    public Shader lit;
    public Texture defaultTex;
    float updateTime;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (QualitySettings.GetQualityLevel() == 0)
        {
            foreach (MeshRenderer m in FindObjectsOfType<MeshRenderer>())
            {
                foreach (Material m2 in m.materials)
                {
                    if (m2.shader == lit)
                    {
                        Vector2 scale = m2.mainTextureScale;
                        Texture mainTex = m2.mainTexture;
                        Color c = m2.color;
                        m2.shader = unlit;
                        
                        if (mainTex == null)
                        {
                            mainTex = defaultTex;
                            scale = new Vector3(1, 1);
                        }
                        m2.mainTextureScale.Set(scale.x, scale.y);
                        m2.mainTexture = mainTex;
                        m2.color = c;

                    }
                }
            }
        }
    }
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
