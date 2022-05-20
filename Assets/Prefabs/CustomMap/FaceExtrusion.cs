using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Globalization;
public class FaceExtrusion : MonoBehaviour
{
	Mesh lastMesh;
	Mesh nextMesh;

	Vector3 moveDir;

	Vector3[] vertPos = new Vector3[4];
	Vector3[] vertices;
	Vector3 startPos;
	public Camera c;
	List<GameObject> vertCubes = new List<GameObject> ();
	GameObject extrudingCube;
	GameObject endPos;
	public GameObject viewTarget;
	public GameObject vertCubeObject;
	bool selected;
	bool faceSelected;
	bool createVerticesOnce;

	public string verticesString;
	public Vector3[] storeVertices;
	public string normalsString;
	public Vector3[] storeNormals;
	public string uvsString;
	public Vector2[] storeUVs;
	public string trianglesString;
	public int[] storeTriangles;

	MeshCreator meshCreate;
	Mesh mesh;

	int rotateSpeed = 5;
	int[] triangles;

	float cubeSize = 0.5f;

	RaycastHit hit;
	
	public Transform hitTransform;
	public void saveMesh()
    {
		verticesString = "";
		normalsString = "";
		uvsString = "";
		foreach (Vector3 v in viewTarget.GetComponent<MeshFilter>().mesh.vertices)
			verticesString += v + ",";
		foreach (Vector3 v in viewTarget.GetComponent<MeshFilter>().mesh.normals)
			normalsString += v + ",";
		foreach (Vector3 v in viewTarget.GetComponent<MeshFilter>().mesh.uv)
			uvsString += v + ",";
		foreach (int v in viewTarget.GetComponent<MeshFilter>().mesh.triangles)
			trianglesString += v + ",";
	}
	public void loadMesh()
	{
		//(0.50, -0.50, 0.50),(-0.50, -0.50, 0.50),(0.50, 0.50, 0.50),(-0.50, 0.50, 0.50),(0.50, 0.50, -0.50),(-0.50, 0.50, -0.50),(0.50, -0.50, -0.50),(-0.50, -0.50, -0.50),(0.50, 0.50, 0.50),(-0.50, 0.50, 0.50),(0.50, 0.50, -0.50),(-0.50, 0.50, -0.50),(0.50, -0.50, -0.50),(0.50, -0.50, 0.50),(-0.50, -0.50, 0.50),(-0.50, -0.50, -0.50),(-0.50, -0.50, 0.50),(-0.50, 0.50, 0.50),(-0.50, 0.50, -0.50),(-0.50, -0.50, -0.50),(0.50, -0.50, -0.50),(0.50, 0.50, -0.50),(0.50, 0.50, 0.50),(0.50, -0.50, 0.50),
		hitTransform.gameObject.GetComponent<CustomMapObject>().info = "v" + verticesString + "n" + normalsString + "u" + uvsString + "t" + trianglesString;
		hitTransform.gameObject.GetComponent<CustomMapObject>().type = "Mesh";
		storeVertices = scrapeString(verticesString); ;
		storeNormals = scrapeString(normalsString);
		storeUVs = scrapeStringV2(uvsString);
		storeTriangles = scrapeInt(trianglesString);

		Mesh m = hitTransform.gameObject.GetComponent<MeshFilter>().mesh;
		m.Clear();
		m.name = "STORED";
		
		m.vertices = storeVertices;
		m.normals = storeNormals;
		m.uv = storeUVs;
		m.triangles = storeTriangles;

		hitTransform.gameObject.GetComponent<MeshFilter>().mesh = m;
		Vector3[] v = m.vertices;
		string full = "a";
		foreach (Vector3 k in v)
        {
			full += k + " ";
        }
		Debug.Log("LOADED MESH " + full);
		hitTransform = null;
	}
	public void reloadMesh(MeshFilter m2)
    {
		Mesh m = m2.mesh;
		m.Clear();
		m.name = "STORED";

		m.vertices = storeVertices;

		m.triangles = storeTriangles;
		m.Optimize();
		m.RecalculateNormals();

		//m.uv = storeUVs;
		//m.normals = storeNormals;


		m2.mesh = m;
	}
	void Start ()
	{
		//Instantiate a copy of the Cube mesh so I can alter it without any problems to the original Cube mesh 
		Mesh newMesh = viewTarget.GetComponent<MeshFilter> ().sharedMesh;
		newMesh = (Mesh) Instantiate( newMesh );
		viewTarget.GetComponent<MeshFilter> ().sharedMesh = newMesh;
		viewTarget.GetComponent<MeshCollider> ().sharedMesh = newMesh;

		meshCreate = GetComponent<MeshCreator> ();
	}
	

