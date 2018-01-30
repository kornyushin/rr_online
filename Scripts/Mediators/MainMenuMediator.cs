using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Core;
using System.Collections.Generic;

/// <summary>
/// Sample mediators.
/// </summary>
public class MainMenuMediator :  Mediator, IFWMediator
{


	public static string NAME = "MainMenuMediator";
	private IList<string> notificationList = new List<string> ();

	public MainMenuMediator ()
	{
		init ();
	}

	public MainMenuMediator (string mediatorName) : base (mediatorName)
	{
		init ();
	}

	public MainMenuMediator (string mediatorName, object viewComponent) : base (mediatorName, viewComponent)
	{		
		init ();
	}

	private void init ()
	{
		notificationList.Add (Notification.USER_LOADED);
		notificationList.Add (Notification.CLICK_START);
	}

	public override void HandleNotification (INotification notification)
	{
//		D.Log ("Receive " + notification.Name, notification.Body);
		switch (notification.Name) {
		case Notification.USER_LOADED:
			
			break;
		case Notification.CLICK_START:
			mainMenu.gameObject.SetActive (false);
			break;
		default:
			break;
		}
	}

	public override  IList<string>ListNotificationInterests ()
	{
		return notificationList;
	}

	MainMenu mainMenu {
		get { return (MainMenu)ViewComponent; }
	}

	public override void OnRegister ()
	{
//		Debug.Log (MediatorName+ " onRegisterd.");
		mainMenu.btnStart.OnClick += clickStart;
	}

	public override void OnRemove ()
	{
		mainMenu.btnStart.OnClick -= clickStart;
	}

	void clickStart ()
	{
		SendNotification (Notification.CLICK_START);
	}
}
