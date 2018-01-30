using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class PhotonMonoBehaviourCommand :  Photon.MonoBehaviour, INotifier, ICommand
{
	public virtual void Execute (PureMVC.Interfaces.INotification notification)
	{
		
	}

	public void SendNotification (string notificationName)
	{
		m_facade.SendNotification(notificationName);
	}

	public void SendNotification (string notificationName, object body)
	{
		m_facade.SendNotification(notificationName, body);
	}

	public void SendNotification (string notificationName, object body, string type)
	{
		m_facade.SendNotification(notificationName, body, type);
	}



	#region Accessors

	/// <summary>
	/// Local reference to the Facade Singleton
	/// </summary>
	protected IFacade Facade
	{
		get { return m_facade; }
	}

	#endregion

	#region Members

	/// <summary>
	/// Local reference to the Facade Singleton
	/// </summary>
	private IFacade m_facade = UnityFacade.GetInstance();

	#endregion
}


