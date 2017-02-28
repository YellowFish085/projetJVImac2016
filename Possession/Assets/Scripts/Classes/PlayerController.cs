using UnityEngine;
using Possession;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    public ZombieMovement activeZombie;
    public GameObject scientist;
	public GameObject wheel;
    public float maxSwappingDistance = 100f;

    private GameObject controlledZombie = null;
    private ZombieSelector zombieSelector;
    private Player player;

    private void Awake()
    {
        player = new Player();
    }

    // Update is called once per frame
    void Update () {
        GameManager.State gameState = GameManager.Instance.GetState();
        Player.State playerState = player.GetState();

        if (gameState == GameManager.State.PAUSE || gameState == GameManager.State.MAIN_MENU)
        {
            return;
        }

        if (playerState == Player.State.CONTROLLING)
        {
            Controlling();
        }
        else if (playerState == Player.State.SWAPPING)
        {
            Swapping();
        }
    }

    private void Controlling()
    {
        if (Input.GetButtonDown("Jump"))
        {
            activeZombie.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Input.GetAxis("Vertical") > 0) {
                activeZombie.Action(Direction.Up);
            }

            else if (Input.GetAxis("Horizontal") > 0) {
                activeZombie.Action(Direction.Right);
            }

            else if (Input.GetAxis("Vertical") < 0) {
                activeZombie.Action(Direction.Down);
            }

            else if (Input.GetAxis("Horizontal") < 0) {
                activeZombie.Action(Direction.Left);
            }

            else
            {
                activeZombie.Action(Direction.None);
            }
        }

        float h = Input.GetAxisRaw("Horizontal");
        activeZombie.Move(h);

		if (Input.GetButtonDown ("Swap")) {
			SetToSwapping ();
		}
    }

    private void Swapping()
    {
        UpdateZombiesAround();
		if (Input.GetButtonDown("Cancel") || Input.GetButtonUp("Swap"))
        {
            SetToControlling();
        }
    }

    public void SetToSwapping()
    {
		scientist.GetComponentInChildren<BillBoard>().enableDrawCircle();
        activeZombie.active = false;
        player.SetState(Player.State.SWAPPING);
        InitZombieSelector();
		IEnumerable<GameObject> zombies = zombieSelector.GetZombiesAround();

		foreach(GameObject z in zombies)
		{
			scientist.GetComponentInChildren<BillBoard> ().addSelectable(z);
		}

        //TODO (Victor) : cache camera to avoid fetching
        CameraMovement camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        camera.SetTarget(scientist);
    }

    public void SetToControlling()
    {
		scientist.GetComponentInChildren<BillBoard>().disableDrawCircle();
        activeZombie.active = true;
        player.SetState(Player.State.CONTROLLING);

        //TODO (Victor) : cache camera to avoid fetching
        CameraMovement camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        camera.SetTarget(activeZombie.gameObject);
    }

    private void InitZombieSelector()
    {
        zombieSelector = new ZombieSelector(scientist.transform.position, maxSwappingDistance);
        controlledZombie = activeZombie.gameObject;
    }

    private void UpdateZombiesAround()
    {
        zombieSelector.Update();
    }
}
