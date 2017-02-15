using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	public class SwitchButton : Activator {

		// Set currentStep value according to Fire2 button state
		void Update() {
			if (Input.GetButtonDown ("Fire2")) {
				currentStep = 1;
				this.runStep ();
			} else {
				currentStep = 0;
				this.runStep();
			}
		}

	}

}