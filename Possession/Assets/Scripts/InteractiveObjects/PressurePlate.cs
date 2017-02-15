using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {
	/* Example class deriving Interruptor */

	public class PressurePlate : Activator {
			
		// Run step 1 of attachedObject when object on pressurePlate
		void OnCollisionEnter2D(Collision2D collision) {
			currentStep = 1;
			this.runStep();
		}

		// Run step 0 of attachedObject when object out of pressurePlate
		void OnCollisionExit2D(Collision2D collision) {
			currentStep = 0;
			this.runStep();
		}

		// Increment step
		public void NextStepOnInput() {
			if(currentStep < this.attachedObject.numberOfSteps) {
				currentStep++;
			} else {
				currentStep = this.attachedObject.numberOfSteps;
			}
		}

	}

}
