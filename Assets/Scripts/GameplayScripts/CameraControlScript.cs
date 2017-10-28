//1
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControlScript : MonoBehaviour 
{
	public float zoom;
	public int zoomMin;
	public int zoomMax;
	public float zoomSpeed;
	public float worldRotSpeed;
	public float cameraRotSpeed;
	public float rotY = 0f;
	public float minCamXRot;
	public float maxCamXRot;
	public GameObject cameraPivot;
	public static CameraControlScript Instance;
	void Awake(){Instance = this;}

	void Update()
	{
		//Rotate camera and sun around world based on WASD controls
		float hRot = 0;
		float vRot = 0;
		hRot = -Input.GetAxisRaw("Horizontal")* worldRotSpeed * Time.unscaledDeltaTime;
		vRot =  Input.GetAxisRaw("Vertical") * worldRotSpeed * Time.unscaledDeltaTime;
		transform.Rotate(Vector3.up, hRot, Space.Self);
		transform.Rotate(Vector3.left, vRot, Space.Self);

		//Pivots camera based on mouse movement
		if(Input.GetMouseButton(1))
		{
			transform.Rotate(Vector3.forward, Input.GetAxisRaw("Mouse X") * cameraRotSpeed * Time.unscaledDeltaTime, Space.Self);
			rotY += Input.GetAxisRaw("Mouse Y") * cameraRotSpeed * Time.unscaledDeltaTime;
			rotY = Mathf.Clamp(rotY, minCamXRot, maxCamXRot);
			cameraPivot.transform.localEulerAngles = new Vector3(-rotY, Camera.main.transform.localEulerAngles.y, 0);
		}

		//Zoom camera
		zoom = -Camera.main.transform.localPosition.z;
		if(zoom > zoomMin && zoom < zoomMax)
		{
			zoom += -(Input.GetAxisRaw("Mouse ScrollWheel") + Input.GetAxisRaw("Zoom")) * zoomSpeed;
			if(zoom > zoomMin && zoom < zoomMax)
			{
				Camera.main.transform.localPosition = new Vector3(0, 0, -zoom);
			}
		}
	}

	//Changes camera rotation to the tile index
	public void MoveCamera(int tileIndex)
	{
		transform.LookAt(GridData.Instance.points[tileIndex]);
	}
}
