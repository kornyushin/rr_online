using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonConnectCommand :  BaseSimpleCommand
{
	PhotonManager photon;

	

	public override void Execute (PureMVC.Interfaces.INotification notification)
	{		
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "connect to server...");
		photon = Facade.photonManager;
		photon.ConnectInUpdate = true;
		photon.onConnectedToMaster += onConnected;
	}





	void onConnected ()
	{
		photon.onConnectedToMaster -= onConnected;
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "connected");
		SendNotification (Notification.LOAD_USER);
	}
}


