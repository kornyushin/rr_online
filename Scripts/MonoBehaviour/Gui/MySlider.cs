using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


	
[RequireComponent (typeof(RectTransform))]
public class MySlider :  MonoBehaviour
{
	float top;
	public RectTransform btn2;
	float bottom;
	public RectTransform btn1;
	public Text chanses;

	public delegate void UserActionParam (int[] f);

	public event UserActionParam changeSlider;


	void Start ()
	{
		var r = GetComponent<RectTransform> ();
		top = r.sizeDelta.y * .5f;
		bottom = -r.sizeDelta.y * .5f;

	}

	public void updateText (int[] pch)
	{
		var prev = 0;
		var s = "";
		for (int i = 0; i < pch.Length; i++) {
			s = (pch [i] - prev) + "\n" + s;
			prev = pch [i];
		}
		chanses.text = s;
	}

	void normalizeY ()
	{
		var h = GetComponent<RectTransform> ().sizeDelta.y;
		var y1 = Mathf.FloorToInt ((btn1.localPosition.y - bottom) / h * 100);
		var y2 = Mathf.FloorToInt ((btn2.localPosition.y - bottom) / h * 100);
		int[] pch = new int[]{ 0, 0, 100 };
		pch [0] = y2;
		pch [1] = y1;
		changeSlider (pch);
		updateText (pch);
	}

	void moveSlider (object[] obj)
	{
		Transform btn = (Transform)obj [0];
		float y = 0;
		if (btn.name == "btn1") {
			y = Mathf.Clamp (btn.localPosition.y + (float)obj [1], btn2.localPosition.y, top);

		}
		if (btn.name == "btn2") {
			y = Mathf.Clamp (btn.localPosition.y + (float)obj [1], bottom, btn1.localPosition.y);

		}
		SpriteUtil.setY (btn, y);
		normalizeY ();
	}


}

