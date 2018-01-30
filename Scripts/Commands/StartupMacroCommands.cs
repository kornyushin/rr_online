using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class StartupMacroCommands : MacroCommand
{

	override protected void InitializeMacroCommand ()
	{
		AddSubCommand (typeof(ProxyPrepCommand));
		AddSubCommand (typeof(MediatorPrepCommand));
		AddSubCommand (typeof(PhotonConnectCommand));
	}
}
