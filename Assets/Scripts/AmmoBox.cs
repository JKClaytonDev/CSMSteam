using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AmmoBox : MonoBehaviour
{
    Vector3 startPos;
    public void Start()
    {
        Time.timeScale = 1;
    }
    public AudioClip sound;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            startPos = transform.position;
            FindObjectOfType<PlayerVoices>().FoundAmmo();
            FindObjectOfType<WeaponsAnim>().refillAmmo();
            FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().PlayOneShot(sound);
            if (!FindObjectOfType<BossEnableShard>())
                Destroy(gameObject);
            else
            {
                transform.position += Vector3.up * -500;
                StartCoroutine(ExampleCoroutine());
            }
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(15);
        GetComponent<Rigidbody>().velocity = new Vector3();
        transform.position = startPos;
    }
}
