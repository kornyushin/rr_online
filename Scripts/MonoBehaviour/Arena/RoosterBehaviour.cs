using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RoosterBehaviour : AbstractGO
{
	//static PlayerBehaviour instance;
	public const string JUMP = "jump";
	public const string WAIT = "wait";
	public const string ACTIVE = "active";
	public const string LOSE = "lose";

	public Transform bulletSpawn;
	//public Transform parent;

	int skin = 1;
	//	bool isLeft;
	public int lives = 3;
	//	int jumpcooldown = 1;
		


	void OnChangeHealth (int health)
	{
		//SpriteUtil.updateText (transform.FindChild("count"), health + "");
	}

	protected override void onStart ()
	{
		
		base.onStart ();
//		initPlayer ();
	}

//	protected override void initImage ()
//	{
//		sprite.SetSprite ("skin" + skin.ToString ().PadLeft (4, "0" [0]));
//	}
//
//	public void initPlayer ()
//	{
//		
//		//skin = (int)photonView.instantiationData [0];
//		state = ACTIVE;
//		rigidBody2D.isKinematic = false;
//		collider2D.isTrigger = false;
//		cooldownFire = 50;
//		onGround = true;
//		sprite.color = new Color (1, 1, 1, 1);
//		anim.Pause ();
//
//		initImage ();
//	}

//	public void changeSkin ()
//	{
//		initImage ();
//	}
//
//	public void stop ()
//	{
//		gameObject.SetActive (true);
//		state = WAIT;
//		anim.Pause ();
//		initImage ();
//		active = true;
//	}



	[PunRPC]
	void createEnemyFire (Vector3 _position)
	{
		_position.x *= -1;
//		var f = createFire (_position, true, "enemyFire");
	}

	protected  void fire ()
	{
//		if (photonView.isMine) {
//			if (state == ACTIVE) {
//				createFire (bulletSpawn.position, transform.localScale.x < 0, "myFire");
//				photonView.RPC ("createEnemyFire", PhotonTargets.Others, bulletSpawn.position);
//			}
//			cooldownFire = 60;
//		}
	}

//	FireBehaviour createFire (Vector3 _position, bool _isRight, string _fname)
//	{
//		sprite.SetSprite ("skin" + (skin + 1).ToString ().PadLeft (4, "0" [0]));
//		fireAnimLength = 30;
//		var f = PrefabsManager.Instance.getFire ();
//		f.parent = transform.parent;
//		var fireBehaviour = f.GetComponent<FireBehaviour> ();
//		f.name = _fname;
//		float k;
//		if (_fname == "enemyFire") {
////			Debug.Log (world.isRightPlayer + " " + _isRight);
//		}
//		if (_isRight) {
//			k = -1;
//			if (_fname == "enemyFire" /*&& world.isRightPlayer*/) {
//				k = 1;
//			}
//		} else {
//			k = 1;
//		}
//		fireBehaviour.setPosition (_position, k);
//		//SoundManager.PlaySound (SoundManager.GameAudio.shoot);
//		return fireBehaviour;
//	}
//
//	protected override void tick ()
//	{
//		base.tick ();
//
//
//	}


//	protected virtual void jump ()
//	{
//		rigidBody2D.velocity = Vector2.zero;
//		rigidBody2D.AddForce (new Vector2 (0, 500));
//		cooldown = 10;
//		onGround = false;
//		showUnder ();
//	}

	/*void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.collider.name == "ground" && state != JUMP) {
			onGround = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		
		var fire = other.gameObject.GetComponent<FireBehaviour> ();
		if (fire != null && fire.isActive ()) {
			if (photonView.isMine) {
				if (fire.name == "enemyFire") {
					fire.destroy ();
			
//					lives--;
					photonView.RPC ("hitEnemy", PhotonTargets.Others, lives);
					GUIManager.Instance.topPanel.GetComponent<TopPanel> ().setMyScore (lives);
					if (lives == 0) {
						gameModel.isWin = false;
						WorldBehaviour.Instance.finishMatch ();	
					}
				}
			} else if (fire.name == "myFire") {
				fire.destroy ();
			}
		}

	}*/



	/*[PunRPC]
	void hitEnemy (int _l)
	{
		GUIManager.Instance.topPanel.GetComponent<TopPanel> ().setEnemyScore (_l);
		if (_l == 0) {
			gameModel.isWin = true;
			WorldBehaviour.Instance.finishMatch ();
		}
	}*/
	/*protected override void hit ()
	{
		base.hit ();
		if (active && !world.bot) {
			state = LOSE;
			active = false;
			anim.Resume ();
			anim.Play ("angel");
			rigidBody2D.isKinematic = true;
			collider2D.isTrigger = true;
			boom.gameObject.SetActive (true);
			boom.GetComponent<tk2dSpriteAnimator> ().AnimationCompleted = onBoomComplete;
			boom.GetComponent<tk2dSpriteAnimator> ().Play ();
			WorldBehaviour.Instance.destroyFire ();
			SoundManager.PlaySound (SoundManager.GameAudio.die);
		}
	}*/

//	void onBoomComplete (tk2dSpriteAnimator arg1, tk2dSpriteAnimationClip arg2)
//	{
//		boom.gameObject.SetActive (false);
//		LeanTween.alpha (gameObject, 0, 1f).setSprite (sprite);
////		LeanTween.moveLocalY (gameObject, transform.localPosition.y + 3f, 1f)
////			.setOnComplete (world.finishMatch);
//	}


}
