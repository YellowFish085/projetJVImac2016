using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Possession;

public class PlayerController : MonoBehaviour {
    public ZombieMovement activeZombie;

    private void Awake()
    {
        if (!activeZombie)
        {
            Debug.LogError("You need to set an active Zombie");
        }
    }

    // Update is called once per frame
    void Update () {
		if (GameManager.Instance.getState() == GameManager.State.PAUSE || GameManager.Instance.getState() == GameManager.State.MAIN_MENU)
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
