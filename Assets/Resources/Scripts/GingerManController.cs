using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GingerManController : MonoBehaviour {

	public static AudioClip onTouchSound = null;

	public float lifespan = 20;

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

		AudioSource.PlayClipAtPoint (onTouchSound, transform.position);
		GameObject.FindWithTag ("Player").GetComponent <CarolController> ().OnHit (tag);
		OnFinished ();
	}

	public void OnHit (CarolController carol) {

		AudioSource.PlayClipAtPoint (onTouchSound, transform.position);
		carol.OnHit (tag);
		OnFinished ();
	}

	public void OnFinished () {

		CancelInvoke ();
		HeavenController.RecycleGingerMan (gameObject);
	}
}