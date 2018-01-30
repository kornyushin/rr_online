using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : AbstractGO
{
	protected MoveController moveController;
	protected PlayerView viewControl;
	protected int cooldownFire = 10;
	public Transform bulletSpawn;

	protected override void onStart ()
	{
		base.onStart ();
		moveController = GetComponent<MoveController> ();
		viewControl = GetComponent<PlayerView> ();
	}





	protected override void onFixedUpdate ()
	{
		base.onFixedUpdate ();
		if (arena.state == ArenaBehaviour.ACTIVE) {
			if (--cooldownFire == 0) {
				fire ();
			}
		}
	}

	protected virtual void fire ()
	{
		createFire ();
		cooldownFire = 60;
	}

	protected virtual void firePosition (FireBehaviour fireBehaviour)
	{
		
	}

	protected virtual FireBehaviour createFire ()
	{

		var f = PrefabsManager.Instance.getFire ();
		f.parent = transform.parent;
		f.name = "fire" + name;// photonView.viewID;//+ (FIRENUM++);
		var fireBehaviour = f.GetComponent<FireBehaviour> ();
		firePosition (fireBehaviour);
		//SoundManager.PlaySound (SoundManager.GameAudio.shoot);
		return fireBehaviour;
	}

	protected virtual void handleFire ()
	{
		
	}




	void OnTriggerEnter2D (Collider2D other)
	{
		if (Options.NO_HIT)
			return;
		
		if (other.name.IndexOf (name) == -1) {


			other.GetComponent<FireBehaviour> ().destroy ();

			handleFire ();
			viewControl.showBoom ();
		}

	}
}
