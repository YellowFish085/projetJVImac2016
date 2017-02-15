using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Possession;

public class PlayerController : MonoBehaviour {
    public ZombieMovement activeZombie;
    public GameObject scientist;
    public float maxSwappingDistance = 100f;

    private GameObject controlledZombie = null;
    private ZombieSelector zombieSelector;

    // TODO (Victor) : potentiellement découpler dans la classe Player
    enum PlayerState { SWAPPING, CONTROLLING }

    private PlayerState playerState = PlayerState.CONTROLLING;

    // Update is called once per frame
    void Update () {
        GameManager.State currentState = GameManager.Instance.GetState();

        if (currentState == GameManager.State.PAUSE || currentState == GameManager.State.MAIN_MENU)
        {
            return;
        }

        if (playerState == PlayerState.CONTROLLING)
        {
            Controlling();
        }
        else if (playerState == PlayerState.SWAPPING)
        {
            Swapping();
        }
    }

    private void Controlling()
    {
        if (Input.GetButtonDown("Jump"))
        {
            activeZombie.Jump();

			// TODO change this hack into a clean design pattern asap
			var zombieNinja = activeZombie.gameObject.GetComponent<ZombieNinja>();
			if (zombieNinja) {
				zombieNinja.WallJump ();
			}
        }

        float h = Input.GetAxisRaw("Horizontal");
        activeZombie.Move(h);

        if (Input.GetButtonDown("Swap"))
        {
            SetToSwapping();
        }
    }

    private void Swapping()
    {
        UpdateZombiesAround();
        if (Input.GetButtonDown("Cancel"))
        {
            SetToControlling();
        }

        if (Input.GetButtonDown("Swap"))
        {
            controlledZombie = zombieSelector.Next();

            //TODO (Victor) : cache camera to avoid fetching
            CameraMovement camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
            camera.SetTarget(controlledZombie.gameObject);
        }

        if (Input.GetButtonDown("Jump"))
        {
            activeZombie = controlledZombie.GetComponent<ZombieMovement>();
            zombieSelector = null;
            SetToControlling();
        }
    }

    private void SetToSwapping()
    {
        activeZombie.active = false;
        playerState = PlayerState.SWAPPING;
        InitZombieSelector();

        //TODO (Victor) : cache camera to avoid fetching
        CameraMovement camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        camera.SetTarget(scientist);
    }

    private void SetToControlling()
    {
        activeZombie.active = true;
        playerState = PlayerState.CONTROLLING;

        //TODO (Victor) : cache camera to avoid fetching
        CameraMovement camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        camera.SetTarget(activeZombie.gameObject);
    }

    private void InitZombieSelector()
    {
        zombieSelector = new ZombieSelector(scientist.transform.position, maxSwappingDistance);
        GameObject firstZombie = zombieSelector.Next();

        if (firstZombie != null)
        {
            controlledZombie = firstZombie;
        }
    }

    private void UpdateZombiesAround()
    {
        zombieSelector.Update();
    }
}
