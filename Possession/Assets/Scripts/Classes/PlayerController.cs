using System.Collections.Generic;
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

			// TODO change this hack into a clean design pattern asap
			var zombieNinja = activeZombie.gameObject.GetComponent<ZombieNinja>();
			if (zombieNinja) {
				zombieNinja.WallJump ();
			}
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Action");

            activeZombie.Action(ComputeDirection());
        }

        float h = Input.GetAxisRaw("Horizontal");
        activeZombie.Move(h);

		if (Input.GetButtonDown ("Swap")) {
			SetToSwapping ();
		}
    }

    private Direction ComputeDirection()
    {
        float hDir = Input.GetAxis("Horizontal");
        float vDir = Input.GetAxis("Vertical");

        if(Mathf.Abs(hDir) > Mathf.Abs(vDir))
        {
            return hDir > 0 ? Direction.Right : Direction.Left; //Made by Lucas Horand aka Luhof Le Grand
        }
        else if(Mathf.Abs(hDir) < Mathf.Abs(vDir))
        {
            return vDir > 0 ? Direction.Up : Direction.Down;
        }
        else
        {
            return Direction.None;
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
		scientist.GetComponentInChildren<BillBoard>().EnableDrawCircle();
        activeZombie.active = false;
        player.SetState(Player.State.SWAPPING);
        InitZombieSelector();
		IEnumerable<GameObject> zombies = zombieSelector.GetZombiesAround();
        /*if (zombieSelector.GetZombiesAmount() == 1)
        {
            GameManager gm = GameManager.Instance;
            gm.ResetLevel();
            return;
        }*/

        foreach (GameObject z in zombies)
		{
			scientist.GetComponentInChildren<BillBoard> ().AddSelectable(z);
		}

        // Reset if no zombies (1 -> only scientist in buffer)
        if (zombieSelector.GetZombiesAmount() == 1)
        {
            GameManager gm = GameManager.Instance;
            gm.ResetLevel();
            return;
        }

        //TODO (Victor) : cache camera to avoid fetching
        CameraMovement camera = Camera.main.GetComponent<CameraMovement>();
        camera.SetTarget(scientist);
    }

    public void SetToControlling()
    {
		scientist.GetComponentInChildren<BillBoard>().DisableDrawCircle();
        activeZombie.active = true;
        player.SetState(Player.State.CONTROLLING);

        //TODO (Victor) : cache camera to avoid fetching
        CameraMovement camera = Camera.main.GetComponent<CameraMovement>();
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
