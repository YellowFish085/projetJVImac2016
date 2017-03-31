using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCarrier : MonoBehaviour {

    public float minDistToCarry = 10;

    private GameObject scientist;

    // initial parent for switch beetween parent of the different scene
    private Transform initialScientistParent;
    static private Vector3 localPosition;

    // verif contact with scientist
    private bool canCarry = false;
    private bool carry = false;
    

    // Use this for initialization
    void Start () {
        scientist = GameObject.FindGameObjectWithTag("Scientist");
        localPosition = new Vector3(-2, 2, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if(!carry)
            VerifDistance();
        else
            scientist.transform.localPosition = localPosition;
        /*if(!carry && contactScientist)
        {
            c
        }*/
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        canCarry = col.gameObject.name == "Scientist";
    }

    public bool GetCarry()
    {
        return carry;
    }

    public void Carry()
    {
        if(canCarry && !carry)
        {
            initialScientistParent = scientist.transform.parent;
            scientist.transform.SetParent(gameObject.transform);
            scientist.transform.localPosition = localPosition;
            carry = true;
        }
        else if(carry)
        {
            scientist.transform.SetParent(initialScientistParent);
            carry = false;
        }
    }

    private void VerifDistance()
    {
        float distance = Mathf.Abs(scientist.transform.position.x - transform.position.x);
        canCarry = (distance <= minDistToCarry);
    }
}
