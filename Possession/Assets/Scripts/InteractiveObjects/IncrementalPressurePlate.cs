using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {
	/* Example class deriving Interruptor */

	public class IncrementalPressurePlate : Activator {

		// Call NextStep on collision
		void OnCollisionExit2D(Collision2D collision) {
			NextStep ();
		}

		// Increment step
		private void NextStep() {
			if(currentStep < this.attachedObject.numberOfSteps) {
				currentStep++;
			} else {
				currentStep = 0;
			}
			this.runStep();
		}

	}

}