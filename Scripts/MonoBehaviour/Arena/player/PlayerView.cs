using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : AbstractGO
{
	public Transform boom;
	public Transform under;



	protected int fireAnimLength;


	protected tk2dSprite sprite;
	protected tk2dSpriteAnimator anim;

	protected override void onAwake ()
	{
		base.onAwake ();
		anim = transform.GetComponent<tk2dSpriteAnimator> ();
		sprite = transform.GetComponent<tk2dSprite> ();
	}

	public void setSkin (string str)
	{
		sprite.SetSprite (str);
	}

	public void showBoom ()
	{
		boom.gameObject.SetActive (true);
		boom.GetComponent<tk2dSpriteAnimator> ().AnimationCompleted = onBoomComplete;
		boom.GetComponent<tk2dSpriteAnimator> ().Play ();
	}

	void onBoomComplete (tk2dSpriteAnimator arg1, tk2dSpriteAnimationClip arg2)
	{
		boom.gameObject.SetActive (false);
	}

	/*protected virtual void initImage ()
	{
		//		Debug.Log ("cock" + type.ToString ().PadLeft (4, "0" [0]));
		sprite.SetSprite ("cock" + type.ToString ().PadLeft (4, "0" [0]));
	}

	protected virtual void fire ()
	{
		sprite.SetSprite ("cock" + (type + 1).ToString ().PadLeft (4, "0" [0]));
		fireAnimLength = 30;
	}*/




	protected void showUnder ()
	{
		under.gameObject.SetActive (true);
		under.GetComponent<tk2dSpriteAnimator> ().AnimationCompleted = onUnderComplete;
		under.GetComponent<tk2dSpriteAnimator> ().Play ();
		//		if(HeroBehaviour.Instance.Active) SoundManager.PlaySound(SoundManager.GameAudio.tap);
	}

	void onUnderComplete (tk2dSpriteAnimator arg1, tk2dSpriteAnimationClip arg2)
	{
		under.gameObject.SetActive (false);
	}
}
