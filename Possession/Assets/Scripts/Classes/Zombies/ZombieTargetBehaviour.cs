using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargetBehaviour : MonoBehaviour {

    public GameObject target = null;
    public string targetName; // TODO : Delete after test

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

    // For unactive temporary collision
    private int oldLayer;
    private int voidLayer;

    void Awake () {
        initParent = transform.parent.transform;
        oldLayer = gameObject.GetComponent<Collider2D>().gameObject.layer;
        voidLayer = LayerMask.NameToLayer("VoidCollision");

        Physics2D.IgnoreLayerCollision(voidLayer, voidLayer);
        this.SetTarget(GameObject.Find(targetName)); //TODO : Remove after test.
    }
	
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
    
    private void FollowTarget()
    {
        Vector3 directionVector = target.transform.position - gameObject.transform.position;
        if (!gripped)
        {
            if (Mathf.Abs(directionVector.y) >= 0)
                stop = (Mathf.Abs(directionVector.x) <= offsetX);
            else
                stop = gripped;

            float direction = 0;
            if (!stop)
                direction = Mathf.Sign(directionVector.x);

            gameObject.GetComponent<ZombieMovement>().Move(direction);
        }
        else
        {
            if(directionVector.y > 0) // if target jump
            {
                float step = gameObject.GetComponent<ZombieMovement>().jumpForce * Time.deltaTime;
                gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, localPosition, step);
            }
            else
            {
                gameObject.transform.localPosition = localPosition;
            }            
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        if(!gripped || !newTarget)
        {
            if(!newTarget && gripped)
            {
                ZombieLife targetLifeComponent = target.GetComponent<ZombieLife>();
                if(targetLifeComponent)
                    targetLifeComponent.RemoveAssailent(this.gameObject);

                UngrabTarget();
            }

            this.target = newTarget;
        }
        
        if(target)
        {
            float deltaY = Mathf.Abs(target.transform.position.y - gameObject.transform.position.y);

            gameObject.GetComponent<ZombieMovement>().Move(0);

            this.GetComponent<Collider2D>().gameObject.layer = voidLayer;
            offsetX = (deltaY > 0) ? Random.Range(5, 10) : 0;
        }
        else
            this.GetComponent<Collider2D>().gameObject.layer = oldLayer;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public bool GetGripped()
    {
        return gripped;
    }
    
    private void GrabTarget()
    {
        gripped = true;

        target.GetComponent<ZombieLife>().AddAssailant(this.gameObject);

        gameObject.transform.SetParent(target.transform);
        localPosition = gameObject.transform.localPosition;
        var zombieMovementComponent = target.GetComponent<ZombieMovement>();

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
