using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCannibal : MonoBehaviour
{
    public float detectionDistance = 25;
//    private GameObject target;
    private bool locked = false;
    private ZombieTargetBehaviour targetBehaviour;
    private RaycastHit2D hit;
    private float heightObject;

    void Start ()
    {
        heightObject = gameObject.GetComponent<Collider2D>().bounds.size.y;
        targetBehaviour = gameObject.GetComponent<ZombieTargetBehaviour>();
        //        this.SetTarget(GameObject.FindObjectOfType<UniqueObjectsHandler>().GetComponent<PlayerController>().gameObject);
        //        target = GameObject.FindObjectOfType<UniqueObjectsHandler>().GetComponent<PlayerController>().gameObject;
    }

    void Update ()
    {
        Debug.Log("Update");
        //GameObject target = targetBehaviour.target.gameObject;
        //float currentDistance = Vector3.Distance(target.transform.position, transform.position);
        //if (currentDistance <= detectionDistance)
        float direction = Mathf.Sign(Vector2.right.x - 2 * transform.right.x); // to retrive the direction cause transform.forward doesn't work, because there is a rotation on the gameobject (I think)
        Vector2 origin = new Vector2(transform.position.x, transform.position.y + heightObject);
        Vector2 target = new Vector2(5 * transform.localScale.x, 0);
        hit = Physics2D.Raycast(origin, target);
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x);
            Debug.Log("COLLID = " + distance);
        }

        Debug.DrawRay(origin, target);
    }

    /*private new void SetTarget(GameObject newTarget)
    {
        //if (!locked)
        //    base.SetTarget(newTarget);
      //  controlledZombie
    }*/
}
