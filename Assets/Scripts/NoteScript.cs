using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public AudioClip clip;
    float canvasTime;
    public GameObject noteChild;
    public GameObject c;
    public Vector3 startPos;
    public GameObject note;
    public bool active;
    PlayerMovement p;
    private void Start()
    {
        note.SetActive(false);
        Time.timeScale = 1;
        p = FindObjectOfType<PlayerMovement>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        active = true;
        foreach (NoteScript s in FindObjectsOfType<NoteScript>())
        {
            if (s != this && s.active)
                Destroy(s.gameObject);
        }
        noteChild.GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        canvasTime = Time.realtimeSinceStartup + 2;
    }

    // Update is called once per frame
    void Update()
    {
        c.SetActive(Time.realtimeSinceStartup < canvasTime);
        if (note.activeInHierarchy)
            Time.timeScale = 0;
        noteChild.transform.localPosition = 3 * Vector3.forward * Mathf.Sin(Time.realtimeSinceStartup);
        if (!active)
            note.SetActive(false);
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            note.SetActive(!note.activeInHierarchy);
            if (note.activeInHierarchy)
            {
                GetComponent<AudioSource>().PlayOneShot(clip);
                p.enabled = false;
                Time.timeScale = 0;
            }
            else {
                p.enabled = true;
                Time.timeScale = 1;
            }
        }
    }
}
