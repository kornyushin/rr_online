using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PureMVC.Interfaces;
using PlayFab;
using PlayFab.ClientModels;

public class LoadUserCommand : BaseSimpleCommand
{
	string PlayFabId = "CB50";

	public override void Execute (PureMVC.Interfaces.INotification notification)
	{
		PlayFabSettings.TitleId = PlayFabId;


		SendNotification (Notification.PHOTON_STATUS_CHANGED, "load player...");
		if (Options.DUMMY_USER) {
			loadTestUser ();
		} else {
			Login (PlayFabId);
		}
	}

	int tmpskin;

	void Login (string titleId)
	{
		tmpskin = UnityEngine.Random.Range (2, 10) * 2 + 1;
		#if UNITY_EDITOR
		var cid = SystemInfo.deviceUniqueIdentifier + "1";
		tmpskin = 3;
		#else	
		var cid= SystemInfo.deviceUniqueIdentifier;
		#endif
		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest () {
			TitleId = titleId,
			CreateAccount = true,
			CustomId = cid
		};

		PlayFabClientAPI.LoginWithCustomID (request, (result) => {
			PlayFabId = result.PlayFabId;
//			Debug.Log ("Got PlayFabID: " + PlayFabId);

			if (result.NewlyCreated) {
//				Debug.Log ("(new account)");
			} else {
//				Debug.Log ("(existing account)");
			}
			GetUserData ();
		},
			(error) => {
				Debug.Log ("Error logging in player with custom ID:");
				Debug.Log (error.ErrorMessage);
			});
	}

	void GetUserData ()
	{
		GetUserDataRequest request = new GetUserDataRequest () {
			PlayFabId = PlayFabId,
			Keys = null
		};

		PlayFabClientAPI.GetUserData (request, (result) => {
			Debug.Log ("Got user data:");
			if ((result.Data == null) || (result.Data.Count == 0)) {
				Debug.Log ("No user data available");
				SendNotification (Notification.CREATE_USER);
//				PopupManager.Instance.showEnterName ();
			} else {
				if (result.Data.Count == 0) {
					SendNotification (Notification.CREATE_USER);
					//PopupManager.Instance.showEnterName ();
				} else {
//					Parse and create user
					user.parse (result.Data);
					SendNotification (Notification.USER_LOADED);
					if(Options.QUICK_START){
						SendNotification (Notification.CLICK_START);
						SendNotification (Notification.PHOTON_JOIN_ROOM, Options.ROOM_NUM);
					}
				}

			}
		}, (error) => {
			Debug.Log ("Got error retrieving user data:");
			Debug.Log (error.ErrorMessage);
		});
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "user loaded");
	}



	void loadTestUser ()
	{
		user.loadDummy ();
		SendNotification (Notification.USER_LOADED);
		if(Options.QUICK_START){
			SendNotification (Notification.CLICK_START);
			SendNotification (Notification.PHOTON_JOIN_ROOM, Options.ROOM_NUM);
		}
		SendNotification (Notification.PHOTON_STATUS_CHANGED, "");
	}
}
