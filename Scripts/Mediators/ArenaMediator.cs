using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Core;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.IO;

/// <summary>
/// Sample mediators.
/// </summary>
public class ArenaMediator :  BaseMediator
{


	public static string NAME = "ArenaMediator";
	private IList<string> notificationList = new List<string> ();

	public ArenaMediator ()
	{
		init ();
	}

	public ArenaMediator (string mediatorName) : base (mediatorName)
	{
		init ();
	}

	public ArenaMediator (string mediatorName, object viewComponent) : base (mediatorName, viewComponent)
	{		
		init ();
	}

	private void init ()
	{
		notificationList.Add (Notification.START_MATCH);
		notificationList.Add (Notification.RESTART);
	}

	public override void HandleNotification (INotification notification)
	{
		
		switch (notification.Name) {
		case Notification.START_MATCH:
			user.resetGame ();
			user.setChanses (en_var, pl_var);
			arena.gameObject.SetActive (true);
			arena.createPlayer (user.getModel (), user.getEnemyModel ());
			SendNotification (Notification.PHOTON_STATUS_CHANGED, "battle " + (pl_var + en_var * 7 + 1) + " of 49");
			break;
		case Notification.RESTART:
			arena.removeAll ();
			//arena.gameObject.SetActive (false);
			break;
		default:
			break;
		}
	}

	public override  IList<string>ListNotificationInterests ()
	{
		return notificationList;
	}

	ArenaBehaviour arena {
		get { return (ArenaBehaviour)ViewComponent; }
	}

	int pl_var = 0;
	int en_var = 0;

	void log ()
	{
		var phit = user.getEnemyModel ().hitCount;
		var ehit = user.getModel ().hitCount;
		float i = (phit * 1.0f / ehit);
		var str = user.getModel ().chanses () + "      " + user.getEnemyModel ().chanses () + "    " + phit + ":" + ehit + " " + i;
		Debug.Log (str);
		sr.WriteLine (str);
	}

	void checkNext ()
	{
		const int to = 100;
		if (user.getModel ().hitCount == to || user.getEnemyModel ().hitCount == to) {
			log ();
			arena.removeAll ();
			pl_var++;
			if (pl_var == 7) {
				en_var++;
				pl_var = 0;
			}
			if (en_var == 7) {
				Debug.Log ("end");
				sr.Close ();
			} else {
				SendNotification (Notification.START_MATCH);
			}


		}
	}

	void OnHitEnemy ()
	{
		user.hitEnemy ();
		checkNext ();
	}

	void OnHitPlayer ()
	{
		user.playerHit ();
		checkNext ();
	}

	StreamWriter sr;

	public override void OnRegister ()
	{
		sr = File.CreateText ("cocklog.txt");
		sr.AutoFlush = true;
		sr.WriteLine ("Player zone  Enemy zone  player/enemy");

		Debug.Log (sr.AutoFlush);

		arena.onHitEnemy += OnHitEnemy;
		arena.onHitPlayer += OnHitPlayer;
	}

	public override void OnRemove ()
	{
		arena.onHitEnemy -= OnHitEnemy;
		arena.onHitPlayer -= OnHitPlayer;
	}


}
