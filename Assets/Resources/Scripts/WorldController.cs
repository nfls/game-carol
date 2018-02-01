using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour {

	public AudioClip playTimeMusic = null;
	public AudioClip gameOverMusic = null;
	public AudioClip christmasCarol = null;

	public float maxGenerationInterval = 2f;
	public float minGenerationInterval = 0.05f;
	public float totalTime = 90f;

	private Camera mainCamera;
	private TextMesh textClock;
	private GameObject gameOverButton;
	private AudioSource audioSource;
	private static AudioClip carolOnTouchSound;
	private static AudioClip onTouchSound;
	private bool timeOver = false;
	private bool gameOver = false;
	private float rate;
	private double startTime;
	private double currentTime;
	private float currentInterval;
	private int gingerManNum;
	private static System.DateTime beginTime = TimeZone.CurrentTimeZone.ToLocalTime (new System.DateTime (1970, 1, 1));

	void Start () {

		textClock = transform.Find ("TextClock").GetComponent <TextMesh> ();
		gameOverButton = (GameObject) Resources.Load ("Prefabs/Game Over Button");
		gameOverButton = Instantiate (gameOverButton);
		gameOverButton.SetActive (false);

		for (int i = 0; i < 7; i ++) {
			SnowFlakeController.onTouchSounds.Add ((AudioClip) (Resources.Load ("Audios/snow_flake_sound_" + (i + 1))));
		}
		GingerManController.onTouchSound = (AudioClip) Resources.Load ("Audios/ginger_man_sound");	
		carolOnTouchSound = (AudioClip) Resources.Load ("Audios/deng");
		onTouchSound = (AudioClip) Resources.Load ("Audios/switch_sound");

		audioSource = GetComponent <AudioSource> ();
		audioSource.loop = true;

		Input.simulateMouseWithTouches = true;
		mainCamera = Camera.main;

		Init ();
	}

	void Init () {

		textClock.text = "~~";

		audioSource.clip = playTimeMusic;
		audioSource.Play ();

		gingerManNum = 0;

		HeavenController.Init ();

		rate = (maxGenerationInterval - minGenerationInterval) / totalTime;
		currentInterval = maxGenerationInterval;
		startTime = DateToSeconds (System.DateTime.Now.ToLocalTime ());

		Invoke ("GenerateSnowFlake", maxGenerationInterval);
	}

	void Update () {

		double timePassed = DateToSeconds (System.DateTime.Now.ToLocalTime ()) - startTime;

		if (!gameOver && timePassed >= totalTime) {
			CancelInvoke ();
			TimeOver ();
		}

		if (!gameOver && !timeOver) {
			textClock.text = "Time Left\n" + ((int) (totalTime - timePassed)).ToString () + " s";
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (ray.origin.x, ray.origin.y), Vector2.zero);
			if (hit.collider != null) {
				string tag = hit.collider.gameObject.tag;
				if (tag.Equals ("SnowFlake")) {
					hit.collider.gameObject.GetComponent <SnowFlakeController> ().OnTouch ();
				} else if (tag.Equals ("GingerMan")) {
					hit.collider.gameObject.GetComponent <GingerManController> ().OnTouch ();
				} else if (tag.Equals ("Player")) {
					AudioSource.PlayClipAtPoint (carolOnTouchSound, mainCamera.ScreenToWorldPoint (Input.mousePosition));
				} else if (hit.collider.gameObject.name.Contains ("Game Over Button")) {
					AudioSource.PlayClipAtPoint (onTouchSound, mainCamera.ScreenToWorldPoint (Input.mousePosition));
					hit.transform.GetComponent <GameOverButtonController> ().OnClicked ();
				} else {
					AudioSource.PlayClipAtPoint (onTouchSound, mainCamera.ScreenToWorldPoint (Input.mousePosition));
				}
			} else {
				AudioSource.PlayClipAtPoint (onTouchSound, mainCamera.ScreenToWorldPoint (Input.mousePosition));
			}
		}

	}

	private void TimeOver () {
	
		timeOver = true;
		textClock.text = "~~";
		OnFinished ();
		SceneManager.LoadSceneAsync ("GoodEndScene");
	}

	public void GameOver () {
	
		gameOver = true;
		textClock.text = "~~";
		OnFinished ();

		gameOverButton.SetActive (true);
		gameOverButton.GetComponent <GameOverButtonController> ().Appear ();

		AudioSource.PlayClipAtPoint (gameOverMusic, transform.position);
	}

	private void OnFinished () {
	
		CancelInvoke ();
		GameObject[] snow_flakes = GameObject.FindGameObjectsWithTag ("SnowFlake");

		for (int i = 0; i < snow_flakes.Length; i ++) {
			snow_flakes [i].GetComponent <SnowFlakeController> ().OnFinished ();
		}

		GameObject[] ginger_mans = GameObject.FindGameObjectsWithTag ("GingerMan");

		for (int i = 0; i < ginger_mans.Length; i ++) {
			ginger_mans [i].GetComponent <GingerManController> ().OnFinished ();
		}
	}

	private void GenerateSnowFlake () {

		if (gameOver) {
			return;
		} else {
			GameObject snow_flake = HeavenController.GetSnowFlake ();

			Vector3 position = new Vector3 ((float) ((UnityEngine.Random.value - 0.5) * 5), (float) (UnityEngine.Random.value - 0.5 + 5), -2);
			snow_flake.transform.position = position;

			Vector2 velocity;

			if (UnityEngine.Random.value > 0.5) {
				velocity = new Vector2 (0, (float) -(UnityEngine.Random.value + 0.8));
			} else {
				velocity = new Vector2 ((float) (UnityEngine.Random.value - 0.5) / 2, (float) -(UnityEngine.Random.value + 0.8));
			}

			snow_flake.GetComponent <Rigidbody2D> ().velocity = velocity;

			if (UnityEngine.Random.value > 0.5) {
				snow_flake.GetComponent <Rigidbody2D> ().angularVelocity = UnityEngine.Random.value * 10;
			}

			snow_flake.GetComponent <SnowFlakeController> ().Init ();
			
			currentTime = DateToSeconds (System.DateTime.Now.ToLocalTime ());
			currentInterval = (float) (maxGenerationInterval - (currentTime - startTime) * rate);

			if (currentInterval >= minGenerationInterval) {
				Invoke ("GenerateSnowFlake", currentInterval);
			}

			if (gingerManNum < 5) {
				if ((int) (UnityEngine.Random.value * 30) == 15) {
					GenerateGingerMan ();
				}
			}
		}
	}

	private void GenerateGingerMan () {

		gingerManNum += 1;
	
		GameObject ginger_man = HeavenController.GetGingerMan ();

		Vector3 position = new Vector3 ((float) ((UnityEngine.Random.value - 0.5) * 5), (float) (UnityEngine.Random.value - 0.5 + 5), -2);
		ginger_man.transform.position = position;

		Vector2 velocity;

		if (UnityEngine.Random.value > 0.5) {
			velocity = new Vector2 (0, (float) -(UnityEngine.Random.value + 0.8));
		} else {
			velocity = new Vector2 ((float) (UnityEngine.Random.value - 0.5) / 2, (float) -(UnityEngine.Random.value + 0.8));
		}

		ginger_man.GetComponent <Rigidbody2D> ().velocity = velocity;

		if (UnityEngine.Random.value > 0.5) {
			ginger_man.GetComponent <Rigidbody2D> ().angularVelocity = UnityEngine.Random.value * 10;
		}

		ginger_man.GetComponent <GingerManController> ().Init ();
	}

	public static double DateToSeconds (System.DateTime time) {

		return (time - beginTime).TotalSeconds;
	}
}