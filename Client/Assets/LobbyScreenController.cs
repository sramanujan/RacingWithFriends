using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LobbyScreenController : MonoBehaviour {
	
	private static LobbyScreenController m_instance = null;
	private string labelText;
	
	public static LobbyScreenController getInstance() {
		return m_instance;
	}
	
	private int numberOfMembersInLobby = 0;
	private float startTimeLeft = 5.0F;
	
	
	private bool numberUpdated = false;
	private bool readyToStart = false;
	
	// Use this for initialization
	void Start () {
		m_instance = this;
		ListenOnSocketIO.getInstance().sendMessage ("entered_lobby", new {name = "asdf"});
		labelText = "No one in lobby";
	}
	
	// Update is called once per frame
	void Update () {
		if(readyToStart) {
			if(startTimeLeft > 0) {
				startTimeLeft -= Time.deltaTime;
				labelText = "Game Starting in... " + ((int)startTimeLeft).ToString();
			} else {
				readyToStart = false;
				Application.LoadLevel (GlobalVariables.GameScreenNumber);
			}
		}
		if(numberUpdated) {
			labelText = "Number of members: " + numberOfMembersInLobby.ToString();
		}
	}
	
	public void OnJoiningLobby(JSONNode data) {
		numberOfMembersInLobby = data["numberOfMembers"].AsInt;
		numberUpdated = true;
	}
	
	public void OnMemberJoinedLobby(JSONNode data) {
		numberOfMembersInLobby++;
		numberUpdated = true;
	}
	
	public void OnGameStarting(JSONNode data) {
		readyToStart = true;
	}
	
	void OnGUI () {
		// Make a background box
		GUI.Box (new Rect ((Screen.width/2 - Screen.height/4 - 10), Screen.height/2 - (Screen.height/8 + 30), Screen.height/2 + 20, Screen.height/4 + 50), labelText);
	
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if (GUI.Button (new Rect (Screen.width/2 - Screen.height/4, Screen.height/2 - (Screen.height/8 + 5), Screen.height/2, Screen.height/8), "Start Game!")) {
			ListenOnSocketIO.getInstance().sendMessage ("start_game", new {name = "asdf"});
		}
	
		// Make the second button.
		if (GUI.Button (new Rect (Screen.width/2 - Screen.height/4, Screen.height/2 + 5, Screen.height/2, Screen.height/8), "Quit Game")) {
			Application.LoadLevel (GlobalVariables.MenuScreenNumber);
		}
	}
}