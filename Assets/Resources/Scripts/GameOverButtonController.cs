using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonController : MonoBehaviour {

	bool clickable;
	bool clicked;

	private SpriteRenderer buttonRenderer;

	void Start () {

		clickable = false;
		clicked = false;
		buttonRenderer = transform.Find ("game_over_group").GetComponent <SpriteRenderer> ();
		buttonRenderer.color = new Color (buttonRenderer.color.r, buttonRenderer.color.g, buttonRenderer.color.b, 0);
	}

	void Update () {

	}

	public void OnClicked () {

		if (clickable) {
			if (!clicked) {
				clicked = true;
				SceneManager.LoadSceneAsync ("MainScene");
			}
		}
	}

	public void Appear () {

		Invoke ("OnAppear", 0.01f);
	}

	void OnAppear () {
	
		float alpha = (float) (buttonRenderer.color.a + 0.002);
		if (alpha >= 1) {
			alpha = 1;
			buttonRenderer.color = new Color (buttonRenderer.color.r, buttonRenderer.color.g, buttonRenderer.color.b, alpha);
			clickable = true;
		} else {
			buttonRenderer.color = new Color (buttonRenderer.color.r, buttonRenderer.color.g, buttonRenderer.color.b, alpha);
			Invoke ("OnAppear", 0.01f);
		}
	}
}