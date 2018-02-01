using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdnessBarController : MonoBehaviour {

	public GameObject frame = null;
	public GameObject fill = null;

	private BoxCollider2D fillCollider;
	private Vector3 originalFillSize;

	public float maxValue = 100;

	void Start () {

		fillCollider = fill.GetComponent <BoxCollider2D> ();
		originalFillSize = fillCollider.bounds.size;

		OnColdnessChange (0);
	}

	void Update () {
		
	}

	public void OnColdnessChange (float currentValue) {
		fill.transform.localScale = new Vector3 (fill.transform.localScale.x, currentValue / maxValue, fill.transform.localScale.z);

		Vector3 sizeAfter = fillCollider.bounds.size;

		fill.transform.localPosition = new Vector3 (fill.transform.localPosition.x, -(originalFillSize.y - sizeAfter.y), fill.transform.localPosition.z);
	}
}