	void Update ()
	{
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Z) && lastMesh)
        {
			nextMesh = lastMesh;
			hitTransform.gameObject.GetComponent<MeshFilter>().mesh = lastMesh;
		}
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Y) && nextMesh)
		{
			lastMesh = nextMesh;
			hitTransform.gameObject.GetComponent<MeshFilter>().mesh = lastMesh;
		}
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		//If the left mouse button is clicked or held
		if (Input.GetMouseButton(0) || (Input.GetKey(KeyCode.LeftShift)))
		{
			
			//If the mouse is being clicked on the cube, GetVertices will retrieve all vertices and faceSelected will become true
			if (!faceSelected)
				GetVertices ();
			else {
				FindObjectOfType<hintText>().SetText("Shift: Extrude\nMouse: Amplification\n");
				selected = true;
				//check if the vertices have been found by the number of cubes and check if the mouse is being dragged up (like the provided GIF example)
				if (vertCubes.Count != 0 && (Input.GetKey(KeyCode.LeftShift))) {
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
					//create once a visual cube which gives feedback on the size of the extrusion
					if(!createVerticesOnce)
						CreateVisualExtrudeCube ();
					Vector3 move;
					move = c.transform.forward * Input.GetAxis("Mouse Y") + c.transform.right * Input.GetAxis("Mouse X");
					move.y = 0;
					if (Input.GetKey(KeyCode.Q))
					move = Vector3.up * Input.GetAxis("Mouse Y") + c.transform.right * Input.GetAxis("Mouse X");
					
					if (Mathf.Abs(move.x) > Mathf.Abs(move.z))
						move.z = 0;
					else if (Mathf.Abs(move.x) < Mathf.Abs(move.z))
						move.x = 0;
					if (move.x == move.z)
						move.x = 0;

					//move forward each vertice as the mouse is dragged
					for (int i = 0; i < vertPos.Length; i++)
					{
						move = vertCubes[i].transform.forward * Input.GetAxis("Mouse Y");
						vertCubes[i].transform.position += move * Time.deltaTime * 600;
					}
					//also move forward the endPos, which dictates the size of the visual extruded cube
					endPos.transform.position += move * Time.deltaTime * 600;
					//then calculate the between position of the startPos and endPos, and calculate the scale to keep resizing the cube
					Vector3 between = endPos.transform.position - startPos;
					float distance = between.magnitude;
					extrudingCube.transform.localScale = new Vector3(cubeSize, cubeSize, distance);
					extrudingCube.transform.position = startPos + (between / 2);
				}
			}
		}

		//when the mouse button is up, change the bool values and, if an extrusion cube was created, create the mesh and attach it to the original
		else if (selected) {
			selected = false;
			faceSelected = false;

			if(createVerticesOnce)
				CreateNewMesh();

			createVerticesOnce = false;
		}


		//if the right mouse button is held and moved, the camera will rotate around the cube
		if (Input.GetMouseButton(2)) {
			transform.LookAt (viewTarget.transform);
			transform.RotateAround (viewTarget.transform.position, Vector3.up, Input.GetAxis ("Mouse X") * rotateSpeed);
			transform.RotateAround (viewTarget.transform.position, Vector3.left, Input.GetAxis ("Mouse Y") * rotateSpeed);
		}
		if (Input.GetMouseButton(1))
        {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			transform.position += transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 60;
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - Input.GetAxis("Mouse Y"), transform.localEulerAngles.y + Input.GetAxis("Mouse X"), 0);
		}

	}


	//This method will create the new mesh and attach it to the original mesh
	void CreateNewMesh()
	{
		List<Vector3> endPosVerts = new List<Vector3> ();
		//add the original vertices found on the mesh to the endPosVerts list
		endPosVerts.AddRange (vertPos);

		//each cube, which are the vertices, will be moved to the desired extrude position and here I get the positions of each of them
		foreach(GameObject cube in vertCubes)
		{
			endPosVerts.Add(cube.transform.position);
			Destroy(cube);
		}

		//finalVertArray will receive the ordered array of new vertices to be added to the mesh
		Vector3[] finalVertArray = meshCreate.ArrangeVerticesForCube (endPosVerts.ToArray ());

		//change all new vertices to local space which fixes the positioning
		for(int i = 0; i < finalVertArray.Length; i++)
		{
			finalVertArray[i] = hitTransform.InverseTransformPoint(finalVertArray[i]);
		}

		//add the mesh verts and the new vertices to a list, then add it to the mesh
		endPosVerts.Clear ();
		endPosVerts.AddRange (mesh.vertices);
		endPosVerts.AddRange (finalVertArray);
		mesh.vertices = endPosVerts.ToArray ();

		//calculate the new triangles and add them to the mesh triangles array
		List<int> triList = new List<int> ();
		int[] newTriangles = meshCreate.ArrangeTrianglesForCube (mesh.triangles.Length);
		triList.AddRange (mesh.triangles);
		triList.AddRange (newTriangles);
		mesh.triangles = triList.ToArray ();

		//reset and destroy some variables 
		vertCubes.Clear ();
		Destroy (extrudingCube);
		Destroy (endPos);
		//remove and add the MeshCollider so the collider is updated with the new mesh formation
		Destroy (hitTransform.gameObject.GetComponent<MeshCollider> ());
		hitTransform.gameObject.AddComponent <MeshCollider>();
		hitTransform.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();
		saveMesh();
	}
	
	Vector3[] scrapeString(string s)
    {
		string check = "x";
		string scrape = "";
		Vector3 stored = new Vector3();
		Vector3[] scraped = new Vector3[0];
		for (int i = 0; i < s.Length; i++)
		{
			char c = s.ToCharArray()[i];
			if (c == '(' || c == ',' || c == ')')
            {
				Debug.Log(scrape);
				if (scrape != "")
                {
					CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
					ci.NumberFormat.CurrencyDecimalSeparator = ".";
					if (check == "x")
                    {
						stored.x = (float)double.Parse("0.0", NumberStyles.Any, ci);
						check = "y";
                    }
					else if (check == "y")
					{
						stored.y = (float)double.Parse("0.0", NumberStyles.Any, ci);
						check = "z";
					}
					else if (check == "z")
					{
						stored.z = (float)double.Parse("0.0", NumberStyles.Any, ci);
						check = "x";
						Vector3[] scrapedTemp = new Vector3[scraped.Length + 1];
						for (int f = 0; f<scraped.Length; f++)
							scrapedTemp[f] = scraped[f];
						scrapedTemp[scrapedTemp.Length - 1] = stored;
						stored = new Vector3();
					}
				}
				scrape = "";
            }
            else
            {
				scrape += c;
            }
		}
		return scraped;
    }

	Vector2[] scrapeStringV2(string s)
	{
		string check = "x";
		string scrape = "";
		Vector2 stored = new Vector2();
		Vector2[] scraped = new Vector2[0];
		for (int i = 0; i < s.Length; i++)
		{
			char c = s.ToCharArray()[i];
			if (c == '(' || c == ',' || c == ')')
			{
				Debug.Log(scrape);
				if (scrape != "")
				{
					CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
					ci.NumberFormat.CurrencyDecimalSeparator = ".";
					if (check == "x")
					{
						stored.x = (float)double.Parse("0.0", NumberStyles.Any, ci);
						check = "y";
					}
					else if (check == "y")
					{
						stored.y = (float)double.Parse("0.0", NumberStyles.Any, ci);
						check = "x";
						Vector2[] scrapedTemp = new Vector2[scraped.Length + 1];
						for (int f = 0; f < scraped.Length; f++)
							scrapedTemp[f] = scraped[f];
						scrapedTemp[scrapedTemp.Length - 1] = stored;
						stored = new Vector2();
					}
				}
				scrape = "";
			}
			else
			{
				scrape += c;
			}
		}
		return scraped;
	}

	int[] scrapeInt(string s)
	{
		string scrape = "";
		int[] scraped = new int[0];
		for (int i = 0; i < s.Length; i++)
		{
			char c = s.ToCharArray()[i];
			if (c == '(' || c == ',' || c == ')')
			{
				Debug.Log(scrape);
				if (scrape != "")
				{
					CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
					ci.NumberFormat.CurrencyDecimalSeparator = ".";
					Debug.Log("SCRAPING " + scrape);
					int temp = int.Parse(scrape);
					int[] scrapedTemp = new int[scraped.Length + 1];
					for (int f = 0; f < scraped.Length; f++)
						scrapedTemp[f] = scraped[f];
					scrapedTemp[scrapedTemp.Length - 1] = temp;
				}
				scrape = "";
			}
			else
			scrape += c;
		}
		return scraped;
	}

	//This method will create and position a visual representation cube which will show the extrusion size while the user drags the mouse
	void CreateVisualExtrudeCube ()
	{
		extrudingCube = Instantiate (vertCubeObject) as GameObject;
		Vector3 cubeScale = vertPos[0];
		Vector3 cubeScale2 = vertPos[0];

		//here I'm trying to find the most distant vertice from vertPos[0] so I can locate the middle point between them
		float dist = Vector3.Distance (cubeScale, cubeScale2);
		foreach(Vector3 point in vertPos)
		{
			if(Vector3.Distance(cubeScale, point) > dist)
			{
				dist = Vector3.Distance(cubeScale, point);
				cubeScale2 = point;
			}
		}

		//set the position, rotation and size of the cube related to the 2 most distant vertices of the selected face
		Vector3 between = cubeScale2 - cubeScale;
		extrudingCube.transform.localScale = new Vector3 (cubeSize, cubeSize, cubeSize);
		extrudingCube.transform.rotation = Quaternion.LookRotation (hit.normal);
		extrudingCube.transform.position = cubeScale + (between / 2);
		//save the starting point and end point which will be used to resize the cube as the endPos is moved
		startPos = extrudingCube.transform.position;
		endPos = Instantiate (vertCubeObject, extrudingCube.transform.position, extrudingCube.transform.rotation) as GameObject;

		createVerticesOnce = true;
	}


	//This method will find the 4 vertices of the clicked mesh face
	void GetVertices ()
	{ 
		Ray ray = c.ScreenPointToRay (Input.mousePosition); 

		//if the mouse hits the MeshCollider of the cube
		if (Physics.Raycast (ray, out hit)) {
			if (!hit.transform.gameObject.GetComponent<CustomMeshObject>())
				return;
			//get the mesh, vertices and triangles
			MeshCollider meshCollider = hit.collider as MeshCollider;
			mesh = meshCollider.sharedMesh;
			vertices = mesh.vertices;
			triangles = mesh.triangles;
			
			hitTransform = hit.collider.transform;
			
			int[] vertsIndex = new int[4];
			int t = hit.triangleIndex * 3;

			//get all 3 vertices based on the triangle that was hit
			for (int i = 0; i < vertPos.Length-1; i++) {
				vertsIndex [i] = t + i;
				vertPos [i] = vertices [triangles [vertsIndex [i]]];
			}

			//here I round the float value of the vertices positions to avoid small differences like vertPos[0].x = 0.05015 and vertPos[1].x = 0.05014
			for (int i = 0; i < vertPos.Length; i++)
			{
				vertPos[i] = new Vector3(Mathf.Round(vertPos[i].x * 100f) / 100f, Mathf.Round(vertPos[i].y * 100f) / 100f, Mathf.Round(vertPos[i].z * 100f) / 100f);
			}

			//this method will find the fourth vertice and store it in vertPos[3]
			FindTheFourthVertice();

			//if there is no vertCubes, which are cubes that demonstrate the selected vertices, instantiate them
			//each vertCube will be positioned on the selected vertices positions
			if (vertCubes.Count == 0) {
				for (int i = 0; i < vertPos.Length; i++) {
					//using TransformPoint to change the position from local to world space so the cubes are correctly positioned
					vertPos [i] = hitTransform.TransformPoint (vertPos [i]);
					vertCubes.Add ((Instantiate (vertCubeObject, vertPos [i], Quaternion.LookRotation (hit.normal))) as GameObject);
				}
				//if cubes already have been instantiated just move them to the new vertices positions
			} else {
				for (int i = 0; i < vertPos.Length; i++) {
					//using TransformPoint to change the position from local to world space so the cubes are correctly positioned
					vertPos [i] = hitTransform.TransformPoint (vertPos [i]);
					vertCubes [i].transform.position = vertPos [i];
					vertCubes [i].transform.rotation = Quaternion.LookRotation (hit.normal);
				}
			}

			faceSelected = true;

		}
	}



	//This method will use the 3 vertices of the selected triangle to find the fourth vertice which results in the selected face of the mesh
	void FindTheFourthVertice()
	{
		//if the x position of vertice 1 and 0 is different
		if (vertPos [1].x != vertPos [0].x) {
			vertPos [3] = vertPos [2];
			vertPos [3].x = vertPos [0].x;
			//check if the fourth vertice is equal to any of the other 3 vertices
			if (vertPos [3] == vertPos [0] || vertPos [3] == vertPos [1] || vertPos [3] == vertPos [2]) {
				vertPos [3] = vertPos [1];
				vertPos [3].x = vertPos [0].x;
				//check one last time for some possible cases
				if (vertPos [3] == vertPos [0] || vertPos [3] == vertPos [1] || vertPos [3] == vertPos [2]) {
					vertPos [3] = vertPos [0];
					vertPos [3].x = vertPos [1].x;
				}
				//this method will fix the order of the vertices, which is necessary when the fourth vertice happens to be equal to any of the other 3 vertices
				CorrectVerticeOrder();	
			}

			//if the y position of vertice 1 and 0 is different
		} else if (vertPos [1].y != vertPos [0].y) {
			vertPos [3] = vertPos [2];
			vertPos [3].y = vertPos [0].y;
			//check if the fourth vertice is equal to any of the other 3 vertices
			if (vertPos [3] == vertPos [0] || vertPos [3] == vertPos [1] || vertPos [3] == vertPos [2]) {
				vertPos [3] = vertPos [0];
				vertPos [3].y = vertPos [1].y;
				//check one last time for some possible cases
				if (vertPos [3] == vertPos [0] || vertPos [3] == vertPos [1] || vertPos [3] == vertPos [2]) {
					vertPos [3] = vertPos [1];
					vertPos [3].y = vertPos [0].y;
				}
				//this method will fix the order of the vertices, which is necessary when the fourth vertice happens to be equal to any of the other 3 vertices
				CorrectVerticeOrder();
			}

			//if the z position of vertice 1 and 0 is different
		} else if (vertPos [1].z != vertPos [0].z) {
			vertPos [3] = vertPos [2];
			vertPos [3].z = vertPos [0].z;
			//check if the fourth vertice is equal to any of the other 3 vertices
			if (vertPos [3] == vertPos [0] || vertPos [3] == vertPos [1] || vertPos [3] == vertPos [2]) {
				vertPos [3] = vertPos [1];
				vertPos [3].z = vertPos [0].z;
				//check one last time for some possible cases
				if (vertPos [3] == vertPos [0] || vertPos [3] == vertPos [1] || vertPos [3] == vertPos [2]) {
					vertPos [3] = vertPos [0];
					vertPos [3].z = vertPos [1].z;
				}
				//this method will fix the order of the vertices, which is necessary when the fourth vertice happens to be equal to any of the other 3 vertices
				CorrectVerticeOrder();
			}
		}

	}



	//This method will ensure that the order of the vertices in vertPos will be correct
	void CorrectVerticeOrder()
	{
		Vector3 aux = vertPos[3];
		vertPos[3] = vertPos[0];
		vertPos[0] = aux;
	}


}




