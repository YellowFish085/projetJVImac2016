using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {
	/* Example class deriving Interruptor */

	public class IncrementalPressurePlate : Activator {

		// Increment step on input
		private void NextStepOnInput() {
			if(currentStep < this.attachedObject.numberOfSteps) {
				currentStep++;
			} else {
				currentStep = 0;
			}
			this.runStep();
		}

		// Decrement step on input
		private void PreviousStepOnInput() {
			if(currentStep > 0) {
				currentStep--;
			} else {
				currentStep = this.attachedObject.numberOfSteps;
			}
			this.runStep();
		}

	}

}