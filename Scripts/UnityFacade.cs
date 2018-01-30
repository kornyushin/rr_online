using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PureMVC.Interfaces;
using System;

public class UnityFacade : PureMVC.Patterns.Facade
{

	public const string STARTUP = "UnityFacade.StartUp";


	// Override Singleton Factory method
	public static UnityFacade GetInstance ()
	{

		if (m_instance == null) {
			m_instance = new UnityFacade ();
		}
		return m_instance as UnityFacade;
	}

	//InitialController used for initial first functions.
	protected override void InitializeController ()
	{
		base.InitializeController ();
		RegisterCommand (STARTUP, typeof(StartupMacroCommands));
		RegisterCommand (Notification.LOAD_USER, typeof(LoadUserCommand));
		RegisterCommand (Notification.PHOTON_JOIN_ROOM, typeof(PhotonJoinRoomCommand));
		RegisterCommand (Notification.RESTART, typeof(PhotonLeaveRoomCommand));
	}

	ViewComponents __viewComponents;

	public ViewComponents viewComponents {
		get {
			return __viewComponents;
		}
	}

	public PhotonManager photonManager;


	public void StartUp (ViewComponents _view, PhotonManager _photon)
	{
		__viewComponents = _view;
		photonManager = _photon;
		SendNotification (STARTUP);
	}
	/*
	//Handle IMediatorPlug connection
	public void ConnectMediator( IMediatorPlug item )
	{
		Type mediatorType = Type.GetType( item.GetClassRef() );
		if( mediatorType!=null){
			IMediator mediatorPlug = (IMediator)Activator.CreateInstance( mediatorType, item.GetName(), item.GetView() ) ;
			RegisterMediator( mediatorPlug );
		}
	}

	public void DisconnectMediator( string mediatorName )
	{
		RemoveMediator( mediatorName );
	}
	*/
}
