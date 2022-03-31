using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public enemyHealth h;
    AnimationEvent evt;
    public GameObject web;
    public GameObject spider;

    // Start is called before the first frame update
    void Start()
    {
        evt = new AnimationEvent();
        evt.functionName = "fireWeb";
    }
    private void Update()
    {
        if (!h)
            Destroy(gameObject);
        GetComponent<Animator>().speed = 1 + ((1 - h.currentHealth)*2 + Mathf.Sin(Time.realtimeSinceStartup/4)/4)/3; 
    }
    public void fireWeb()
    {
        GameObject k = Instantiate(web);
        k.transform.position = spider.transform.position;
        k.transform.LookAt(FindObjectOfType<PlayerMovement>().gameObject.transform.position);
        k.transform.Rotate(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        k.transform.position += k.transform.forward * 3 * Random.Range(1, 2);
    }
}
