using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneDirectorController : MonoBehaviour
{

	public GameObject background = null;
	public AudioClip music1 = null;
	public AudioClip music2 = null;

	public GameObject air = null;
	public GameObject carol = null;
	public GameObject sleigh = null;

	public GameObject dialogBubble1 = null;
	public GameObject dialogBubble2 = null;
	public GameObject dialogBubble3 = null;
	public GameObject dialogBubble4 = null;
	public GameObject dialogBubble5 = null;
	public GameObject dialogBubble6 = null;
	public GameObject dialogBubble7 = null;
	public GameObject dialogBubble8 = null;

	public GameObject cast = null;
	public GameObject gameOverButton = null;

	public float prepareTime = 0f;

	private AudioSource audioSource;
	private int phaseNum = 0;

	void Start()
	{

		dialogBubble1.GetComponent<DialogBubbleController>().Hide();
		dialogBubble2.GetComponent<DialogBubbleController>().Hide();
		dialogBubble3.GetComponent<DialogBubbleController>().Hide();
		dialogBubble4.GetComponent<DialogBubbleController>().Hide();
		dialogBubble5.GetComponent<DialogBubbleController>().Hide();
		dialogBubble6.GetComponent<DialogBubbleController>().Hide();
		dialogBubble7.GetComponent<DialogBubbleController>().Hide();
		dialogBubble8.GetComponent<DialogBubbleController>().Hide();
		audioSource = GetComponent<AudioSource>();
		cast.SetActive(false);
		gameOverButton.SetActive(false);

		audioSource.clip = music1;
		audioSource.Play();
		Invoke("PlayPhase1", prepareTime);
	}

	void Update()
	{

		if (phaseNum == 2) {
			if (sleigh.transform.position.x > -0.05 && sleigh.transform.position.x < 0.05) {
				sleigh.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				Invoke("PlayPhase3", 1f);
			}
		} else if (phaseNum == 14) {
			if (sleigh.transform.position.x > 6) {
				//sleigh.GetComponent <Rigidbody2D> ().velocity = Vector2.zero;
				sleigh.SetActive(false);
				Invoke("PlayPhase15", 0f);
			}
		} else if (phaseNum == 15) {
			if (cast.transform.position.y > 12) {
				cast.SetActive(false);
				phaseNum = 16;
				gameOverButton.SetActive(true);
				gameOverButton.GetComponent<GameOverButtonController>().Appear();
			}
		} else if (phaseNum == 16) {
			if (Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero);
				if (hit.collider != null) {
					if (hit.collider.gameObject.name.Contains("Game Over Button")) {
						hit.transform.GetComponent<GameOverButtonController>().OnClicked();
					}
				}
			}
		}
	}

	void PlayPhase1()
	{

		phaseNum = 1;
		dialogBubble1.GetComponent<DialogBubbleController>().Display(2);
		Invoke("PlayPhase2", 2.5f);
	}

	void PlayPhase2()
	{

		phaseNum = 2;
		sleigh.GetComponent<Rigidbody2D>().velocity = new Vector2(2, -1);
	}

	void PlayPhase3()
	{

		phaseNum = 3;
		dialogBubble2.GetComponent<DialogBubbleController>().Display(2);
		Invoke("PlayPhase4", 3.5f);
	}

	void PlayPhase4()
	{

		phaseNum = 4;
		dialogBubble3.GetComponent<DialogBubbleController>().Display(2);
		Invoke("PlayPhase5", 2.5f);
	}

	void PlayPhase5()
	{

		phaseNum = 5;
		air.SetActive(false);
		Invoke("PlayPhase6", 1f);
	}

	void PlayPhase6()
	{

		phaseNum = 6;
		air.SetActive(true);
		air.transform.Find("AirWithHat").localScale = new Vector3(0.8f, 0.8f, 1);
		air.transform.position = new Vector3(-2, carol.transform.position.y, 0);
		air.GetComponent<Rigidbody2D>().simulated = false;
		carol.transform.position = new Vector3(2, carol.transform.position.y, 0);
		Invoke("PlayPhase7", 1f);
	}

	void PlayPhase7()
	{

		phaseNum = 7;
		dialogBubble4.GetComponent<DialogBubbleController>().Display(2);
		Invoke("PlayPhase8", 3.5f);
	}

	void PlayPhase8()
	{

		phaseNum = 8;
		dialogBubble5.GetComponent<DialogBubbleController>().Display(2);
		Invoke("PlayPhase9", 3.5f);
	}

	void PlayPhase9()
	{

		phaseNum = 9;
		dialogBubble6.GetComponent<DialogBubbleController>().Display(2);
		Invoke("PlayPhase10", 3.5f);
	}

	void PlayPhase10()
	{

		phaseNum = 10;
		dialogBubble7.GetComponent<DialogBubbleController>().Display(2);
		Invoke("PlayPhase11", 3.5f);
	}

	void PlayPhase11()
	{

		phaseNum = 11;
		air.transform.Find("AirWithHat").localScale = new Vector3(0.4f, 0.4f, 1);
		air.transform.parent = sleigh.transform;
		air.transform.localPosition = new Vector3(-0.6f, 0.6f, 0);
		air.SetActive(false);
		carol.transform.parent = sleigh.transform;
		carol.transform.localPosition = new Vector3(-2f, 0.6f, 0);
		//carol.transform.localScale = new Vector3(0.4f, 0.4f, 1);
		carol.SetActive(false);
		Invoke("PlayPhase12", 1f);
	}

	void PlayPhase12()
	{

		phaseNum = 12;
		air.SetActive(true);
		carol.SetActive(true);
		Invoke("PlayPhase13", 1f);
	}

	void PlayPhase13()
	{

		phaseNum = 13;
		dialogBubble8.GetComponent<DialogBubbleController>().Display(2f);
		Invoke("PlayPhase14", 3.5f);
	}

	void PlayPhase14()
	{

		phaseNum = 14;
		sleigh.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 1);
		audioSource.clip = music2;
		audioSource.Play();
	}

	void PlayPhase15()
	{

		phaseNum = 15;
		cast.SetActive(true);
		cast.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.4f);
	}
}
