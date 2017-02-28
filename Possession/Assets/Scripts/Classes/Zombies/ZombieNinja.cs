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
	private Transform guyCheck;
	private bool isAgainstWall;
	private float _jumpForce;
	private float lastDir;
	private RaycastHit2D wallHit;
	private string latestWall;

	private void Awake(){
		wallCheck = transform.Find("wallCheck");
		guyCheck = transform.Find("guyCheck");
		_jumpForce = GetComponent<ZombieMovement>().jumpForce;
	}

	public void WallJump() {
		if (IsAgainstWall () && !GetComponent<ZombieMovement>().IsGrounded()) {
			if (wallHit.collider.name != latestWall) {

				latestWall = wallHit.collider.name;
				float dir = Mathf.Sign (wallCheck.position.x - transform.position.x);
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-dir*_jumpForce*3, _jumpForce));
				GetComponent<ZombieMovement>().Flip(-dir);
			}

		}
	}

	private void Update()
	{
		isAgainstWall = IsAgainstWall ();
		if (GetComponent<ZombieMovement> ().IsGrounded ())
			latestWall = null;
	}

	private bool IsAgainstWall()
	{
		
		wallHit = Physics2D.Linecast(guyCheck.position, wallCheck.position);
		if (wallHit.collider)
		{
			return wallHit.collider.gameObject.HasTag ("Wall");
		}
		else
		{
			return false;
		}

	}
		
}
