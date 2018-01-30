using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MoveController
{
	
	
	protected override void onAwake ()
	{
		base.onAwake ();
	}

	protected override void onStart ()
	{
		base.onStart ();	


		if (photonView.isMine) {			
			transform.position = arena.spawnPlayer1.position;
			gameObject.tag = "left";
		} else {           
			transform.position = arena.spawnPlayer2.position;
			setScaleX (-1);
			gameObject.tag = "right";
		}
		gameObject.name = gameObject.name + photonView.viewID;

		var obj = photonView.instantiationData;
		parseMove ((string)obj [0]);
		viewControl.setSkin ((string)obj [1]);
		positionChances = (int[])obj [2];
	}



	protected override void onUpdate ()
	{
		
	}

	protected override void push ()
	{
		base.push ();
//		Debug.Log (nextHeight + " " + (y < nextHeight));
	}

	void setStartMatchTimer ()
	{
//		Debug.Log (PhotonNetwork.isMasterClient);
		if (PhotonNetwork.isMasterClient) {
			float t = (float)PhotonNetwork.time;
			t += 1;
//			sendTimer (t);
			photonView.RPC ("sendTimer", PhotonTargets.All, t);
		}
	}

	[PunRPC]
	void sendTimer (float _t)
	{
		Debug.Log ("got from client " + _t + ", my time " + PhotonNetwork.time);
		arena.setStartMatchTime (_t);
	}
}
