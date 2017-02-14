using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Possession;

public class UniqueObjectsHandler : MonoBehaviour {
    public GameObject scientistPrefab;
    public GameObject carrierPrefab;
    public GameObject gameplayNode;
    public GameObject playerControllerNode;

    public bool SetScientist(Transform sci)
    {
        if (gameplayNode.transform.Find("Actors/Scientist"))
        {
            Debug.Log("Scientist already exists");
            return false;
        }

        Transform actors = gameplayNode.transform.Find("Actors");
        GameObject scientist = Instantiate(scientistPrefab, sci.position, sci.rotation, actors);
        scientist.name = "Scientist";

        playerControllerNode.GetComponent<PlayerController>().scientist = scientist;

        return true;
    }

    public bool SetCarrierAsActiveZombie(Transform zombie)
    {
        if (gameplayNode.transform.Find("Actors/Carrier"))
        {
            Debug.Log("Carrier zombie already exists");
            return false;
        }

        Transform actors = gameplayNode.transform.Find("Actors");
        GameObject carrier = Instantiate(carrierPrefab, zombie.position, zombie.rotation, actors);
        carrier.name = "Carrier";
        carrier.tag = "Scientific";// TODO : remove after the carrier realy carries - for collider loader test and save. -- QC

        playerControllerNode.GetComponent<PlayerController>().activeZombie = carrier.GetComponent<ZombieMovement>();
        //GameObject.FindObjectOfType<CameraMovement>().UpdateTarget(); // TODO : remove when playerController observer -- QC
        return true;
    }
}
