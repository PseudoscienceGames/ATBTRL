using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Caeludum;

public class CursorScript : MonoBehaviour 
{
	public static int gridLoc;
	public int testGridLoc;
	public GameObject cursorMesh;
	public GameObject tileInfo;
	public int rotSpeed;
	
	public static CursorScript Instance;
	void Awake(){Instance = this;}

	//Check for mouse being over GUI
	public bool mouseOverGUI = false;
	public void MouseOverGUI(){mouseOverGUI = true;}
	public void MouseNotOverGUI(){mouseOverGUI = false;}

	void Update()
	{	
		//Spin chain mesh
		cursorMesh.transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();

		if(!mouseOverGUI)
		{
			if(Physics.Raycast(ray, out hit))
			{
				gridLoc = Math.FindNearestTile(hit.point, gridLoc);
				testGridLoc = gridLoc;
				//float height = GridData.Instance.worldRadius;
				//if(LandData.FindTile(gridLoc).height > 0)
				//{
				//	height += LandData.FindTile(gridLoc).height * LandData.Instance.heightIncrement;
				//}
				//transform.position = GridData.Instance.points[gridLoc].normalized * height;
				//transform.LookAt(Vector3.zero);
			}
		}
	}
}
