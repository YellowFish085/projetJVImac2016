using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBerserkController : MonoBehaviour {

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

	public void Charge () {
		transform.Translate( Time.deltaTime, 0, 0);
		// TODO : - set good direction with speed
		//        - add animation on start/end
	}

	public void DestroyTheFloor () {
		transform.Translate( 0, -Time.deltaTime, 0);

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
