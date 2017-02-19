using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {
	/* Example class deriving Interruptor */

	public class PressurePlate : OnOffActivator {
			
		// Run step 1 of attachedObject when object on pressurePlate
		void OnTriggerEnter2D(Collider2D collision) {
				var elementCollide = collision.gameObject;
				var newElementPositionY = GetComponent<Collider2D>().bounds.max.y;
				var newElementPositionX = GetComponent<Collider2D> ().bounds.center.x;
				currentStep = 1;
				this.runStep ();
		}

		// Run step 0 of attachedObject when object out of pressurePlate
		void OnTriggerExit2D(Collider2D collision) {
			currentStep = 0;
			this.runStep();
		}

	}

}
