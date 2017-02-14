using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float smoothingMove = 5f;
    public float smoothingMoveEnd = 0.5f;
    public float deltaXToCurrent = 0f;
    public float deltaYToCurrent = 4f;

    private GameObject target;
    private Vector3 offset;
    private Vector3 targetCamPos = new Vector3(0, 0, 0);
	
	void FixedUpdate()
	{
        Vector3 newTargetCamPos = target.transform.position + offset;
        this.MoveCamera(newTargetCamPos);       
    }

    public void SetPosition(Vector3 pos)
    {
        this.MoveCamera(pos);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        offset = new Vector3(deltaXToCurrent, deltaYToCurrent, (transform.position.z - target.transform.position.z));
    }

    // TODO
    public void SetShaders()
    { }

    private void MoveCamera(Vector3 newTargetCamPos)
    {
        if (newTargetCamPos != targetCamPos)
        {
            targetCamPos = newTargetCamPos;
            transform.position = Vector3.Lerp(transform.position, newTargetCamPos, smoothingMove * Time.deltaTime);
        }
        else
            transform.position = Vector3.Lerp(transform.position, newTargetCamPos, smoothingMoveEnd * Time.deltaTime);
    }
}

