using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class prefabPhotoBooth : MonoBehaviour
{

    public GameObject prefab;
    public RawImage rawImg;
    public Text objectName;
    Vector3 photoPos;
    float localX = -50;
    // Start is called before the first frame update
    void Start()
    {
        photoPos = new Vector3(-50, 100, 0);
        rawImg.gameObject.SetActive(false);
    }
    public void CreatePrefabPhoto(GameObject b)
    {
        GameObject k = Instantiate(rawImg.gameObject);
        k.gameObject.SetActive(true);
        k.transform.parent = rawImg.gameObject.transform.parent;
        k.transform.localScale = rawImg.transform.localScale;
        k.GetComponent<RectTransform>().localPosition = photoPos + new Vector3(76, -(76 * 2), 0);
        takePhoto(b, k.GetComponent<RawImage>());
        photoPos.x += 50;
        if (photoPos.x == 150)
        {
            photoPos.y -= 50;
            photoPos.x = -50;
        }
    }
    public void takePhoto(GameObject b, RawImage i)
    {
        Vector3 oldCamPos = Camera.main.transform.position;
        Camera.main.transform.position = transform.position;
        i.gameObject.GetComponent<viewportImage>().t.GetComponent<Text>().text = b.name;
        i.gameObject.GetComponent<viewportImage>().g.prefabName = b.name;
        GameObject newO = Instantiate(b);
        newO.transform.parent = null;
        foreach (Animator anim in FindObjectsOfType<Animator>())
            Destroy(anim);
        foreach (ZombieScript anim in FindObjectsOfType<ZombieScript>())
            Destroy(anim);
        foreach (RandomMoveDir anim in FindObjectsOfType<RandomMoveDir>())
            Destroy(anim);
        foreach (Rigidbody anim in FindObjectsOfType<Rigidbody>())
            Destroy(anim);
        foreach (EnemyHealth anim in FindObjectsOfType<EnemyHealth>())
            Destroy(anim);
        foreach (ZombieScript anim in FindObjectsOfType<ZombieScript>())
            Destroy(anim);
        GameObject selected = newO;
        selected.transform.position = transform.position + transform.forward * 3;
        if ((selected.gameObject.GetComponent<billboardOBJ>() || selected.gameObject.GetComponent<treeBillboard>()))
        {
            if (selected.gameObject.GetComponent<treeBillboard>())
                selected.gameObject.transform.Rotate(selected.gameObject.GetComponent<treeBillboard>().setOffset);
            transform.parent = selected.transform;
            transform.localPosition = Vector3.up * 17;
            transform.LookAt(selected.transform);
        }
        else
        {
            foreach (Transform c in newO.transform)
            {
                c.LookAt(transform);
                if (c.gameObject.GetComponent<MeshRenderer>())
                {
                    selected = c.gameObject;
                }
                if (c.gameObject.GetComponent<Animator>())
                {
                    Destroy(c.gameObject.GetComponent<Animator>());
                }
            }
            transform.parent = selected.transform;
            transform.localPosition = Vector3.up * 7;
            transform.parent = null;
            selected.transform.position = transform.position + transform.forward * 3;
            transform.LookAt(selected.transform);
        }
        
        
        
        GetComponent<Camera>().targetTexture = RenderTexture.GetTemporary(128, 128, 16);
        GetComponent<Camera>().Render();
        RenderTexture saveActive = RenderTexture.active;
        RenderTexture.active = GetComponent<Camera>().targetTexture;
        float width = GetComponent<Camera>().targetTexture.width;
        float height = GetComponent<Camera>().targetTexture.height;

        Texture2D texturee = new Texture2D(128, 128);

        texturee.ReadPixels(new Rect(0, 0, texturee.width, texturee.height), 0, 0);

        texturee.Apply();

        transform.parent = null;
        newO.SetActive(false);
        Destroy(selected);
        Destroy(newO);
        Camera.main.transform.position = oldCamPos;
        i.GetComponent<RawImage>().texture = texturee;
        
    }
}
