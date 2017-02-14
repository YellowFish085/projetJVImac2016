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

        float h = Input.GetAxisRaw("Horizontal");
        activeZombie.Move(h);
	}
}
