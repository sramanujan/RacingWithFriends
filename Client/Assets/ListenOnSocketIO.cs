using UnityEngine;
using System;
using System.Collections;
using SocketIOClient;
using SimpleJSON;

public class ListenOnSocketIO {
	
	private static ListenOnSocketIO m_instance = null;
	
	public static ListenOnSocketIO getInstance() {
		if(m_instance == null) {
			m_instance = new ListenOnSocketIO();
		}
		return m_instance;
	}
	
	private Client socket = null;
	
	public ListenOnSocketIO() {
        string socketUrl = "http://192.168.46.1:8028";
        this.socket = new Client(socketUrl);
        this.socket.Opened += this.SocketOpened;
        this.socket.Message += this.SocketMessage;
        this.socket.SocketConnectionClosed += this.SocketConnectionClosed;
        this.socket.Error += this.SocketError;
        this.socket.Connect();
	}
	
	public void sendMessage(string eventName, object data) {
		this.socket.Send (new SocketIOClient.Messages.JSONMessage(new {name = "GameMessage", args = new {eventName = eventName, data = data}}));
	}
	
	public void handleIncoming(JSONNode data) {
		string[] names = GlobalVariables.CallbackList[data["eventName"]].Split(new string[] { "::" }, StringSplitOptions.None);
		System.Reflection.MethodInfo instanceMethodInfo = Type.GetType(names[0]).GetMethod("getInstance");
		System.Reflection.MethodInfo handlerMethodInfo = Type.GetType(names[0]).GetMethod(names[1]);
		handlerMethodInfo.Invoke(instanceMethodInfo.Invoke(null, null), new object[] { data["data"] });
	}
	
    private void SocketOpened (object sender, EventArgs e){
        Debug.Log("socket opened");
    }

    private void SocketMessage (object sender, MessageEventArgs e) {
		if(e != null) {
			switch(e.Message.Event) {
				case "message" :
					//e.Message.MessageText is a string
					break;
				case "open" :
					//MessageText not of much use.
					break;
				case "connect" :
					//MessageText not of much use.
					break;
				default :
					//This is an EMIT!!! This is where we get our JSON objects. e.Message.MessageText is a json string.
					JSONNode item = JSON.Parse(e.Message.MessageText);
					foreach (JSONNode node in item["args"].Childs)
					{
					    handleIncoming (node);
					}
					break;
			}
		}
    }

    private void SocketConnectionClosed (object sender, EventArgs e) {
        Debug.Log("socket closed");
    }

    private void SocketError (object sender, ErrorEventArgs e) {
        Debug.Log("socket error: " + e.Message);
    }
}