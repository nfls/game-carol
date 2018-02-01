using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFlakeController : MonoBehaviour {

	public static List <AudioClip> onTouchSounds = new List <AudioClip> ();

	public int type;
	public float lifespan = 15;

	void Start () {

	}

	void Update () {


	}
		
	void OnTriggerEnter2D (Collider2D collider) {

		if (collider.gameObject.tag.Equals ("Player")) {
			OnHit (collider.gameObject.GetComponent <CarolController> ());
		}
	}

	public void Init () {

		Invoke ("OnFinished", lifespan);
	}

	public void OnTouch () {

		int r = (int) (UnityEngine.Random.value * 7);
		if (r == 7) {
			r = 6;
		}
		AudioSource.PlayClipAtPoint (onTouchSounds [r], transform.position);
		OnFinished ();
	}

	public void OnHit (CarolController carol) {
	
		carol.OnHit (tag);
		OnFinished ();
	}

	public void OnFinished () {

		CancelInvoke ();
		HeavenController.RecycleSnowFlake (gameObject);
	}
}