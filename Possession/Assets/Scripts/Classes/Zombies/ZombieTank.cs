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

	void Update () {
		if (_isCharging) {
			Charge ();
		}
		else if (_isDestroyingTheFloor) {
			DestroyTheFloor ();
		}
	}

	void OnCollisionEnter (Collision col) {
		Debug.Log (col.gameObject.name);
	}

	public void Charge () {
		transform.Translate( Time.deltaTime, 0f, 0f);

		// TODO : - set good direction with speed
		//        - add animation on start/end
	}

	public void DestroyTheFloor () {
		transform.Translate( 0f, -Time.deltaTime, 0f);

		// TODO : - keep negative value
		//        - set good direction with speed
		//        - add animation on start/end
	}

	public bool GetIsCharging () {
		return _isCharging;
	}

	public bool GetIsDestroyingTheFloor () {
		return _isDestroyingTheFloor;
	}

	public void SetIsCharging (bool state) {
		_isCharging = state;
	}

	public void SetIsDestroyingTheFloor (bool state) {
		_isDestroyingTheFloor = state;
	}

}