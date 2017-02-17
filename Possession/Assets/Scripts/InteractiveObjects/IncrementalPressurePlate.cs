using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {
	/* Example class deriving Interruptor */

	public class IncrementalPressurePlate : Activator {

		// Call NextStep on collision
		void OnCollisionEnter2D(Collision2D collision) {
			var contactPoint = collision.contacts[0].point;

			if (contactPoint.y >= GetComponent<Transform> ().position.y
			    && collision.gameObject.GetComponent<Transform> ().position.x > GetComponent<Transform> ().position.x - GetComponent<Collider2D> ().bounds.size.x / 2
			    && collision.gameObject.GetComponent<Transform> ().position.x < GetComponent<Transform> ().position.x + GetComponent<Collider2D> ().bounds.size.x / 2) 
			{
				NextStep ();
			}
		}

		// Increment step
		private void NextStep() {
			uint maxStep = 0;
			foreach (var attachedObject in attachedObjects) {
				if (attachedObject.numberOfSteps > maxStep) {
					maxStep = attachedObject.numberOfSteps;
				}
			}

			if (currentStep < maxStep - 1) {
				currentStep++;
			} else {
				currentStep = 0;
			}

			foreach (var attachedObject in attachedObjects) {
				this.runStep ();
			}
		}

	}

}