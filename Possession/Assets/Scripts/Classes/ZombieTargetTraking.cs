using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargetTraking : MonoBehaviour {

    public GameObject target;
    
    private static Vector3 rightDirection = new Vector3(1, 0, 0);
    private static Vector3 downDirection = new Vector3(0, -1, 0);

    private float currentSign = 0;
    private bool contact = false;

    // Use this for initialization
    void Start () {
        //TEMP
        target = GameObject.FindGameObjectWithTag("Scientist");
	}
	
	// Update is called once per frame
	void Update () {
        if(!contact)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 thisPosition = gameObject.transform.position;
            Vector3 directionVector = targetPosition - thisPosition;
            // Check horizontal direction.
            float direction = Mathf.Sign(Vector3.Dot(directionVector, rightDirection));
            if (direction != 0 && currentSign != direction)
            {
                currentSign = Mathf.Sign(direction);
                Debug.Log("direction = " + currentSign);
            }
            gameObject.GetComponent<ZombieMovement>().Move(currentSign);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        contact = (col.gameObject == target) ? true : false; // Oui! Parce que contact = (col.gameObject == target) ça marche.
    }

    void SetTarget(GameObject newTarget)
    {
        this.target = newTarget;
    }


}
