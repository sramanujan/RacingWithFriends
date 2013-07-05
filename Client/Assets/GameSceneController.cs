using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameSceneController : MonoBehaviour {
	
	private GameObject tempCube;
	List<GameObject> listOfPlayers = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		tempCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		listOfPlayers.Add(tempCube)
		tempCube.transform.position = new Vector3(0, 0.5F, 0);
		tempCube.transform.localScale = new Vector3 (1.25F, 1.5F, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
