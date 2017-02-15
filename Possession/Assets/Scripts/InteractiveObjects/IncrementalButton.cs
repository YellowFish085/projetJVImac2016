using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	public class IncrementalButton : Activator {

		private bool isAlreadyActive = false;

		// Call NextStep method if Fire2 button is not already active
		void Update() {
			if (!Input.GetButtonDown ("Fire2")) {
				isAlreadyActive = true;
				NextStep ();
			} else {
				isAlreadyActive = false;
			}
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