using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBubbleController : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}

	public void Display () {

		gameObject.SetActive (true);
		CancelInvoke ();
	}

	public void Display (float time) {
	
		gameObject.SetActive (true);
		CancelInvoke ();
		Invoke ("Hide", time);
	}

	public void Hide () {
	
		gameObject.SetActive (false);
	}
}
