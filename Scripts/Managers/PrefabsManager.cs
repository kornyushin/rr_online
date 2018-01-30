using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabsManager : MonoBehaviour
{

	public Transform fire;
	public Transform fire_enemy;
	static PrefabsManager instance;

	public static PrefabsManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<PrefabsManager> ();				
			}
			return instance;
		}
	}
	//caches
	Queue<Transform> fireQueue;

		

	//Methods
	void Awake ()
	{
		fireQueue = new Queue<Transform> ();

	}

	Transform findInCache (Transform obj, Queue<Transform> cache)
	{
		Transform t = null;
		foreach (var item in cache) {
			if (item && !item.gameObject.activeSelf) {
				t = item;
				break;
			}
		}
		if (t == null) {
			t = Instantiate (obj) as Transform;
			cache.Enqueue (t);
		}	
		t.gameObject.SetActive (true);
		return t;
	}

	public Transform getFire ()
	{
		return findInCache (fire, fireQueue);
	}

	public Transform getEnemyFire ()
	{
		return findInCache (fire_enemy, fireQueue);
	}
		

}
