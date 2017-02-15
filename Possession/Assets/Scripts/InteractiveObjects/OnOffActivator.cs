using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {
	public class OnOffActivator : Activator {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		void OnValidate() {
			if (this.attachedObject != null) {
				if (attachedObject.getMaxNumberOfSteps() != 2) {
					attachedObject.setMaxNumberOfSteps(2);
					attachedObject.OnValidate ();
				}
			}
		}
	}
}
