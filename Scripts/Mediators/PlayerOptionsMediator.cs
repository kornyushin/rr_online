using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Core;
using System.Collections.Generic;

/// <summary>
/// Sample mediators.
/// </summary>
public class PlayerOptionsMediator : BaseMediator
{


	public static string NAME = "PlayerOptionsMediator";
	private IList<string> notificationList = new List<string> ();

	public PlayerOptionsMediator ()
	{
		init ();
	}

	public PlayerOptionsMediator (string mediatorName) : base (mediatorName)
	{
		init ();
	}

	public PlayerOptionsMediator (string mediatorName, object viewComponent) : base (mediatorName, viewComponent)
	{		
		init ();
	}

	private void init ()
	{
		notificationList.Add (Notification.USER_LOADED);
		notificationList.Add (Notification.RESTART);
	}

	public override void HandleNotification (INotification notification)
	{
//		D.Log ("Receive " + notification.Name, notification.Type);
		switch (notification.Name) {
		case Notification.USER_LOADED:			
			showChances ();
			if (!Options.QUICK_START)
				userPanel.gameObject.SetActive (true);
			break;
		case Notification.RESTART:			
			userPanel.gameObject.SetActive (true);
			break;
		}
	}

	public override  IList<string>ListNotificationInterests ()
	{
		return notificationList;
	}

	PlayerOptions userPanel {
		get { return (PlayerOptions)ViewComponent; }
	}

	void OnClickPlayer ()
	{
		Options.WITH_BOT = false;
		userPanel.gameObject.SetActive (false);
		SendNotification (Notification.PHOTON_JOIN_ROOM, Options.ROOM_NUM);

	}

	void OnClickBot ()
	{
		Options.WITH_BOT = true;
		userPanel.gameObject.SetActive (false);
//		SendNotification (Notification.START_MATCH);
		SendNotification (Notification.PHOTON_JOIN_ROOM, Random.Range (0, 1000));
	}

	void OnChangePlayerSlider (int[] f)
	{
		user.getModel ().positionChanses = f;
	}

	void OnChangeEnemySlider (int[] f)
	{		
		user.getEnemyModel ().positionChanses = f;
	}

	void showChances ()
	{
		userPanel.playerSlider.updateText (user.getModel ().positionChanses);
		userPanel.enemySlider.updateText (user.getEnemyModel ().positionChanses);
	}

	void OnClickUpgrade (Upgrade u)
	{
		throw new System.NotImplementedException ();
	}

	void OnClickRestart ()
	{
		SendNotification (Notification.RESTART);
	}

	public override void OnRegister ()
	{
		userPanel.gameObject.SetActive (false);
		userPanel.onClickBot += OnClickBot;
		userPanel.onClickPlayer += OnClickPlayer;
		userPanel.onClickRestart += OnClickRestart;
		userPanel.playerSlider.changeSlider += OnChangePlayerSlider;
		userPanel.enemySlider.changeSlider += OnChangeEnemySlider;
		userPanel.upgradesView.onClickUpgrade += OnClickUpgrade;


//		userPanel.upgradesView.show (upgrade.getElements());
	}

	public override void OnRemove ()
	{
		userPanel.onClickBot -= OnClickBot;
		userPanel.onClickPlayer -= OnClickPlayer;
		userPanel.onClickRestart -= OnClickRestart;
		userPanel.playerSlider.changeSlider -= OnChangePlayerSlider;
		userPanel.enemySlider.changeSlider -= OnChangeEnemySlider;
		userPanel.upgradesView.onClickUpgrade -= OnClickUpgrade;

	}
}
