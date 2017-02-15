using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNinja : MonoBehaviour {

	// Zombie caracteristics :
	// ----------------------
	//    - speed : fast
	//    - sound : "hop hop"
	//    - jump : quick jumps
	//    - carry : can't carry


	// Zombie specific actions :
	// -------------------------
	//    - Walljump : gain another jump if against a wall


	private Transform wallCheck;
	private bool isAgainstWall;
	private float _jumpForce;
	private float lastDir;

	private void Awake(){
		wallCheck = transform.Find("wallCheck");
		_jumpForce = GetComponent<ZombieMovement>().jumpForce;
	}

	public void WallJump() {
		if (IsAgainstWall () && !GetComponent<ZombieMovement>().IsGrounded()) {

			float dir = Mathf.Sign (wallCheck.position.x - transform.position.x);

			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-dir*_jumpForce*3, _jumpForce));
			GetComponent<ZombieMovement>().Flip(-dir);
		}
	}

	private void Update()
	{
		isAgainstWall = IsAgainstWall ();

	}

	private bool IsAgainstWall()
	{
		return Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Wall"));
	}
		
}
