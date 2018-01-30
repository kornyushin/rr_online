using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MoveController
{

	protected override void onStart ()
	{
		base.onStart ();
		transform.position = arena.spawnPlayer2.position;
		setScaleX (-1);
		gameObject.tag = "right";
		viewControl.setSkin ("skin000" + (Random.Range (0, 5) * 2 + 1));
		string moveValue = "";
		for (int i = 0; i < 100; i++) {
			moveValue += (Random.Range (30, 100)) + ",";
		}
		parseMove (moveValue + PlayerParameters.RANDOM);
//		positionChances = new int[]  { 30, 70, 100 };
	}

	public void init (int[] _positionChanses)
	{
		positionChances = _positionChanses;
	}
}
