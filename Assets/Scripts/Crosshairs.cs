﻿using UnityEngine;
using System.Collections;

public class Crosshairs : MonoBehaviour {

	public LayerMask targetMask;
	public Color dotHighlightColor;
	Color originalDotColor;
	public SpriteRenderer dot;
	// Update is called once per frame
	void Start(){
		Cursor.visible = false;
		originalDotColor = dot.color;
	}
	void Update () {
		transform.Rotate (Vector3.forward * 40 * Time.deltaTime);
	}

	public void DetectTargets(Ray ray){
		if (Physics.Raycast (ray, 100, targetMask)) {
			dot.color = dotHighlightColor;
		} else {
			dot.color = originalDotColor;
		}
	}
}
