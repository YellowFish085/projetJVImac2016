using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTank : MonoBehaviour {

	// Zombie caracteristics :
	// ----------------------
	//    - speed : slow
	//    - sound : muffled
	//    - jump : can't jump
	//    - carry : can't carry


	// Zombie specific actions :
	// -------------------------
	//    - Charge : charge something if it can be charged and/or broken
	//    - DestroyFloor : destroy the floor if it can be broken



	private bool _isCharging = false;
	private bool _isDestroyingTheFloor = false;

	public enum Direction { Left, Right };
	private Direction _targetedDirection;
	private float _timeOnStart;

	public float chargeSpeed;

	void Update () {

		if( (Time.time - _timeOnStart) > 2*1000f) {
			_isCharging = false;
			_isDestroyingTheFloor = false;
		}


		if (_isCharging) {
			Vector2 rgVelocity = GetComponent<Rigidbody2D> ().velocity;
			if (_targetedDirection == Direction.Left) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2(chargeSpeed, rgVelocity.y);
			} else if (_targetedDirection == Direction.Right) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2(-chargeSpeed, rgVelocity.y);
			}
		}
		else if (_isDestroyingTheFloor) {
			transform.Translate( 0f, Time.deltaTime, 0f);
		}
	}

	void OnCollisionEnter (Collision col) {
		if (_isDestroyingTheFloor && col.gameObject.name == "Ground") {
			Destroy (col.gameObject);
			Debug.Log (col.gameObject.name + " was destroyed");
			_isDestroyingTheFloor = false;
		} else if (_isCharging) {
			Destroy (col.gameObject);
			Debug.Log (col.gameObject.name + " was destroyed");
			_isCharging = false;
		}
	}

	public void Charge (Direction direction) {
		if (!_isCharging && !_isDestroyingTheFloor) {
			_isCharging = true;
			_targetedDirection = direction;
			_timeOnStart = Time.time;
		}
	}

	public void DestroyTheFloor () {
		if (!_isDestroyingTheFloor && !_isCharging) {
			_isDestroyingTheFloor = true;
			_timeOnStart = Time.time;
		}
	}
}