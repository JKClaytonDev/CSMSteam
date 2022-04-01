using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
public class MapMakerCamera : MonoBehaviour
{
    Vector3 startScale;
    float camDistance;
    public GameObject copied;
    public GameObject selectedObject;
    float camSpeed;
    bool inputMode;
    int blockMode = 1;
    Vector3 rot;
    bool objectDrag;
    public GameObject modelText;
    Vector3 lastSelectedPos;
    public Text t;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        camDistance = 40;
        startScale = new Vector3();
        Time.timeScale = 1;
        Physics.gravity = new Vector3();
        camSpeed = 15;
    }
    public void loadModel()
    {
        selectedObject.GetComponent<CustomModel>().loadModel(PlayerPrefs.GetString("CustomMapDirectory") + "\\" + t.text + ".obj");
    }
    // Update is called once per frame
    void Update()
    {

        if (selectedObject)
            modelText.SetActive(selectedObject.GetComponent<CustomModel>());
        else
            modelText.SetActive(false);
        transform.position += Vector3.up * Input.mouseScrollDelta.y;
        
        if (Input.GetMouseButton(1))
        {
            camDistance += Input.mouseScrollDelta.y;
            pos += (Vector2.right * Input.GetAxis("Mouse X") - Vector2.up * Input.GetAxis("Mouse Y")) * 3;
        }
        transform.position = transform.forward * -camDistance + transform.up * pos.y - transform.right * pos.x;
        if (Input.GetMouseButton(2))
        {
            if (Input.GetKey(KeyCode.LeftControl))
                transform.localEulerAngles = new Vector3(90, 90, 0);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            transform.Rotate(-Input.GetAxis("Mouse Y") * 45, Input.GetAxis("Mouse X") * 45, 0);
            return;
        }
        //Selecting Blocks
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.gameObject.GetComponent<CantSelect>())
                {
                    Debug.Log("SELECTED " + hit.transform.gameObject.name);
                    selectedObject = hit.collider.gameObject;

                }
            }
        }

        if (selectedObject && selectedObject.GetComponent<CantSelect>())
            selectedObject = null;
        if (Input.GetMouseButton(0))
        {
            
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(r, out mouseHit) && selectedObject)
            {
                if (mouseHit.transform.gameObject == selectedObject || objectDrag)
                {
                    objectDrag = true;
                    while (selectedObject.gameObject.transform.parent)
                        selectedObject = selectedObject.gameObject.transform.parent.gameObject;
                    if (blockMode == 1)
                    {
                        
                        Vector3 newPos = selectedObject.transform.position;
                        newPos.z = mouseHit.point.z;
                        newPos.x = mouseHit.point.x;
                        newPos.y = selectedObject.transform.position.y;
                        if (lastSelectedPos != new Vector3())
                            selectedObject.transform.position += newPos - lastSelectedPos;
                        lastSelectedPos = newPos;
                        selectedObject.transform.position += Vector3.up * Input.mouseScrollDelta.y*Time.deltaTime;
                    }
                    if (blockMode == 2)
                    {
                        selectedObject.transform.Rotate(new Vector3(-25 * Input.GetAxis("Mouse Y"), 25 * Input.GetAxis("Mouse X"), 0));
                    }
                    if (blockMode == 3)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            float scaleX = hit.point.x - selectedObject.transform.position.x;
                            float scaleZ = hit.point.z - selectedObject.transform.position.z;
                            Vector3 scale = startScale;
                            scale.x = scaleX;
                            scale.z = scaleZ;
                            if (Input.GetKey(KeyCode.LeftControl))
                            {
                                selectedObject.transform.localScale += new Vector3(1, 1, 1) * (Input.GetAxis("Mouse X") + Input.GetAxis("Mouse Y"));
                            }
                            else
                            {
                                selectedObject.transform.localScale += scale - startScale;
                                startScale = scale;
                            }
                        }
                    }
                    return;
                }
            }

        }
        else
        {
            lastSelectedPos = new Vector3();
            objectDrag = false;
        }
        startScale = new Vector3();
        if (Input.GetKeyDown(KeyCode.LeftControl))
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyUp(KeyCode.LeftControl))
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        if (Time.realtimeSinceStartup < 1 )
        {
            selectedObject = null;
            return;
        }
        UnityEngine.Cursor.visible = true;
        //Input
        //Universal Keys
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            copyBlock();
            Debug.Log("COPY");
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("PASTE");
            pasteBlock(copied);
            
        }
        else if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            breakBlock();
        }
        else if (Input.GetKeyDown(KeyCode.C) && Input.GetMouseButton(1))
            inputMode = !inputMode;
        else if (Input.GetKeyDown(KeyCode.O) && Input.GetMouseButton(1))
            GetComponent<Camera>().orthographic = !GetComponent<Camera>().orthographic;
        //Input modes
        if (inputMode)
        {
            Vector3 move = new Vector3(Time.deltaTime / Time.timeScale * 250 * -Input.GetAxis("Mouse Y"), Time.deltaTime / Time.timeScale * 250 * Input.GetAxis("Mouse X"), 0);
            transform.position += 15 * camSpeed * Time.deltaTime / Time.timeScale * (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right);
            if (Input.GetKey((PlayerPrefs.GetString("GhostKeybind"))))
            {
                if (Mathf.Abs(move.x) > Mathf.Abs(move.y) && Mathf.Abs(move.x) > Mathf.Abs(move.z))
                {
                    move.y = 0;
                    move.z = 0;
                }
                else if (Mathf.Abs(move.y) > Mathf.Abs(move.z) && Mathf.Abs(move.y) > Mathf.Abs(move.x))
                {
                    move.x = 0;
                    move.z = 0;
                }
                else if (Mathf.Abs(move.z) > Mathf.Abs(move.y) && Mathf.Abs(move.z) > Mathf.Abs(move.x))
                {
                    move.x = 0;
                    move.y = 0;
                }
            }
            transform.localEulerAngles += move;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        

        Vector3 dirr = transform.localEulerAngles;
        Vector3 newDir = dirr;
        newDir.x = 0;
        newDir.z = 0;
        transform.localEulerAngles = newDir;

        //Moving Blocks
        if (Input.GetKey("1"))
            setBlockMode(1);
        if (Input.GetKey("2"))
            setBlockMode(2);
        if (Input.GetKey("3"))
            setBlockMode(3);
        

        transform.localEulerAngles = dirr;
    }

    public void pasteBlock(GameObject block)
    {
        GameObject g = Instantiate(block);
        g.transform.parent = null;
        selectedObject = g;
    }
    public void pasteBlock(int block)
    {
        GameObject g = Instantiate(FindObjectOfType<CustomMapMaker>().prefabs[block]);
        g.transform.parent = null;
        selectedObject = g;
    }
    public void placeBlock(int block)
    {
        GameObject g = Instantiate(FindObjectOfType<CustomMapMaker>().prefabs[block]);
        g.transform.parent = null;
        Vector3 pos = transform.position + transform.forward * 55;
        g.transform.position = pos;
        selectedObject = g;
    }
    public void placeBlock(GameObject block)
    {
        GameObject g = Instantiate(block);
        g.transform.parent = null;
        Vector3 pos = transform.position + transform.forward * 55;
        g.transform.position = pos;
        selectedObject = g;
    }
    public void copyBlock()
    {
        if (selectedObject)
            copied = selectedObject;
    }
    public void setBlockMode(int mode) {
        blockMode = mode;
    }
    public void cloneBlock()
    {
        if (selectedObject)
        {
            GameObject g = Instantiate(selectedObject);
            g.transform.parent = null;
            selectedObject = g;
        }
    }
    public void breakBlock()
    {
        while (selectedObject.gameObject.transform.parent)
            selectedObject = selectedObject.gameObject.transform.parent.gameObject;
        Destroy(selectedObject);

    }
}
