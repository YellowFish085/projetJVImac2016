using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public ZombieMovement activeZombie;
    public GameObject scientist;

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            activeZombie.Jump();
        }

        float h = Input.GetAxisRaw("Horizontal");
        activeZombie.Move(h);
	}
}
