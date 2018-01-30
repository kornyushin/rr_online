using UnityEngine;
using System.Collections;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class UserProxy : Proxy,IFWProxy
{

	public static string NAME = "UserProxy";
	private UserModel __model = new UserModel ();
	private UserModel __enemyModel = new UserModel ();

	public bool isLeftPlayer;

	public UserProxy ()
	{
	}

	public UserProxy (string proxyName) : base (proxyName)
	{

	}

	public UserProxy (string proxyName, object data) : base (proxyName, data)
	{

	}



	public void parse (Dictionary<string, UserDataRecord> data)
	{
		foreach (var item in data) {
//			Debug.Log ("    " + item.Key + " == " + item.Value.Value);
			if (item.Key == "name") {
				__model.name = item.Value.Value;
			} else if (item.Key == "coins") {
				__model.coins = int.Parse (item.Value.Value);
			} else if (item.Key == "expirience") {
				__model.expirience = int.Parse (item.Value.Value);
			} else if (item.Key == "wins") {
				__model.wins = int.Parse (item.Value.Value);
			} else if (item.Key == "games") {
				__model.games = int.Parse (item.Value.Value);
			} else if (item.Key == "skin") {
				__model.skin = int.Parse (item.Value.Value);
			}
		}
		//		D.Log (__model.getDebugString ());
	}

	public string name {
		get { return __model.name; }
	}

	public int coins {
		get { return __model.coins; }
	}

	public void loadDummy ()
	{
		__model = Options.Dummy;
		__enemyModel = Options.EnemyDummy;
	}

	public void hitEnemy ()
	{
		__enemyModel.hitCount++;
		SendNotification (Notification.CHANGE_HIT, null, Notification.ENEMY);
	}

	public void playerHit ()
	{
		__model.hitCount++;
		SendNotification (Notification.CHANGE_HIT, null, Notification.PLAYER);
	}

	public int getEnemyHitCount ()
	{
		return __enemyModel.hitCount;
	}

	public int getPlayerHitCount ()
	{
		return __model.hitCount;
	}

	public UserModel getModel ()
	{
		return __model;
	}

	public UserModel getEnemyModel ()
	{
		return __enemyModel;
	}

	public void resetGame ()
	{
		__enemyModel.hitCount = 0;
		__model.hitCount = 0;
	}

	int[][] chanses = new int[][] {
		new int[]{ 33, 66, 100 },
		new int[]{ 60, 80, 100 },
		new int[]{ 20, 80, 100 },
		new int[]{ 20, 40, 100 },
		new int[]{ 0, 0, 100 },
		new int[] {
			100,
			100,
			100
		},
		new int[] {
			0,
			100,
			100
		}
	};

	public void setChanses (int en_var, int pl_var)
	{
		__model.positionChanses = chanses [pl_var];
		__enemyModel.positionChanses = chanses [en_var];
	}
}

