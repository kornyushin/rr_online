using UnityEngine;
using System.Collections;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class UpgradeProxy : Proxy,IFWProxy
{

	public static string NAME = "UpgradeProxy";

	UpgradeModel upgrades = new UpgradeModel ();

	public UpgradeProxy ()
	{
	}

	public UpgradeProxy (string proxyName) : base (proxyName)
	{

	}

	public UpgradeProxy (string proxyName, object data) : base (proxyName, data)
	{

	}



	public Upgrade[] getElements ()
	{
		return upgrades.data.upgrades;
	}
}

