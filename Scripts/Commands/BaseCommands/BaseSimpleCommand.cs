using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class BaseSimpleCommand :  SimpleCommand
{
	protected UnityFacade Facade {
		get { return UnityFacade.GetInstance (); }
	}

	protected UserProxy user {
		get { return Facade.RetrieveProxy (UserProxy.NAME) as UserProxy; }
	}
}

