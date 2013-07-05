using UnityEngine;
using System.Collections;
using SimpleJSON;

public class MenuScreenController : MonoBehaviour {
	
	private static MenuScreenController m_instance = null;
	
	public static MenuScreenController getInstance() {
		return m_instance;
	}
	
	// Use this for initialization
	void Start () {
		m_instance = this;	
		ListenOnSocketIO.getInstance();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		// Make a background box
		GUI.Box (new Rect ((Screen.width/2 - Screen.height/4 - 10), Screen.height/2 - (Screen.height/8 + 30), Screen.height/2 + 20, Screen.height/4 + 50), "Loader Menu");
	
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if (GUI.Button (new Rect (Screen.width/2 - Screen.height/4, Screen.height/2 - (Screen.height/8 + 5), Screen.height/2, Screen.height/8), "Play Game!")) {
			Application.LoadLevel (GlobalVariables.LobbyScreenNumber);
		}
	
		// Make the second button.
		if (GUI.Button (new Rect (Screen.width/2 - Screen.height/4, Screen.height/2 + 5, Screen.height/2, Screen.height/8), "About...")) {
			//Application.LoadLevel (2);
		}
	}
}
