using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private GameObject target;
    public float smoothingMove = 5f;
    public float smoothingMoveEnd = 0.5f;
    public float deltaXToCurrent = 0f;
    public float deltaYToCurrent = 4f;

    private Vector3 offset;
    private Vector3 targetCamPos = new Vector3(0, 0, 0);

    void Start ()
	{
        this.UpdateTarget();
    }
	
	void FixedUpdate()
	{
        Vector3 newTargetCamPos = target.transform.position + offset;
        this.MoveCamera(newTargetCamPos);       
    }

    public void SetPosition(Vector3 pos)
    {
        this.MoveCamera(pos);
    }

    // TODO
    public void SetShaders()
    { }

    public void UpdateTarget()
    {
        target = GameObject.FindObjectOfType<PlayerController>().activeZombie.gameObject;
        offset = new Vector3(deltaXToCurrent, deltaYToCurrent, (transform.position.z - target.transform.position.z));
    }

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

