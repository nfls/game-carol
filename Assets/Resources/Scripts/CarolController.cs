using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarolController : MonoBehaviour {

	public float flipInterval = 0.8f;
	public AudioClip OnHitSound = null;

	private WorldController worldController;
	private ColdnessBarController coldnessBar;
	private int coldness = 0;

	private GameObject dyingDialogBubble;
	private GameObject warmerDialogBubble;

	private SpriteRenderer carolRenderer;

	void Start () {

		worldController = FindObjectOfType <WorldController> ();
		coldnessBar = FindObjectOfType <ColdnessBarController> ();

		dyingDialogBubble = (GameObject) Resources.Load ("Prefabs/Dying Dialog Bubble");
		dyingDialogBubble = Instantiate (dyingDialogBubble);
		dyingDialogBubble.transform.position = new Vector3 (1, -2, -1);
		dyingDialogBubble.GetComponent <DialogBubbleController> ().Hide ();

		warmerDialogBubble = (GameObject) Resources.Load ("Prefabs/Warmer Dialog Bubble");
		warmerDialogBubble = Instantiate (warmerDialogBubble);
		warmerDialogBubble.transform.position = new Vector3 (1, -2, -1);
		warmerDialogBubble.GetComponent <DialogBubbleController> ().Hide ();

		carolRenderer = transform.Find ("carol").GetComponent <SpriteRenderer> ();

		InvokeRepeating ("Flip", flipInterval, flipInterval);
	}

	void Update () {
		
	}

	void Flip () {

		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		//transform.localScale.Set (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	public void OnHit (string tag) {
	
		if (tag.Equals ("SnowFlake")) {
			coldness += (int) ((UnityEngine.Random.value + 1) * 10);
			if (coldness >= 50) {
				OnDying ();
			}

			if (coldness > 100) {
				coldness = 100;
			}

			coldnessBar.OnColdnessChange (coldness);

			if (coldness == 100) {
				OnDead ();
			}

		} else if (tag.Equals ("GingerMan")) {
			coldness -= (int) ((UnityEngine.Random.value + 1) * 10);

			OnWarmer ();

			if (coldness < 0) {
				coldness = 0;
			}

			coldnessBar.OnColdnessChange (coldness);
		}
	}

	public void OnWarmer () {
	
		if (dyingDialogBubble.activeSelf) {
			dyingDialogBubble.GetComponent <DialogBubbleController> ().Hide ();
		}
		warmerDialogBubble.GetComponent <DialogBubbleController> ().Display (1f);
	}

	public void OnDying () {
	
		if (warmerDialogBubble.activeSelf) {
			warmerDialogBubble.GetComponent <DialogBubbleController> ().Hide ();
		}
		dyingDialogBubble.GetComponent <DialogBubbleController> ().Display (1f);
	}

	public void OnDead () {
	
		worldController.GameOver ();
		Invoke ("FadeOut", 0.01f);
	}

	void FadeOut () {
		
		float alpha = (float) (carolRenderer.color.a - 0.002 );
		if (alpha < 0) {
			alpha = 0;
			carolRenderer.color = new Color (carolRenderer.color.r, carolRenderer.color.g,carolRenderer.color.b, alpha);
		} else {
			carolRenderer.color = new Color (carolRenderer.color.r, carolRenderer.color.g,carolRenderer.color.b, alpha);
			Invoke ("FadeOut", 0.01f);
		}
	}
}