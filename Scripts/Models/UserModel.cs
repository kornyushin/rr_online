using System.Collections.Generic;

public class UserModel
{
	
	public string name;
	public int coins = 100;
	public int expirience = 0;
	public int wins = 0;
	public int games = 0;
	public int skin = 1;
	public int hitCount;
	//вероятность нахождения в частях экрана
	public int[] positionChanses;
	//параметры петуха
	public int attack = 1;
	public int defence = 1;
	public int speed = 1;

	public UserModel ()
	{
		
	}

	public Dictionary<string, string> getData ()
	{
		return new Dictionary<string, string> () {
			{ "name", name },
			{ "coins", coins.ToString () },
			{ "expirience", expirience.ToString () },
			{ "wins", wins.ToString () },
			{ "skin", skin.ToString () },
			{ "games", games.ToString () }
		};
	}

	public Dictionary<string, string> getCoinsData ()
	{
		return new Dictionary<string, string> () {			
			{ "coins", coins.ToString () }
		};
	}

	public string getDebugString ()
	{
		var d = getData ();
		string s = "";
		foreach (var item in d) {
			s += item.Key + ":" + item.Value + "\n";
		}
		return s;
	}

	public string chanses ()
	{
		
		var prev = 0;
		var s = "";
		for (int i = 0; i < positionChanses.Length; i++) {
			var i2 = (positionChanses [i] - prev).ToString ();//.PadLeft (3, " " [0]);
			s += ((i == 0) ? "" : ",") + i2;//+ "," + s;
			prev = positionChanses [i];
		}
		return s;

	}
}

