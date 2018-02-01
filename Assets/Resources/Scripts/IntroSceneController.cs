using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour {

	public GameObject dialogBubble1;
	public GameObject dialogBubble2;
	public GameObject dialogBubble3;
	public GameObject dialogBubble4;
	public GameObject dialogBubble5;
	public GameObject dialogBubble6;
	public GameObject dialogBubble7;
	public GameObject dialogBubble8;
	public GameObject dialogBubble9;
	public GameObject dialogBubble10;
	public GameObject dialogBubble11;

	public GameObject snowFlake;
	public GameObject gingerMan;

	float displayTime = 3.5f;
	float interval = 4f;

	void Start() {

		dialogBubble1.GetComponent<DialogBubbleController>().Hide();
		dialogBubble2.GetComponent<DialogBubbleController>().Hide();
		dialogBubble3.GetComponent<DialogBubbleController>().Hide();
		dialogBubble4.GetComponent<DialogBubbleController>().Hide();
		dialogBubble5.GetComponent<DialogBubbleController>().Hide();
		dialogBubble6.GetComponent<DialogBubbleController>().Hide();
		dialogBubble7.GetComponent<DialogBubbleController>().Hide();
		dialogBubble8.GetComponent<DialogBubbleController>().Hide();
		dialogBubble9.GetComponent<DialogBubbleController>().Hide();
		dialogBubble10.GetComponent<DialogBubbleController>().Hide();
		dialogBubble11.GetComponent<DialogBubbleController>().Hide();

		snowFlake.SetActive(false);
		gingerMan.SetActive(false);

		bool played = bool.Parse(PlayerPrefs.GetString("playedForOnce", "false"));

		if (played) {
			Invoke("PlayPhase12", 0);
		} else {
			Invoke("PlayPhase1", interval);
		}
	}

	void Update() {

	}

	void PlayPhase1() {

		dialogBubble1.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase2", interval);
	}

	void PlayPhase2() {

		dialogBubble2.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase3", interval);
	}

	void PlayPhase3() {

		dialogBubble3.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase4", interval);
	}

	void PlayPhase4() {

		dialogBubble4.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase5", interval);
	}

	void PlayPhase5() {

		dialogBubble5.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase6", interval);
	}

	void PlayPhase6() {

		snowFlake.SetActive(true);
		dialogBubble6.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase7", interval);
	}

	void PlayPhase7() {

		dialogBubble7.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase8", interval);
	}

	void PlayPhase8() {

		gingerMan.SetActive(true);
		dialogBubble8.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase9", interval);
	}

	void PlayPhase9() {

		dialogBubble9.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase10", interval);
	}

	void PlayPhase10() {

		dialogBubble10.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase11", interval);
	}

	void PlayPhase11() {

		dialogBubble11.GetComponent<DialogBubbleController>().Display(displayTime);
		Invoke("PlayPhase12", interval);
	}

	void PlayPhase12() {
		PlayerPrefs.SetString("playedForOnce", "true");
		SceneManager.LoadSceneAsync("MainScene");
	}
}
