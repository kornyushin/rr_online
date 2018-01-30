using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Core;
using System.Collections.Generic;

/// <summary>
/// Sample mediators.
/// </summary>
public class RoomSelectMediator :  Mediator, IFWMediator
{


	public static string NAME = "RoomSelectMediator";
	private IList<string> notificationList = new List<string> ();

	public RoomSelectMediator ()
	{
		init ();
	}

	public RoomSelectMediator (string mediatorName) : base (mediatorName)
	{
		init ();
	}

	public RoomSelectMediator (string mediatorName, object viewComponent) : base (mediatorName, viewComponent)
	{		
		init ();
	}

	private void init ()
	{
		notificationList.Add (Notification.CLICK_START);
		notificationList.Add (Notification.PHOTON_JOIN_ROOM);
	}

	public override void HandleNotification (INotification notification)
	{
//		D.Log ("Receive " + notification.Name, notification.Body);
		switch (notification.Name) {
		case Notification.CLICK_START:
			view.gameObject.SetActive (true);
			break;
		case Notification.PHOTON_JOIN_ROOM:
			view.gameObject.SetActive (false);
			break;
		default:
			break;
		}
	}

	public override  IList<string>ListNotificationInterests ()
	{
		return notificationList;
	}

	SelectRoomView view {
		get { return (SelectRoomView)ViewComponent; }
	}

	public override void OnRegister ()
	{
		Debug.Log (MediatorName + " onRegisterd.");
		view.btnPlay.OnClick += clickStart;
	}

	public override void OnRemove ()
	{
		view.btnPlay.OnClick -= clickStart;
	}

	void clickStart ()
	{		
		SendNotification (Notification.PHOTON_JOIN_ROOM, view.RoomNum);
	}
}
