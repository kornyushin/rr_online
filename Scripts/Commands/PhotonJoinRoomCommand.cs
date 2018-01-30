using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PureMVC.Interfaces;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

public class PhotonJoinRoomCommand : BaseSimpleCommand
{
	int room;
	PhotonManager photon;

	public override void Execute (PureMVC.Interfaces.INotification notification)
	{
		room = (int)notification.Body;
		photon = Facade.photonManager;
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "connect to match...");
		photon.onJoinedRoom += OnJoinedRoom;
		photon.onFullRoom += startMatch;
		photon.joinRoom (room);
	}

	public void OnJoinedRoom (int playersCount)
	{        
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "search player...");
		photon.onJoinedRoom -= OnJoinedRoom;
		user.isLeftPlayer = playersCount == 1;
	}


	void startMatch ()
	{
		photon.onFullRoom -= startMatch;
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "");
		SendNotification (Notification.START_MATCH);
	}


}
