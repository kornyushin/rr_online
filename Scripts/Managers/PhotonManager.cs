using System;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class PhotonManager : Photon.MonoBehaviour
{
	
	bool waitPlayer;
	bool connected;
	public bool AutoConnect = true;
	int waitingTimer = 60;
	public byte Version = 1;
	[HideInInspector]
	public bool ConnectInUpdate = false;

	int room;

	public delegate void PhotonAction ();

	public delegate void PhotonActionInt (int num);

	public event PhotonAction onConnectedToMaster;
	public event PhotonActionInt onJoinedRoom;
	public event PhotonAction onLeaveRoom;
	public event PhotonAction onFullRoom;

	public virtual void Start ()
	{
		PhotonNetwork.autoJoinLobby = false;    
	}

	public virtual void Update ()
	{
		if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected) {
			ConnectInUpdate = false;
			PhotonNetwork.ConnectUsingSettings (Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
		}

		if (waitPlayer) {
			//Debug.Log ("wait players in the room " + PhotonNetwork.playerList.Length);
			if (PhotonNetwork.playerList.Length >= Options.PLAYERS || Options.WITH_BOT) {	
				
				for (int i = 0; i < PhotonNetwork.playerList.Length; i++) {
					if (PhotonNetwork.playerList [i].name != PhotonNetwork.playerName) {
						
					} 
				}

				waitPlayer = false;
				onFullRoom ();
			}

			if (--waitingTimer == 0) {
				
				waitPlayer = false;
				//start bot match
			}
		}
	}


	public virtual void OnConnectedToMaster ()
	{
		Debug.Log ("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
		connected = true;
		onConnectedToMaster ();
	}


	public virtual void OnPhotonRandomJoinFailed ()
	{
		Debug.Log ("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. ");

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.CustomRoomPropertiesForLobby = new string[]{ "map" };
		roomOptions.CustomRoomProperties = new Hashtable () { { "map", room } };
		roomOptions.MaxPlayers = 2;
		PhotonNetwork.CreateRoom (null, roomOptions, null);
	}

	// the following methods are implemented to give you some context. re-implement them as needed.

	public virtual void OnFailedToConnectToPhoton (DisconnectCause cause)
	{
		Debug.LogError ("Cause: " + cause);
	}

	public void OnJoinedRoom ()
	{        
		Debug.Log ("Wait, players in the room  " + PhotonNetwork.playerList.Length);
		onJoinedRoom (PhotonNetwork.playerList.Length);
		waitPlayer = true;
		waitingTimer = 6000;
	}

	public void joinRoom (int _room)
	{
		if (connected) {
			room = _room;
			Debug.Log ("join map  " + room);
			Hashtable expectedCustomRoomProperties = new Hashtable () { { "map", room } };
			byte expectedMaxPlayers = 2;
			PhotonNetwork.JoinRandomRoom (expectedCustomRoomProperties, expectedMaxPlayers);
		}
	}

	public void leaveRoom ()
	{
		if (connected) {
			PhotonNetwork.LeaveRoom ();

		}
	}

	void OnLeftRoom ()
	{
		Debug.Log ("disconnect room  ");
		onLeaveRoom ();
	}

}
