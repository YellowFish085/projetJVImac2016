using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CircularBuffer;

public class BillBoard : MonoBehaviour {

	private Camera _camera;
	private bool drawCircle;
	public GameObject crossMark;
	private List<GameObject> crossMarks = new List<GameObject>();

    private PlayerController playerController;

    //DEBUG WITHOUT CONTROLLER
    public bool debug = false;

    [Range(-1f, 1f)]
    public float joystick_x = 0f;
    [Range(-1f, 1f)]
    public float joystick_y = 0f;

    public Vector3 joystick_direction;

    void Awake () {
        _camera = Camera.main.GetComponent<Camera>();
        drawCircle = false;

        playerController = GameObject.Find("Management/PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
		GetComponent<SpriteRenderer>().enabled = drawCircle;
		transform.LookAt (transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);

        float x, y;
        if (debug)
        {
            x = joystick_x;
            y = joystick_y;
        } else
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }

        joystick_direction = new Vector3(x, y, 0);
        joystick_direction.Normalize();
        joystick_direction *= 10;

        Vector3 point = transform.position + joystick_direction;

        if(crossMarks.Count > 0 && joystick_direction != Vector3.zero)
        {
            GameObject selectedCm = crossMarks.Aggregate(
                (c, d) => Vector3.Distance(c.transform.position, point) < Vector3.Distance(d.transform.position, point) ? c : d
            );

            if (Input.GetButtonDown("Jump"))
            {
                playerController.activeZombie = selectedCm.GetComponent<CheckMark>().referencedZombie;
                playerController.SetToControlling();
            }
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + joystick_direction);
    }

    public void EnableDrawCircle(){
		drawCircle = true; 
	}

	public void DisableDrawCircle(){
		drawCircle = false;
		DeleteSelectables ();
	}

	public void AddSelectable(GameObject zombie){
		Vector3 offset = zombie.transform.position - transform.position;
		offset.Normalize ();
		offset *= 10; //radius of the circle
		Vector3 newPosition = transform.position + offset;
		newPosition.Set(newPosition.x, newPosition.y, transform.position.z);


		GameObject go = Instantiate(crossMark, newPosition, transform.rotation);
		go.GetComponent<CheckMark>().referencedZombie = zombie.GetComponent<ZombieMovement> ();
		crossMarks.Add (go);
	}

	public void DeleteSelectables(){
		foreach (GameObject cross in crossMarks) {
			Destroy (cross);
		}
        crossMarks.Clear();
	}
}
