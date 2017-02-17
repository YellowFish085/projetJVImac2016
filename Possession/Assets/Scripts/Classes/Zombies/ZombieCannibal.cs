using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCannibal : MonoBehaviour {

    private GameObject target;
    private bool locked = false;
    private ZombieTargetBehaviour targetBehaviour;

	// Use this for initialization
	void Start () {
        targetBehaviour = gameObject.GetComponent<ZombieTargetBehaviour>();
        target = GameObject.FindObjectOfType<UniqueObjectsHandler>().GetComponent<PlayerController>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetTarget(GameObject newTarget)
    {
      //  controlledZombie
    }
}
