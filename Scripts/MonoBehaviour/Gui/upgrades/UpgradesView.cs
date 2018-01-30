using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesView : MonoBehaviour
{

	public ScrollRect scroll;
	public Transform upgradePrefab;
	public Text lblParameters;

	public delegate void UpgradeAction (Upgrade u);

	public event UpgradeAction onClickUpgrade;

	public void show (Upgrade[] upgrade)
	{
		int p = 0;
		foreach (var item in upgrade) {
			var u = GameObject.Instantiate (upgradePrefab, scroll.content);
			u.localPosition = new Vector3 (120 * p++, 0, 0);
			u.GetComponent<UpgradeItem> ().init (item);
		}
		scroll.content.sizeDelta = new Vector2 (p * 120, 100);
	}

	void itemClick (Upgrade item)
	{
		BroadcastMessage ("click", item.id);
		onClickUpgrade (item);
	}
}
