using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVariables : MonoBehaviour {
	
	public static int MenuScreenNumber = 0;
	public static int LobbyScreenNumber = 1;
	public static int GameScreenNumber = 2;
	public static int xyz = 35;
	public static readonly Dictionary<string, string> CallbackList = new Dictionary<string, string>
	{
	    { "member_joined_lobby", "LobbyScreenController::OnMemberJoinedLobby" },
		{ "joining_lobby", "LobbyScreenController::OnJoiningLobby" },
		{ "game_starting", "LobbyScreenController::OnGameStarting" }
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
/*
public class MethodMapper {
	public MethodMapper(class classType, string methodName) {
		
	}
}
*/
