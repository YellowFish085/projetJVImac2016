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
		Debug.Log (offset);
		GameObject go = Instantiate(crossMark, transform.position+offset, transform.rotation);
		crossMarks.Add (go);

	}

	public void deleteSelectables(){
		foreach (GameObject cross in crossMarks) {
			Destroy (cross);
		}
	}
}
