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


	public void Charge () {
	
	}

	public void DestroyTheFloor () {
	
	}
}
