using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeItem : MonoBehaviour,  IPointerClickHandler
{

	public Text lblId;
	public Text lblName;
	public Text lblType;
	public Text lblCost;
	public RawImage img;

	Upgrade upgrade;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void init (Upgrade item)
	{
		lblId.text = item.id + "";
		lblName.text = item.name;
		lblType.text = item.type;
		lblCost.text = "$" + item.value;
		upgrade = item;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		SendMessageUpwards ("itemClick", upgrade);
	}

	void click (int id)
	{
		if (upgrade.id == id) {
			img.color = Color.green;
		} else {
			img.color = Color.white;
		}
	}
}
