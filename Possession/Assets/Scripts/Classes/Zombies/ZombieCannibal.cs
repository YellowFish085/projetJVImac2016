using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCannibal : MonoBehaviour
{
    public float power = 1;
    public float frontDetectionDistance = 25; // View far distance 
    public float backDetectionDistance = 10; // Detection distance to back (equivalente of hearing)
    public float viewAngle = 90;
    public int deltaDetection = 10;

    public string zombieTagToVerify = "Zombie";
    
    private ZombieTargetBehaviour targetBehaviourComponent;
    private ZombieMovement zombieMovementComponent;
    private float heightObject;
    private float widthObject;

    private int itsLayer;
    
    void Start ()
    {
        itsLayer = LayerMask.NameToLayer("CannibalLayer");
        this.GetComponent<Collider2D>().gameObject.layer = itsLayer;
        Physics2D.IgnoreLayerCollision(itsLayer, itsLayer);

        heightObject = gameObject.GetComponent<Collider2D>().bounds.size.y;
        widthObject = gameObject.GetComponent<Collider2D>().bounds.size.x;
        targetBehaviourComponent = gameObject.GetComponent<ZombieTargetBehaviour>();
        zombieMovementComponent = gameObject.GetComponent<ZombieMovement>();

        var model = transform.FindChild("Cylinder_000_Cylinder_001");
        var material = model.GetComponent<SkinnedMeshRenderer>().material;

        Texture nTex = Resources.Load("zombieTextures/cannibal") as Texture;

        material.SetTexture("_MainTex", nTex);
    }

    void Update ()
    {
        if (!targetBehaviourComponent.GetGripped())
        {
            Vector2 raycastOrigin = new Vector2(transform.position.x + zombieMovementComponent.GetDirection() * widthObject, transform.position.y + heightObject);
            bool res = checkFront(raycastOrigin);

            if(!res)
            {
                raycastOrigin = new Vector2(transform.position.x, transform.position.y + heightObject);
                checkBack(raycastOrigin);
            }            
        }
        else
        {
            GameObject target = targetBehaviourComponent.GetTarget();
            ZombieLife targetLifeComponent = target.GetComponent<ZombieLife>();
            if(targetLifeComponent.IsAlive())
            {
                float step = -power * Time.deltaTime;
                targetLifeComponent.IncrementLife(step);
            }
        }
    }

    private bool checkFront(Vector2 raycastOrigin)
    {
        Vector2 viewDirection = new Vector2(frontDetectionDistance * zombieMovementComponent.GetDirection(), 0);
        int stepAngle = (int)(viewAngle / deltaDetection);

        GameObject targetGM = null;
        // Re-code of the Physics2D.Raycast cause doesn't work with ground layer when use ignore a layer. 
        for (int i = -stepAngle / 2; i <= stepAngle / 2; i++)
        {
            float minDist = Single.MaxValue;
            Vector2 target = Quaternion.Euler(0, 0, i * deltaDetection) * viewDirection;

            RaycastHit2D[] rayHits = Physics2D.RaycastAll(raycastOrigin, target, frontDetectionDistance);

            GameObject tmpTargetGM = this.CheckRayCast(rayHits, minDist);
            if (tmpTargetGM)
                targetGM = tmpTargetGM;

            Debug.DrawRay(raycastOrigin, target);
        }

        if(targetGM)
        {
            this.SetTarget(targetGM);
            return true;     
        }
        return false;
    }

    private void checkBack(Vector2 raycastOrigin)
    {
        Vector2 backDirection = backDetectionDistance * -1 * zombieMovementComponent.GetDirection() * Vector2.right;
        RaycastHit2D[] rayHits = Physics2D.RaycastAll(raycastOrigin, backDirection, backDetectionDistance);
        GameObject targetGM = this.CheckRayCast(rayHits);

        if(targetGM)
            this.SetTarget(targetGM);

        Debug.DrawRay(raycastOrigin, backDirection);
    }

    private GameObject CheckRayCast(RaycastHit2D[] rayHits, float minDist = Single.MaxValue)
    {
        GameObject targetGM = null;
        for (int j = 0; j < rayHits.Length; ++j)
        {
            float distance = Mathf.Abs(rayHits[j].point.x - transform.position.x);
            if (distance < minDist && rayHits[j].collider.gameObject.layer != this.gameObject.layer)
            {
                minDist = distance;
                if (rayHits[j].collider != null
                    && rayHits[j].collider.gameObject.tag == zombieTagToVerify
                    && !rayHits[j].collider.gameObject.gameObject.HasTag("Cannibal")) // TODO : probably change the tag with the evolution
                {
                    targetGM = rayHits[j].collider.gameObject;
                }
            }
        }
        return targetGM;
    }

    public void SetTarget(GameObject target)
    {
        /*if (!target)
            return;*/

        targetBehaviourComponent.SetTarget(target);
        this.GetComponent<Collider2D>().gameObject.layer = itsLayer;
    }
}
