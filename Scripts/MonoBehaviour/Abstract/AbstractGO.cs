using UnityEngine;
using System.Collections;

public class AbstractGO : AbstractNetworkBehaviour
{
	
	//	public const string JUMP = "jump";
	//	public const string WAIT = "wait";
	//	public const string ACTIVE = "active";
	//	public const string LOSE = "lose";
	protected ArenaBehaviour arena;

	protected override void onStart ()
	{
		base.onStart ();
		arena = GameObject.FindGameObjectWithTag ("Arena").GetComponent<ArenaBehaviour> ();
	}

	//	void OnTriggerEnter2D (Collider2D other)
	//	{
	//		var fire = other.gameObject.GetComponent<FireBehaviour> ();
	//		if (fire != null && fire.isActive ()) {
	//			if (fire.isEnemy && !isEnemy) {
	//				hit ();
	//				other.gameObject.SetActive (false);
	//			} else if (!fire.isEnemy && isEnemy) {
	//				hitEnemy ();
	//				other.gameObject.SetActive (false);
	//			}
	//
	//		}
	//	}




}
