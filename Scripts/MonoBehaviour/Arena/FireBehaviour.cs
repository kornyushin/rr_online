using UnityEngine;
using System.Collections;

public class FireBehaviour : AbstractGO
{
	//public string type;
	public float speed;
	public float speedY = 0;
	public Transform boom;
	public bool isEnemy = false;
	public bool isSin = false;
	public bool isAutoAim = false;
	int sinTimer = 0;

	bool active;

	protected override void onStart ()
	{
		base.onStart ();
		name = name.Replace ("(Clone)", "");
	}

	protected override void onEnable ()
	{
		base.onEnable ();
		boom.gameObject.SetActive (false);
		active = true;
		isSin = false;
		isAutoAim = false;
		GetComponent<tk2dSprite> ().color = Color.white;
		speedY = 0;
		GetComponent<Renderer> ().enabled = true;
		collider2D.enabled = true;
	}

	protected override void onFixedUpdate ()
	{
		base.onFixedUpdate ();
		if (active) {
			x += speed;
			y += speedY;
			if (x < -5 || x > 5)
				hide ();
		}
	}

	//	protected override void onUpdate ()
	//	{
	//		base.onUpdate ();
	//		if (active) {
	//			x += speed * Time.timeScale;
	//			if (isSin) {
	//				y += .1f * Mathf.Sin (0.1f * sinTimer++);
	//			} else if (isAutoAim) {
	//				var hy = HeroBehaviour.Instance.transform.localPosition.y;
	//				var hx = HeroBehaviour.Instance.transform.localPosition.x;
	//				if (hy > y) {
	//					y += speedY*Time.timeScale;
	//				} else if (hy < y) {
	//					y -= speedY*Time.timeScale;
	//				}
	//				transform.localRotation = Quaternion.Euler (0, 0, Mathf.Clamp (Mathf.Atan2 (y - hy, x - hx) * Mathf.Rad2Deg, -45, 45));

	//			} else {
	//				y += speedY * Time.timeScale;
	//
	//			}

	//			if (x < -5 || x > 5)
	//				gameObject.SetActive (false);
	//			if (x > 5 && isEnemy) {
	//				if (state == "toDelete") {
	//					gameObject.SetActive (false);
	//				} else {
	//					state = "toDelete";
	//					x = -5;
	//				}
	//			}
	//		}
	//	}

	void hide ()
	{
		active = false;
		collider2D.enabled = false;

		gameObject.SetActive (false);
	}

	public void destroy ()
	{
		active = false;
		collider2D.enabled = false;
//		hide ();
		LeanTween.alpha (gameObject, 0, 1).setSprite (GetComponent<tk2dSprite> ())
			.setEase (LeanTweenType.easeInExpo)
			.setOnComplete (hide);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		
		if (other.name.IndexOf ("fire") > -1 && other.name != name) {
//			if (isEnemy) {
//				SoundManager.PlaySound (SoundManager.GameAudio.explosion);
//				showBoom ();
//			} else {
			hide ();
//			}
		} 


	}

	public void showBoom ()
	{
		if (active) {
			active = false;
			collider2D.enabled = false;
			z = -1;
			GetComponent<Renderer> ().enabled = false;
			boom.gameObject.SetActive (true);
			SpriteUtil.rotate (boom, Random.Range (0, 360));
			boom.GetComponent<tk2dSpriteAnimator> ().AnimationCompleted = onBoomComplete;
			boom.GetComponent<tk2dSpriteAnimator> ().Play ();
		}
	}

	void onBoomComplete (tk2dSpriteAnimator arg1, tk2dSpriteAnimationClip arg2)
	{
		boom.gameObject.SetActive (false);
		destroy ();
	}

	public bool isActive ()
	{
		return active;
	}

	public void setPosition (Vector3 position, float k)
	{
		position.z += .1f;
		//position.y -= .1f;

		//position.x += .2f * k;
		speed = .12f * k;
		transform.localScale = new Vector3 (1.5f * k, 1.5f, 1);
		transform.position = position;
	}
}
