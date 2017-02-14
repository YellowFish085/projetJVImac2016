using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargetTraking : MonoBehaviour {

    public GameObject target;
    
    private bool stop = false;
    private bool collided = false;
    private float offsetX;
    //private float heightObject;
    
    void Start () {
        //heightObject = gameObject.GetComponent<Collider2D>().bounds.size.x;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = target.transform.position;
        Vector3 thisPosition = gameObject.transform.position;
        Vector3 directionVector = targetPosition - thisPosition;

        //if (Mathf.Abs(directionVector.y) > heightObject)
        if (Mathf.Abs(directionVector.y) > 0)
            stop = (Mathf.Abs(directionVector.x) <= offsetX) ? true : false;
        else
            stop = collided;

        if (!stop)
        {
            float direction = Mathf.Sign(directionVector.x);
            gameObject.GetComponent<ZombieMovement>().Move(direction);
        }
        else if(stop)
            gameObject.GetComponent<ZombieMovement>().Move(0);

    }

    private void OnCollisionStay2D(Collision2D col)
    {
        collided = (col.gameObject == target) ? true : false; // Oui! Parce que stop = (col.gameObject == target) ça marche.
    }

    void SetTarget(GameObject newTarget)
    {
        this.target = newTarget;
        
        float deltaY = Mathf.Abs(target.transform.position.y - gameObject.transform.position.y);

        offsetX = (deltaY > heightObject) ? Random.Range(5, 10) : 0;
    }


}
