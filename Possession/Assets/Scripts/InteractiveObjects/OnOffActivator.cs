using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	/* Generic two states Activator. To create a On/Off Activator you can derive this class */
	public class OnOffActivator : Activator {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		void OnValidate() {
			if (attachedObjects != null) {
				foreach (var attachedObject in attachedObjects) {
					if (attachedObject != null) {
						if (attachedObject.getMaxNumberOfSteps () != 2) {
							attachedObject.setMaxNumberOfSteps (2);
							attachedObject.OnValidate ();
						}
					}
				}
			}
		}
	}
}
