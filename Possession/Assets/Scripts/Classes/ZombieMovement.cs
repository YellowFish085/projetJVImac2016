using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour {
    public float jumpForce;
    public float lateralAirborneAcceleration;
    public float maxSpeed;

    [HideInInspector]
    public bool active = true;

    private bool grounded = false;
    private Transform groundCheck;

    private void Awake()
    {
        groundCheck = transform.Find("groundCheck");
    }

    private void Update()
    {
        grounded = IsGrounded();
    }

    public void Jump()
    {
        if (!enabled) return;

        if (grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }

    public void Action(Direction direction)
    {
        Debug.Log("Action");

        if(name == "Seductive")
        {
            GetComponent<ZombieSeductive>().Seduce();
        }

		else if (GetComponent<ZombieTank> ()){
			if (direction == Direction.Left || direction == Direction.Right || direction == Direction.None) {
				GetComponent<ZombieTank> ().Charge (direction);
			} else if (direction == Direction.Down) {
				GetComponent<ZombieTank> ().DestroyTheFloor ();
			}
		}
    }

    /// <summary>
    /// Méthode appelée par le PlayerController. 
    /// Le mouvement est fait pour être arcade et pas chiant, aka 0 inertie sauf dans les sauts.
    /// </summary>
    /// <param name="magnitude">Axe du stick/bouton pressé</param>
    public void Move(float magnitude)
    {

        if (!enabled) return;

        if (magnitude != 0)
        {
            Flip(magnitude);
        }

        if (grounded && magnitude == 0 && GetComponent<Rigidbody2D>().velocity.x != 0)
        {
            StopX();
        }

        if (grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(magnitude * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * magnitude * lateralAirborneAcceleration);
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void StopX()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(30, GetComponent<Rigidbody2D>().velocity.y);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck.position);
        if (hit.collider)
        {
            return hit.collider.gameObject.HasTag("Ground");
        }
        else
        {
            return false;
        }
    }

    private void Flip(float lateralAcceleration)
    {
		Vector3 currentScale = transform.localScale;
		currentScale.x = Mathf.Sign(lateralAcceleration);
		transform.localScale = currentScale;
    }
}
