using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCannibal : MonoBehaviour
{
    public float frontDetectionDistance = 25;
    public float backDetectionDistance = 5;
    public float viewAngle = 90;
    public int deltaDetection = 10;
    
    private ZombieTargetBehaviour targetBehaviour;
    private ZombieMovement zombieMovement;
    private float heightObject;
    private float widthObject;
    private RaycastHit2D hit;

    private int voidLayer;

    private void Awake()
    {
        voidLayer = LayerMask.NameToLayer("VoidCollision");
        this.GetComponent<Collider2D>().gameObject.layer = voidLayer;
    }

    void Start ()
    {
        heightObject = gameObject.GetComponent<Collider2D>().bounds.size.y;
        widthObject = gameObject.GetComponent<Collider2D>().bounds.size.x;
        targetBehaviour = gameObject.GetComponent<ZombieTargetBehaviour>();
        zombieMovement = gameObject.GetComponent<ZombieMovement>();
        //        this.SetTarget(GameObject.FindObjectOfType<UniqueObjectsHandler>().GetComponent<PlayerController>().gameObject);
        //        target = GameObject.FindObjectOfType<UniqueObjectsHandler>().GetComponent<PlayerController>().gameObject;
    }

    void Update ()
    {
        Debug.Log("Update");
        if(!targetBehaviour.GetGripped())
        {
            Vector2 raycastOrigin = new Vector2(transform.position.x + zombieMovement.GetDirection() * widthObject, transform.position.y + heightObject);
            checkFront(raycastOrigin);

            //raycastOrigin = new Vector2(transform.position.x, transform.position.y + heightObject);
            //checkBack(raycastOrigin);
        }
    }

    private void checkFront(Vector2 raycastOrigin)
    {
        Vector2 viewDirection = new Vector2(frontDetectionDistance * zombieMovement.GetDirection(), 0);
        int stepAngle = (int)(viewAngle / deltaDetection);

        GameObject targetGM = null;
        bool hitATarget = false;
        // Re-code of the Physics2D.Raycast cause doesn't work with ground layer when use ignore a layer. 
        for (int i = -stepAngle / 2; i <= stepAngle / 2; i++)
        {
            float minDist = Single.MaxValue;
            Vector2 target = Quaternion.Euler(0, 0, i * deltaDetection) * viewDirection;
            RaycastHit2D[] rayHits = Physics2D.RaycastAll(raycastOrigin, target, frontDetectionDistance);
            for (int j = 0; j < rayHits.Length; ++j)
            {
                float distance = Mathf.Abs(rayHits[j].point.x - transform.position.x);
                if (distance < minDist && rayHits[j].collider.gameObject.layer != voidLayer)
                {
                    minDist = distance;
                    Debug.Log("rayHits[" + j + "] = " + rayHits[j].collider.gameObject.layer);
                    if (rayHits[j].collider != null && rayHits[j].collider.gameObject.tag == "Zombie") // TODO : probably change the tag with the evolution
                    {
                        hitATarget = true;
                        targetGM = rayHits[j].collider.gameObject;
                    }
                }
            }

            Debug.DrawRay(raycastOrigin, target);
        }

        if (hitATarget)
            targetBehaviour.SetTarget(targetGM);
                
    }

    private void checkBack(Vector2 raycastOrigin)
    {
        Vector2 backDirection = backDetectionDistance * -1 * zombieMovement.GetDirection() * Vector2.right;
        hit = Physics2D.Raycast(raycastOrigin, backDirection);
        Debug.Log("backDirection = " + backDirection);
        if (hit.collider != null && hit.collider.gameObject.tag == "Zombie") // TODO : probably change the tag with the evolution
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            Debug.Log("distance = " + distance);
            if (distance < backDetectionDistance)
                targetBehaviour.SetTarget(hit.collider.gameObject);
        }
        Debug.DrawRay(raycastOrigin, backDirection);
    }
}
