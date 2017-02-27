using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {
	/* Example class deriving Interruptor */

	public class PressurePlate : OnOffActivator {
			
		// Run step 1 of attachedObject when object on pressurePlate
		void OnCollisionEnter2D(Collision2D collision) {
			var contactPoint = collision.contacts[0].point;

			if (contactPoint.y >= GetComponent<Transform>().position.y
				&& collision.gameObject.GetComponent<Transform>().position.x > GetComponent<Transform>().position.x - GetComponent<Collider2D>().bounds.size.x / 2
				&& collision.gameObject.GetComponent<Transform>().position.x < GetComponent<Transform>().position.x + GetComponent<Collider2D>().bounds.size.x / 2)
			{
				currentStep = 1;
				this.runStep ();
			}
		}

		// Run step 0 of attachedObject when object out of pressurePlate
		void OnCollisionExit2D(Collision2D collision) {
			currentStep = 0;
			this.runStep();
		}

	}

}
