using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using PureMVC.Core;
using System.Collections.Generic;

/// <summary>
/// Sample mediators.
/// </summary>
public class BaseMediator :  Mediator, IFWMediator
{
	public BaseMediator ()
	{
		
	}

	public BaseMediator (string mediatorName) : base (mediatorName)
	{
		
	}

	public BaseMediator (string mediatorName, object viewComponent) : base (mediatorName, viewComponent)
	{		
		
	}

	protected UserProxy user {
		get { return Facade.RetrieveProxy (UserProxy.NAME) as UserProxy; }
	}

	protected UpgradeProxy upgrade {
		get { return Facade.RetrieveProxy (UpgradeProxy.NAME) as UpgradeProxy; }
	}

}
