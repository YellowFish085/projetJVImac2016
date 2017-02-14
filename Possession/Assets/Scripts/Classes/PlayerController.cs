using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Possession;

public class PlayerController : MonoBehaviour {
    public ZombieMovement activeZombie;
    public GameObject scientist;

    // Update is called once per frame
    void Update () {
        GameManager.State currentState = GameManager.Instance.GetState();

        if (currentState == GameManager.State.PAUSE || currentState == GameManager.State.MAIN_MENU)
		{
			return;
		}

        if (Input.GetButtonDown("Jump"))
        {
            activeZombie.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Action");
            if (Input.GetAxis("Vertical") > 0) {
                Debug.Log("Up");
                activeZombie.Action("Up");
            }

            else if (Input.GetAxis("Horizontal") > 0) {
                activeZombie.Action("Right");
            }

            else if (Input.GetAxis("Vertical") < 0) {
                Debug.Log("Down");
                activeZombie.Action("Down");
            }

            else if (Input.GetAxis("Horizontal") < 0) {
                activeZombie.Action("Left");
            }

            else
            {
                activeZombie.Action("");
            }
        }

        float h = Input.GetAxisRaw("Horizontal");
        activeZombie.Move(h);
	}
}
