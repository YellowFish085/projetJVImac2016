using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CircularBuffer;

public class BillBoard : MonoBehaviour {

	private Camera _camera;
	private bool drawCircle;
	public GameObject crossMark;
	private List<GameObject> crossMarks = new List<GameObject>();

	void Awake (){
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>() ;
		drawCircle = false;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().enabled = drawCircle;
		transform.LookAt (transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			Debug.Log (hit.collider);
		}
	}

	public void enableDrawCircle(){
		drawCircle = true; 
	}

	public void disableDrawCircle(){
		drawCircle = false;
		deleteSelectables ();
	}

	public void addSelectable(GameObject zombie){
		Vector3 offset = zombie.transform.position - transform.position;
		offset.Normalize ();
		offset *= 10;
		Vector3 newPosition = transform.position + offset;
		newPosition.Set(newPosition.x, newPosition.y, transform.position.z);


		GameObject go = Instantiate(crossMark, newPosition, transform.rotation);
		Debug.Log(zombie.GetComponent<ZombieMovement>());
		go.GetComponent<checkMark>().referencedZombie = zombie.GetComponent<ZombieMovement> ();
		crossMarks.Add (go);
	}

	public void deleteSelectables(){
		foreach (GameObject cross in crossMarks) {
			Destroy (cross);
		}
	}

}
