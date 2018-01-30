using UnityEngine;

public class Options
{
	public const bool QUICK_START = false;
	public static int ROOM_NUM = 2;
	public static int PLAYERS = 2;
	public static bool NO_HIT = false;
	public static bool WITH_BOT = true;

	public static bool DUMMY_USER = true;

	#region Test User

	public static UserModel Dummy {
		get {
			var u = new UserModel ();
			u.name = "Dummy" + Random.Range (1, 100);
			u.coins = 100;
			u.positionChanses = new int[]{ 20, 70, 100 };
			return u;
		}
	}

	public static UserModel EnemyDummy {
		get {
			var u = new UserModel ();
			u.name = "Enemy" + Random.Range (1, 100);
			u.coins = 100;
			u.positionChanses = new int[]{ 50, 60, 100 };
			return u;
		}
	}

	#endregion

}
