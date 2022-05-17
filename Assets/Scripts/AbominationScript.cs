using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbominationScript : MonoBehaviour
{
    LineRenderer line;
    public GameObject canvas;
    PlayerMovement player;
    public float distance = 15;
    public float damage;
    playerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        player = FindObjectOfType<PlayerMovement>();
        health = FindObjectOfType<playerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.SetActive(false);
        line.enabled = false;
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            health.health -= Time.deltaTime * damage;
            line.enabled = true;
            canvas.SetActive(true);
            line.SetPosition(0, transform.position+new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1))+(Vector3.up*2));
            line.SetPosition(1, player.transform.position - Vector3.up);
            player.slowSpeedTime = Time.realtimeSinceStartup + 0.1f;
            Time.timeScale = Random.Range(0.8f, 1);
        }
    }
}
