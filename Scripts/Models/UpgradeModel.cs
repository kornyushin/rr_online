using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeModel
{
	public UpgradeCollection data;

	public UpgradeModel ()
	{
		TextAsset txt = Resources.Load ("items") as TextAsset;
		data = JsonUtility.FromJson<UpgradeCollection> (txt.text);
	}

}

[Serializable]
public class UpgradeCollection
{
	public Upgrade[] upgrades;
}

[Serializable]
public class Upgrade
{
	public int id;
	public string name;
	public string type;
	public int value;
}
