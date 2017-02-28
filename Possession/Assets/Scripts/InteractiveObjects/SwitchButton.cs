using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	public class SwitchButton : Activator {

		private bool onButton;

		// Set currentStep value according to Fire2 button state
		void Update() {
			if (onButton && Input.GetButtonDown ("Fire2")) {
				if (currentStep == 0) {
					currentStep = 1;
				} else {
					currentStep = 0;
				}
				this.runStep ();
			}
		}

		void OnTriggerEnter2D(Collider2D collision) {
			onButton = true;
		}

		void OnTriggerExit2D(Collider2D collision) {
			onButton = false;
		}

	}

}