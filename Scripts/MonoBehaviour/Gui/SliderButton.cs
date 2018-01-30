using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderButton : MonoBehaviour ,  IDragHandler
{

	public void OnDrag (PointerEventData data)
	{		
		//SpriteUtil.setY (transform, transform.localPosition.y + data.delta.y);
		object[] obj=new object[2];
		obj [0] = transform;
		obj [1] = data.delta.y;
		SendMessageUpwards ("moveSlider", obj);
	}
}
