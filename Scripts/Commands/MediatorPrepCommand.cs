using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PureMVC.Interfaces;

public class MediatorPrepCommand : BaseSimpleCommand
{

	public override void Execute (PureMVC.Interfaces.INotification notification)
	{
		
		Facade.RegisterMediator (new PhotonStatusMediator (PhotonStatusMediator.NAME, Facade.viewComponents.lblPhotonStatus));
		Facade.RegisterMediator (new UserPanelMediator (UserPanelMediator.NAME, Facade.viewComponents.userPanel));
		Facade.RegisterMediator (new MainMenuMediator (MainMenuMediator.NAME, Facade.viewComponents.mainMenu));
		Facade.RegisterMediator (new RoomSelectMediator (RoomSelectMediator.NAME, Facade.viewComponents.selectRoomView));
		Facade.RegisterMediator (new ArenaMediator (ArenaMediator.NAME, Facade.viewComponents.arena));
		Facade.RegisterMediator (new PlayerOptionsMediator (PlayerOptionsMediator.NAME, Facade.viewComponents.playerOptions));
	}
}
