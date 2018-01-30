using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : FireController
{
	
	protected override void firePosition (FireBehaviour fireBehaviour)
	{
		fireBehaviour.setPosition (bulletSpawn.position, photonView.isMine ? 1 : -1);
	}

	protected override void handleFire ()
	{
		if (photonView.isMine)
			SendMessageUpwards ("hitPlayer");
		else
			SendMessageUpwards ("hitEnemy");
	}




	void OnTriggerEnter2D (Collider2D other)
	{
		if (Options.NO_HIT)
			return;
		//Debug.Log (other.name + " " + photonView.viewID + " " + Time.realtimeSinceStartup);
		if (other.name.IndexOf (photonView.viewID.ToString ()) == -1) {


			other.GetComponent<FireBehaviour> ().destroy ();

			handleFire ();
			viewControl.showBoom ();
		}

	}


}