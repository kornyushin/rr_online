using UnityEngine;
using System.Collections;

public class SelectRoomView : AbstractView
{
	public Transform button;
	public Transform dot;
	public tk2dUIItem btnPlay;
	int pos;
	Transform[] buttons;
	//Transform[] dots;
	float minXPos = 0;
	//min x position of the camera
	float maxXPos;
	//max x position of the camera
	float xDist;
	//distance between camMinXPos and camMaxXPos
	//	float xDistFactor ; // = 1/camXDist

	float swipeSmoothFactor = .1f;
	// 1/swipeCtrl.maxValue

	//	float rememberYPos;
	const float dx = 5f;
	Transform content;
	SwipeControl swipeCtrl;

	protected override void onStart ()
	{
		base.onStart ();
	

		if (!swipeCtrl)
			swipeCtrl = gameObject.GetComponent<SwipeControl> (); //Find SwipeControl on same GameObject if none given

		ignoreTimeScale = true;

		var worldsCnt = 7;
		buttons = new Transform[worldsCnt];
		for (int i = 0; i < worldsCnt; i++) {


			var ch = Instantiate (button);
			ch.parent = transform;
			ch.GetComponent<LevelButton> ().init (i + 1);
			ch.name = "button" + i;
			buttons [i] = ch;
			var p = new Vector3 (dx * i, .4f, 0);
			ch.localPosition = p;

			var d = Instantiate (dot);
			d.parent = transform;
			d.name = "dot" + i;
			p = new Vector3 (.4f * i - worldsCnt * .5f * .4f, -3f, 0);
			d.localPosition = p;
		}
		maxXPos = dx * worldsCnt;
		swipeSmoothFactor = 1f / worldsCnt;

		xDist = maxXPos - minXPos; //calculate distance between min and max


		swipeCtrl.SetMouseRect (new Rect (0, 0, Screen.width, Screen.height * .9f)); //entire screen
		swipeCtrl.maxValue = worldsCnt - 1; //max value
		swipeCtrl.currentValue = 0; //current value set to max, so it starts from the end
		swipeCtrl.startValue = 0;//Mathf.RoundToInt ((gameModel.skin-1) * 0.5f); //when Setup() is called it will animate from the end to the middle
		swipeCtrl.partWidth = Screen.width / 2; //how many pixels do you have to swipe to change the value by one? in this case we make it dependent on the screen-width and the maxValue, so swiping from one edge of the screen to the other will scroll through all values.
		swipeCtrl.Setup ();

	}


	public void init ()
	{

	}

	protected override void onUpdate ()
	{
		base.onUpdate ();

		var v = Mathf.RoundToInt (swipeCtrl.smoothValue);

		for (int i = 0; i < buttons.Length; i++) {	
			//SpriteUtil.setX (buttons [i], worldPos.x+(i+v)*dx);
			if (Mathf.Abs (v - i) < 2) {
				var _x = minXPos + i * (xDist * swipeSmoothFactor) - swipeCtrl.smoothValue * swipeSmoothFactor * xDist;			
				SpriteUtil.setX (buttons [i], _x);
				buttons [i].gameObject.SetActive (true);
			} else {
				buttons [i].gameObject.SetActive (false);
			}
			var d = transform.FindChild ("dot" + i);
			d.GetComponent<tk2dSprite> ().color = (i == v) ? new Color (1, 1, 1) : new Color (0, 0, 0);
		}	
	}

	public int RoomNum {
		get { return (Mathf.RoundToInt (swipeCtrl.smoothValue)); }
	}
}
