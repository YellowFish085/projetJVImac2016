using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    public GameObject player; // TODO: remove and remplace player by GameManger::player::Actors::BasePantin.
    public float smoothingMove = 5f;
    public float smoothingMoveEnd = 0.5f;
    public float deltaXToCurrent = 0f;
    public float deltaYToCurrent = 4f;

    private Vector3 offset;
    private Vector3 targetCamPos = new Vector3(0, 0, 0);

    void Start ()
	{
        offset = new Vector3(deltaXToCurrent, deltaYToCurrent, (transform.position.z - player.transform.position.z));
    }
	
	void FixedUpdate()
	{
        Vector3 newTargetCamPos = player.transform.position + offset;
        this.MoveCamera(newTargetCamPos);       
    }

    public void SetPosition(Vector3 pos)
    {
        this.MoveCamera(pos);
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

