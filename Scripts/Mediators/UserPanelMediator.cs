using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Core;
using System.Collections.Generic;

/// <summary>
/// Sample mediators.
/// </summary>
public class UserPanelMediator : BaseMediator
{


	public static string NAME = "UserPanelMediator";
	private IList<string> notificationList = new List<string> ();

	public UserPanelMediator ()
	{
		init ();
	}

	public UserPanelMediator (string mediatorName) : base (mediatorName)
	{
		init ();
	}

	public UserPanelMediator (string mediatorName, object viewComponent) : base (mediatorName, viewComponent)
	{		
		init ();
	}

	private void init ()
	{
		notificationList.Add (Notification.USER_LOADED);
		notificationList.Add (Notification.CHANGE_HIT);
	}

	public override void HandleNotification (INotification notification)
	{
		
		switch (notification.Name) {
		case Notification.USER_LOADED:			
			userPanel.lblUserName.text = user.name;
			userPanel.lblUserCoins.text = user.coins + "";
			break;
		case Notification.CHANGE_HIT:	
//			if (notification.Type == Notification.ENEMY)
				userPanel.lblHitCount.text = user.getEnemyHitCount () + "";
//			if (notification.Type == Notification.PLAYER)
				userPanel.lblEnemyHitCount.text = user.getPlayerHitCount () + "";
			break;
		default:
			break;
		}
	}

	public override  IList<string>ListNotificationInterests ()
	{
		return notificationList;
	}

	UserPanel userPanel {
		get { return (UserPanel)ViewComponent; }
	}
}
