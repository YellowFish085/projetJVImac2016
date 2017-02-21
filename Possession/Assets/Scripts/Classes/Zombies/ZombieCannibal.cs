using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCannibal : MonoBehaviour
{
    public float frontDetectionDistance = 25;
    public float backDetectionDistance = 5;
    public float viewAngle = 90;
    public int deltaDetection = 10;
//    private GameObject target;
    private bool locked = false;
    private ZombieTargetBehaviour targetBehaviour;
    private ZombieMovement zombieMovement;
    //private RaycastHit2D hit;
    private float heightObject;
    private float widthObject;
    private RaycastHit2D hit;

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
            //PlayerController playerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
            //GameObject currentZombie = playerController.activeZombie.gameObject;
            Vector2 raycastOrigin = new Vector2(transform.position.x + zombieMovement.GetDirection() * widthObject, transform.position.y + heightObject);
            checkFront(raycastOrigin);
            checkBack(raycastOrigin);
        }
    }

    private void checkFront(Vector2 raycastOrigin)
    {
        Vector2 viewDirection = new Vector2(frontDetectionDistance * zombieMovement.GetDirection(), 0);
        int stepAngle = (int)(viewAngle / deltaDetection);
        for (int i = -stepAngle / 2; i <= stepAngle / 2; i++)
        {
            Vector2 target = Quaternion.Euler(0, 0, i * deltaDetection) * viewDirection;
            hit = Physics2D.Raycast(raycastOrigin, target);
            if (hit.collider != null && hit.collider.gameObject.tag == "Zombie")
            {
                float distance = Mathf.Abs(hit.point.x - transform.position.x);
                if (distance < frontDetectionDistance)
                    targetBehaviour.SetTarget(hit.collider.gameObject);
            }
            Debug.DrawRay(raycastOrigin, target);
        }
    }

    private void checkBack(Vector2 raycastOrigin)
    {
        Vector2 viewDirection = new Vector2(backDetectionDistance * -1 * zombieMovement.GetDirection(), 0);
        hit = Physics2D.Raycast(raycastOrigin, viewDirection);
        if (hit.collider != null && hit.collider.gameObject.tag == "Zombie")
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            if (distance < frontDetectionDistance)
                targetBehaviour.SetTarget(hit.collider.gameObject);
        }
        Debug.DrawRay(raycastOrigin, viewDirection);
    }

    /*private new void SetTarget(GameObject newTarget)
    {
        //if (!locked)
        //    base.SetTarget(newTarget);
      //  controlledZombie
    }*/
}
