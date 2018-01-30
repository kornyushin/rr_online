using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MoveController : AbstractGO
{
	public MoveController enemy {
		get;
		set;
	}

	protected float top;
	protected float bottom;

	protected List<int> moveValue;
	protected PlayerFire fireControl;
	protected PlayerView viewControl;

	protected int cooldown = 10;
	protected float speedY = 0f;
	protected int[] positionChances;


	float HEIGHT;

	protected float nextHeight;

	protected override void onStart ()
	{
		base.onStart ();
		transform.parent = arena.transform;
		HEIGHT = GetComponent<tk2dSprite> ().GetBounds ().size.y * .5f;
		top = arena.sky.localPosition.y - arena.sky.GetComponent<BoxCollider2D> ().size.y * .5f - HEIGHT;
		bottom = arena.ground.localPosition.y + arena.ground.GetComponent<BoxCollider2D> ().size.y * .5f + HEIGHT;
		viewControl = GetComponent<PlayerView> ();
	}

	protected void parseMove (string numbers)
	{
		moveValue = new List<int> (Array.ConvertAll (numbers.Split (','), int.Parse));

	}

	protected override void onFixedUpdate ()
	{
		base.onFixedUpdate ();
//		Debug.Log (arena.state);
		if (arena.state == ArenaBehaviour.ACTIVE) {
			y = getNextY (y);
			//			Debug.Log (cooldown);
			if (--cooldown == 0) {
				push ();
				var v = moveValue [0];
				cooldown = 2;//Mathf.RoundToInt (v * .1f);
				moveValue.RemoveAt (0);
				moveValue.Add (v);
//				Debug.Log (cooldown);
				//				Debug.Log ("---------------");
			}
		}
	}

	protected virtual void push ()
	{
		
		if (y < nextHeight) {
			speedY = -.1f;
		}
	}

	protected virtual void setMovePoint ()
	{
		float kf = (moveValue [0] - 30f) / 70f;
		var zone = 0;
		for (int i = 0; i < positionChances.Length; i++) {
			if (kf < positionChances [i] * .01f) {
				zone = i;
				break;
			}
		}	
		var zoneheight = (top - bottom) / 3;
		var zonepos = zoneheight * kf;
		nextHeight = Mathf.Clamp (bottom + zoneheight * zone + zonepos, bottom + HEIGHT, top - HEIGHT);
//		if (GetComponent<PlayerBehaviour> () != null)
//			Debug.Log ("k " + kf + "zone " + zone + " point " + nextHeight);		 
	}

	float limit = .24f;

	float getNextY (float _y)
	{
		float ny = _y;
		ny = Mathf.Clamp (ny - speedY, bottom, top);
		speedY = Mathf.Clamp (speedY + .01f, -limit, limit);
		if (Mathf.Abs (y - nextHeight) < .1f) {
			setMovePoint ();
		}

		return ny;
	}

	void changeBorder (float delta)
	{
		bottom = Mathf.Clamp (bottom + delta, -100, -.1f);
		top = Mathf.Clamp (top - delta, .1f, 100);
		Debug.Log (bottom + " " + top);
	}
}
