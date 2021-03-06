﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSeductive : MonoBehaviour {

    private void Awake() {
        var model = transform.FindChild("Cylinder_000_Cylinder_001");
        var material = model.GetComponent<SkinnedMeshRenderer>().material;

        Texture nTex = Resources.Load("zombieTextures/seductive") as Texture;

        material.SetTexture("_MainTex", nTex);
    }

    public void Seduce () {
        List<GameObject> zombies = GetZombiesInBoundaries();

        foreach (GameObject currentZombie in zombies)
        {
            Debug.Log("Seduce");
            if (currentZombie.GetComponent<ZombieTargetBehaviour>())
            {
                currentZombie.GetComponent<ZombieTargetBehaviour>().SetTarget(gameObject);
            }
        }
        
	}
	
	// Update is called once per frame
	private List<GameObject> GetZombiesInBoundaries () {
        List<GameObject> zombies = new List<GameObject>();
        foreach (GameObject currentZombie in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            if(Vector3.Distance(currentZombie.transform.position, transform.position) < 30 
                && !currentZombie.gameObject.HasTag("Seductive")
                && (currentZombie.name != "Carrier"))
            {
                zombies.Add(currentZombie);
            }
        }

        return zombies;
    }
}
