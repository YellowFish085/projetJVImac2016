using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

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