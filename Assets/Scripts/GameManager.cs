using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	//public static int numOfFaces;

	public static GameManager Instance;
	void Awake() { Instance = this; }

	void Start()
	{
		GenMap();
		StartCoroutine(SetCamera());
	}

	IEnumerator SetCamera()
	{
		float transitionTime = 3;
		float progress = 0;
		while (progress < 1)
		{
			float delta = Time.deltaTime / transitionTime;
			progress += delta;
			Camera.main.transform.Rotate(new Vector3(60 * delta, 0, 0));
			yield return null;
		}
	}
	IEnumerator ResetCamera()
	{
		float transitionTime = 3;
		float progress = 0;
		while (progress < 1)
		{
			float delta = Time.deltaTime / transitionTime;
			progress += delta;
			Camera.main.transform.Rotate(new Vector3(-90 * delta, 0, 0));
			yield return null;
		}
	}

	void GenMap()
	{
		GridData.Instance.GenGrid();
		//LandData.Instance.GenTiles();
	}
}
