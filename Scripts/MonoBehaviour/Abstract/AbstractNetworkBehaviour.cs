using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AbstractNetworkBehaviour : Photon.MonoBehaviour
{
//	public string state;
	protected bool ignoreTimeScale = false;



	protected GameModel gameModel {
		get {
			return GameModel.GetInstance ();
		}
	}

	public Rigidbody2D rigidBody2D {
		get {
			return GetComponent<Rigidbody2D> ();
		}
	}

	public Collider2D collider2D {
		get {
			return GetComponent<Collider2D> ();
		}
	}

	protected float x {
		get {
			return transform.localPosition.x;
		}
		set {
			var p = transform.localPosition;
			p.x = value;
			transform.localPosition = p;
		}
	}

	protected float y {
		get {
			return transform.localPosition.y;
		}
		set {
			var p = transform.localPosition;
			p.y = value;
			transform.localPosition = p;
		}
	}

	protected float z {
		get {
			return transform.localPosition.z;
		}
		set {
			var p = transform.localPosition;
			p.z = value;
			transform.localPosition = p;
		}
	}

	protected virtual void onStart ()
	{

	}

	// Use this for initialization
	void Start ()
	{
		onStart ();
	}

	protected virtual void onAwake ()
	{

	}

	void Awake ()
	{
		onAwake ();
	}

	protected virtual void onEnable ()
	{

	}

	void OnEnable ()
	{
		onEnable ();
	}

	protected virtual void onDisable ()
	{

	}

	void OnDisable ()
	{
		onDisable ();
	}

	protected virtual void onUpdate ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		onUpdate ();
	}

	protected virtual void onFixedUpdate ()
	{
		
	}

	void FixedUpdate ()
	{
		onFixedUpdate ();
	}

	protected virtual void onDestroy ()
	{
		
	}

	void OnDestroy ()
	{
		onDestroy ();
	}

	protected virtual void setXY (float _x, float _y)
	{			
		x = _x;
		y = _y;				
	}

	protected virtual void setXYZ (float _x, float _y, float _z)
	{			
		x = _x;
		y = _y;				
		z = _z;
	}

	protected void setScaleX (float x)
	{
		Vector3 theScale = transform.localScale;
		theScale.x = x;
		transform.localScale = theScale;
	}
}
