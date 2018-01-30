

public class Notification
{


	public const string PHOTON_STATUS_CHANGED = "photon.change_status";
	public const string PHOTON_JOIN_ROOM = "photon.join_room";
	//user
	public const string LOAD_USER = "user.load";
	public const string USER_LOADED = "user.loaded";
	public const string CREATE_USER = "user.create";
	//GUI
	public const string CLICK_START = "mainmenu.click_start";
	public const string CHANGE_HIT = "usergui.changehit";
	//game
	public const string START_MATCH = "game.start_match";
	public const string RESTART = "game.restart";

	#region notifications type

	public const string ENEMY = "enemy";
	public const string PLAYER = "player";

	#endregion
}
