using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCarrier : MonoBehaviour {

    GameObject scientist;

    // initial parent for switch beetween parent of the different scene
    private Transform initialScientistParent;
    static private Vector3 localPosition = new Vector3(0, 1, 0);

    // verif contact with scientist
    private bool contactScientist = false;
    private bool carry = false;
    

    // Use this for initialization
    void Start () {
        scientist = GameObject.FindGameObjectWithTag("Scientist");
    }
	
	// Update is called once per frame
	void Update () {
		/*if(!carry && contactScientist)
        {
            c
        }*/
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        contactScientist = col.gameObject.name == "Scientist";
    }

    public void Carry()
    {
        if(contactScientist)
        {
            gameObject.transform.SetParent(scientist.transform);
            localPosition = gameObject.transform.localPosition;
        }
    }
}
