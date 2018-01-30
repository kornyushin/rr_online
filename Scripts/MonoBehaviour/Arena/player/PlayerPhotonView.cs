

using UnityEngine;


[RequireComponent (typeof(PhotonView))]
[AddComponentMenu ("Photon Networking/Photon Transform View")]
public class PlayerPhotonView : MonoBehaviour, IPunObservable
{


	[SerializeField]
	PhotonTransformViewPositionModel m_PositionModel = new PhotonTransformViewPositionModel ();



	PhotonTransformViewPositionControl m_PositionControl;


	PhotonView m_PhotonView;

	bool m_ReceivedNetworkUpdate = false;
	Rigidbody2D m_Body;

	bool m_firstTake = false;

	void Awake ()
	{
		this.m_PhotonView = GetComponent<PhotonView> ();
		this.m_Body = GetComponent<Rigidbody2D> ();
		this.m_PositionControl = new PhotonTransformViewPositionControl (this.m_PositionModel);

	}

	void OnEnable ()
	{
//		m_firstTake = true;
	}

	void Update ()
	{
		if (this.m_PhotonView == null || this.m_PhotonView.isMine == true || PhotonNetwork.connected == false) {
			return;
		}

		this.UpdatePosition ();

	}

	void UpdatePosition ()
	{
		if (this.m_PositionModel.SynchronizeEnabled == false || this.m_ReceivedNetworkUpdate == false) {
			return;
		}
		var ex = 0;// m_Body.velocity.y * Time.deltaTime;
		//Debug.Log (m_Body.velocity.y);
		var p = transform.localPosition;
		p.y = this.m_PositionControl.UpdatePosition (transform.localPosition).y + ex;
		transform.localPosition = p;
	}

	public void SetSynchronizedValues (Vector3 speed, float turnSpeed)
	{
		this.m_PositionControl.SetSynchronizedValues (speed, turnSpeed);
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		this.m_PositionControl.OnPhotonSerializeView (transform.localPosition, stream, info);

		if (this.m_PhotonView.isMine == false && this.m_PositionModel.DrawErrorGizmo == true) {
			this.DoDrawEstimatedPositionError ();
		}

		if (stream.isReading == true) {
			this.m_ReceivedNetworkUpdate = true;

			// force latest data to avoid initial drifts when player is instantiated.
			if (m_firstTake) {
				m_firstTake = false;

				if (this.m_PositionModel.SynchronizeEnabled) {
					this.transform.localPosition = this.m_PositionControl.GetNetworkPosition ();
				}



			}

		}
	}



	void DoDrawEstimatedPositionError ()
	{
		Vector3 targetPosition = this.m_PositionControl.GetNetworkPosition ();

		// we are synchronizing the localPosition, so we need to add the parent position for a proper positioning.
		if (transform.parent != null) {
			targetPosition = transform.parent.position + targetPosition;
		}

		Debug.DrawLine (targetPosition, transform.position, Color.red, 2f);
		Debug.DrawLine (transform.position, transform.position + Vector3.up, Color.green, 2f);
		Debug.DrawLine (targetPosition, targetPosition + Vector3.up, Color.red, 2f);
	}


}