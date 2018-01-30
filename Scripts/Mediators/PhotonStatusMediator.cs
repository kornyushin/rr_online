using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Core;
using System.Collections.Generic;

/// <summary>
/// Sample mediators.
/// </summary>
public class PhotonStatusMediator :  Mediator, IFWMediator
{


	public static string NAME = "PhotonStatusMediator";
	private IList<string> notificationList = new List<string> ();

	public PhotonStatusMediator ()
	{
		init ();
	}

	public PhotonStatusMediator (string mediatorName) : base (mediatorName)
	{
		init ();
	}

	public PhotonStatusMediator (string mediatorName, object viewComponent) : base (mediatorName, viewComponent)
	{		
		init ();
	}

	private void init ()
	{
		notificationList.Add (Notification.PHOTON_STATUS_CHANGED);
	}

	public override void HandleNotification (INotification notification)
	{
//		D.Log ("Receive " + notification.Name, notification.Body);
		switch (notification.Name) {
		case Notification.PHOTON_STATUS_CHANGED:
			lblStatus.text = (string)notification.Body;
			break;
		default:
			break;
		}
	}

	public override  IList<string>ListNotificationInterests ()
	{
		return notificationList;
	}

	tk2dTextMesh lblStatus {
		get { return (tk2dTextMesh)ViewComponent; }
	}
}
