using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargetBehaviour : MonoBehaviour {

    public GameObject target;

    // abs of "penality" value
    private float speedWeight = 15f;
    private float jumpWeight = 15f;

    // initial parent for switch beetween parent of the different scene
    private Transform initParent;
    private Vector3 localPosition;

    // Boolean step
    private bool stop = false;
    private bool gripped = false;

    // Offset if target is up to this.
    private float offsetX = 0;
    //private Vector3 hitchToOffset;
    //private float heightObject;

    void Awake () {
        initParent = transform.parent.transform;
        this.target = GameObject.Find("Carrier");

        float deltaY = Mathf.Abs(target.transform.position.y - gameObject.transform.position.y);
        offsetX = (deltaY > 0) ? Random.Range(5, 10) : 0;
        //heightObject = gameObject.GetComponent<Csollider2D>().bounds.size.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (target)
            FollowTarget();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject == target)
            GrabTarget();
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject == target)
            UngrabTarget();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 thisPosition = gameObject.transform.position;
        Vector3 directionVector = targetPosition - thisPosition;
        if (!gripped)
        {
            //if (Mathf.Abs(directionVector.y) > heightObject)
            if (Mathf.Abs(directionVector.y) > 0)
                stop = (Mathf.Abs(directionVector.x) <= offsetX);
            else
                stop = gripped;

            if (!stop)
            {
                float direction = Mathf.Sign(directionVector.x);
                gameObject.GetComponent<ZombieMovement>().Move(direction);
            }
            else if (stop)
                gameObject.GetComponent<ZombieMovement>().Move(0);
        }
        else
        {
            if(directionVector.y > 0)
            {
                float step = gameObject.GetComponent<ZombieMovement>().maxSpeed * Time.deltaTime;
                gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, localPosition, step);
            }
            else
                gameObject.transform.localPosition = localPosition;

            //Debug.Log("LocalPosition = " + gameObject.transform.localPosition);
        }
       
    }

    public void SetTarget(GameObject newTarget)
    {
        this.target = newTarget;
        
        if(target)
        {
            float deltaY = Mathf.Abs(target.transform.position.y - gameObject.transform.position.y);

            // TODO : Add unActive Collider

            //offsetX = (deltaY > heightObject) ? Random.Range(5, 10) : 0;
            offsetX = (deltaY > 0) ? Random.Range(5, 10) : 0;
        }
        
    }

    public bool GetGripped()
    {
        return gripped;
    }
    
    private void GrabTarget()
    {
        gripped = true;

        gameObject.transform.SetParent(target.transform);
        localPosition = gameObject.transform.localPosition;

        Debug.Log("LocalPosition = " + gameObject.transform.localPosition);
        //hitchToOffset = gameObject.transform.localPosition - target.transform.localPosition;
        var zombieMovementComponent = target.GetComponent<ZombieMovement>();
        Debug.Log("Grab it");
        //Debug.Log("gameObject.transform.localPosition = " + thisTargetPoint + " -- target.transform.localPosition = " + targetPoint);
        zombieMovementComponent.IncrementSpeedWeight(-speedWeight);
        zombieMovementComponent.IncrementJumpWeight(-jumpWeight);
    }

    private void UngrabTarget()
    {
        gripped = false;
        gameObject.transform.SetParent(initParent);
        var zombieMovementComponent = target.GetComponent<ZombieMovement>();
        zombieMovementComponent.IncrementSpeedWeight(speedWeight);
        zombieMovementComponent.IncrementJumpWeight(jumpWeight);
    }


}
