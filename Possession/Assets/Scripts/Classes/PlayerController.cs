using UnityEngine;
using Possession;

public class PlayerController : MonoBehaviour {
    public ZombieMovement activeZombie;
    public GameObject scientist;
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
        player.SetState(Player.State.SWAPPING);
        InitZombieSelector();

        //TODO (Victor) : cache camera to avoid fetching
        CameraMovement camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        camera.SetTarget(scientist);
    }

    private void SetToControlling()
    {
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
