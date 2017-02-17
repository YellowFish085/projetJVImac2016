﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

	private Camera _camera;
	private bool drawCircle;
	public GameObject crossMark;

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
	}

	public void addSelectable(){
		GameObject go = Instantiate(crossMark, transform.position, transform.rotation);
		Debug.Log (go.transform.position);
	}
}
