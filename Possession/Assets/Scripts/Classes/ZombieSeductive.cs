using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSeductive : MonoBehaviour {

	public void Seduce () {
        foreach (GameObject currentZombie in GetZombiesInBoundaries())
        {
            //currentZombie.GetComponent<ZombieTargetTracking>().SetTarget(this);
        }
        
	}
	
	// Update is called once per frame
	private List<GameObject> GetZombiesInBoundaries () {
        List<GameObject> zombies = new List<GameObject>();
        foreach (GameObject currentZombie in GameObject.FindGameObjectsWithTag("zombie"))
        {
            if(Vector3.Distance(currentZombie.transform.position, transform.position) < 30)
            {
                zombies.Add(currentZombie);
            }
        }

        return zombies;
    }
}
