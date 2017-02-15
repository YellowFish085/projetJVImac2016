using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	/* Decorator to transform any Activator into a two states OnOffActivator *
	 * Please follow this steps to create an OnOffActivator with the decorator : *
	 * 	1. Create the Activable and the Activator and *
	 * 	2. Attach your Activable to the Activator created on step 1 *
	 * 	3. Attach OnOffDecorator script to the Activator created on step 1 *
	 * 	4. Attach the Activator itself to OnOffDecorator's activator attribute *
	 * 	5. The two steps are set in the activable */
	public class OnOffDecorator : Activator {

		public Activator activator;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnValidate() {
			if (this.activator != null && this.activator.attachedObject != null) {
				if (this.activator.attachedObject.getMaxNumberOfSteps() != 2) {
					this.activator.attachedObject.setMaxNumberOfSteps(2);
					this.activator.attachedObject.OnValidate ();
				}
			}
		}

		public void runStep() {
			activator.runStep ();
		}
	}

}