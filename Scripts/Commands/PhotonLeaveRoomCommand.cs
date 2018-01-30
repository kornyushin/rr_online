using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonLeaveRoomCommand :  BaseSimpleCommand
{
	PhotonManager photon;

	

	public override void Execute (PureMVC.Interfaces.INotification notification)
	{		
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "leave room...");
		photon = Facade.photonManager;
		photon.onLeaveRoom += onLeave;
		photon.leaveRoom ();
		photon.ConnectInUpdate = false;
		//
	}





	void onLeave ()
	{
		photon.onLeaveRoom -= onLeave;
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "");
		//SendNotification (Notification.LOAD_USER);
	}
}


