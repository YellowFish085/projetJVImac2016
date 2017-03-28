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
	private bool _isDestroyingTheFloorAnim = false;

	private Direction _targetedDirection;
	private float _timeOnStart;

	public float chargeSpeed;
	public float destroyTheFloorIntensity;

    private void Awake() {
        var model = transform.FindChild("Cylinder_000_Cylinder_001");
        var material = model.GetComponent<SkinnedMeshRenderer>().material;

        Texture nTex = Resources.Load("zombieTextures/tank") as Texture;

        material.SetTexture("_MainTex", nTex);
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
    }

    void Update () {

		if (_isCharging) {
			Vector2 rgVelocity = GetComponent<Rigidbody2D> ().velocity;
			if (_targetedDirection == Direction.Left) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2(-chargeSpeed, rgVelocity.y);
			} else if (_targetedDirection == Direction.Right) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2(chargeSpeed, rgVelocity.y);
			}
		}
		else if (_isDestroyingTheFloorAnim) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D> ().velocity.x, destroyTheFloorIntensity);
		}

		if( (Time.time - _timeOnStart) > 0.5f) {
			_isCharging = false;
			_isDestroyingTheFloorAnim = false;
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (_isDestroyingTheFloor && col.gameObject.HasTag ("DestructibleObject")) {
			Destroy (col.gameObject);
			Debug.Log (col.gameObject + " was destroyed");
			_isDestroyingTheFloor = false;
		} else if (_isCharging && col.gameObject.HasTag ("DestructibleObject")) {
			Destroy (col.gameObject);
			Debug.Log (col.gameObject.name + " was destroyed");
			_isCharging = false;
		} else {
			_isCharging = false;
			_isDestroyingTheFloor = false;
		}
	}

	public void Charge (Direction direction) {
		if (!_isCharging && !_isDestroyingTheFloor) {
			_isCharging = true;
			if (direction == Direction.None) {
				if(transform.lossyScale.x < 0) {
					_targetedDirection = Direction.Left;
				} else {
					_targetedDirection = Direction.Right;
				}
			} else {
				_targetedDirection = direction;
			}
			_timeOnStart = Time.time;
		}
	}

	public void DestroyTheFloor () {
		if (!_isDestroyingTheFloor && !_isCharging) {
			_isDestroyingTheFloor = true;
			_isDestroyingTheFloorAnim = true;
		}
	}
}