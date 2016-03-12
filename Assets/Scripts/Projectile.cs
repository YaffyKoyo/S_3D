﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float speed = 10;
	float damage = 1;

	float lifeTime = 3f;
	float skinWidth = .1f;

	public LayerMask collisionMask;
	public Color trailColor;

	void Start(){
		Destroy (gameObject, lifeTime);

		Collider[] initialCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0) {
			OnHitObject (initialCollisions [0],transform.position);
		}
		GetComponent<TrailRenderer> ().material.SetColor ("_TintColor", trailColor);
	}

	public void SetSpeed(float newSpeed){
		speed = newSpeed;
	}

	// Update is called once per frame
	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector3.forward * moveDistance);
	}

	void CheckCollisions (float moveDistance){
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, moveDistance+skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject (hit.collider,hit.point);
		}
	}


	void OnHitObject(Collider c,Vector3 hitPoint){
		IDamageable damageableObject = c.GetComponent<IDamageable> ();
		if (damageableObject != null) {
			damageableObject.TakeHit (damage,hitPoint,transform.forward);
		}
		GameObject.Destroy (gameObject);
	}
}
