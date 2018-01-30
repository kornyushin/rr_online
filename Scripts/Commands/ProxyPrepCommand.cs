using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PureMVC.Interfaces;

public class ProxyPrepCommand : PureMVC.Patterns.SimpleCommand {

	public override void Execute(PureMVC.Interfaces.INotification notification)
	{
		Facade.RegisterProxy( new UserProxy(UserProxy.NAME ) );
		Facade.RegisterProxy( new UpgradeProxy(UpgradeProxy.NAME ) );
	}
}
