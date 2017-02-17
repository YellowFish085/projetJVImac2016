using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

	private Camera _camera;

	void Awake (){
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>() ;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
	}
}
