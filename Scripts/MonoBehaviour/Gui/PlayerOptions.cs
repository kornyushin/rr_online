using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour
{
	public delegate void UserAction ();



	public event UserAction onClickPlayer;
	public event UserAction onClickBot;
	public event UserAction onClickRestart;

	public UpgradesView upgradesView;
	public MySlider playerSlider;
	public MySlider enemySlider;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void clickPlayer ()
	{
		onClickPlayer ();
	}

	public void clickBot ()
	{
		onClickBot ();
	}

	public void clickRestart ()
	{
		onClickRestart ();
	}


}
