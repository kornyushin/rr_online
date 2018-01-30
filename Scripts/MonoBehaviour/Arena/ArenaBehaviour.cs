using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBehaviour : MonoBehaviour
{
	public tk2dTextMesh lblMessage;
	public Transform photonPrefab;
	public Transform botPrefab;
	public Transform spawnPlayer1;
	public Transform spawnPlayer2;

	public Transform ground;
	public Transform sky;

	public delegate void ArenaAction ();

	public event ArenaAction onHitPlayer;
	public event ArenaAction onHitEnemy;

	int startTimer;

	public const string JUMP = "jump";
	public const string WAIT = "wait";
	public const string WAIT_SERVER_TIME = "waitservertime";
	public const string WAIT_START = "waitstart";
	public const string ACTIVE = "active";
	public const string LOSE = "lose";

	float startMatchTime = -1;
	[HideInInspector]
	public string state;

	string message;

	int hitTimer;

	// Use this for initialization
	void Start ()
	{
		state = WAIT_START;
		message = "start in ";
	}
	
	// Update is called once per frame
	void Update ()
	{
//		Debug.Log (PhotonNetwork.time);
		if (state == WAIT_SERVER_TIME) {
			Debug.Log (PhotonNetwork.time + " " + startMatchTime);
			if (PhotonNetwork.time >= startMatchTime) {
				var l = GameObject.FindWithTag ("left").GetComponent<MoveController> ();
				var r = GameObject.FindWithTag ("right").GetComponent<MoveController> ();
				l.enemy = r;
				r.enemy = l;
				state = ACTIVE;
			}
		}
		if (state == ACTIVE && ++hitTimer > 600) {
			if (sky.localPosition.y > .1f)
				sky.Translate (0, -.01f, 0);
			if (ground.localPosition.y < .1f)
				ground.Translate (0, .01f, 0);
			BroadcastMessage ("changeBorder", .01f);
		}
	}

	public void createPlayer (UserModel user, UserModel enemy)
	{		


		var obj = new object [3];
		string moveValue = "";
		for (int i = 0; i < 100; i++) {
			moveValue += (Random.Range (30, 100)) + ",";
		}
		var behaviour = PlayerParameters.HIDE;
		#if UNITY_EDITOR
		behaviour = PlayerParameters.HIDE;
		#endif
		obj [0] = moveValue + Random.Range (30, 100);
		obj [1] = "skin000" + (Random.Range (0, 5) * 2 + 1);
		obj [2] = user.positionChanses;
		var go = PhotonNetwork.Instantiate (photonPrefab.name, Vector3.zero, Quaternion.identity, 0, obj);//.GetComponent<PhotonBehaviour> ();

		startTimer = 3;
		showStarMathcMessage ();

		if (Options.WITH_BOT) {
			var b = GameObject.Instantiate (botPrefab);
			b.GetComponent<BotBehaviour> ().init (enemy.positionChanses);
		}

	}

	void showStarMathcMessage ()
	{
		lblMessage.transform.localScale = Vector3.one;

		lblMessage.text = message + startTimer;
//		Debug.Log (PhotonNetwork.time + " " + PhotonNetwork.ServerTimestamp); 
		startTimer--;
		if (startTimer == 0) {
			lblMessage.text = "";
			if (state == WAIT)
				state = ACTIVE;
			Time.timeScale = 5;
		} else if (startTimer > 0) {
			LeanTween.scale (lblMessage.gameObject, new Vector3 (1.3f, 1.3f, 1.3f), .5f)
				.setUseEstimatedTime (true)
				.setOnComplete (showStarMathcMessage);
			if (startTimer == 1 && state == WAIT_START) {
//				Debug.Log ("setStartMatchTimer");
				if (Options.WITH_BOT) {
					state = WAIT;
				} else {
					BroadcastMessage ("setStartMatchTimer");
				}
			}
		}
	}

	void hitPlayer ()
	{
		onHitPlayer ();
//		message = "you were shot";
//		lblMessage.color = Color.green;
//		startTimer = 3;
//		Debug.Log ("вас подстрелили ");
//		state = WAIT;
//		showStarMathcMessage ();
//		Time.timeScale = 0;
		hitTimer = 0;
//		destroyAllFire ();
	}

	void hitEnemy ()
	{
		onHitEnemy ();
//		message = "you hit!!!";
//		lblMessage.color = Color.red;
//		Debug.Log ("вы попали ");
//		startTimer = 3;
//		showStarMathcMessage ();
//		state = WAIT;
//		Time.timeScale = 0;
		hitTimer = 0;
//		destroyAllFire ();
	}

	void destroyAllFire ()
	{
		var elements = GameObject.FindObjectsOfType<FireBehaviour> ();
		foreach (var item in elements) {
			Destroy (item.gameObject);
		}
	}

	public void setStartMatchTime (float _t)
	{
		Debug.Log (state);
		state = WAIT_SERVER_TIME;
		startMatchTime = _t;
	}

	public void removeAll ()
	{
		state = WAIT;
		hitTimer = 0;
		SpriteUtil.setY (sky, 6.43f);
		SpriteUtil.setY (ground, -5.85f);
		var elements = GameObject.FindObjectsOfType<AbstractGO> ();
		foreach (var item in elements) {
			Destroy (item.gameObject);
		}
	}
}
