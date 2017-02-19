using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {
	/* Example class deriving Interruptor */

	public class PressurePlate : OnOffActivator {

		private bool triggered = false;
		private float speed = 5;
		private Vector3 sinkPosition;
		private Vector3 originPosition;

		void Start() {
			originPosition = GetComponent<Transform> ().position;
			sinkPosition = originPosition;
			sinkPosition.y = sinkPosition.y - GetComponent<Collider2D>().bounds.extents.y;
		}

		void Update() {
			var newPosition = originPosition;
			if (triggered) {
				newPosition = sinkPosition;
			}

			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (
				transform.position, 
				newPosition, 
				step
			);

			this.runStep ();
		}
			
		// Run step 1 of attachedObject when object on pressurePlate
		void OnTriggerEnter2D(Collider2D collision) {
				var elementCollide = collision.gameObject;
				var newElementPositionY = GetComponent<Collider2D>().bounds.max.y;
				var newElementPositionX = GetComponent<Collider2D> ().bounds.center.x;
				currentStep = 1;
				triggered = true;
		}

		// Run step 0 of attachedObject when object out of pressurePlate
		void OnTriggerExit2D(Collider2D collision) {
			currentStep = 0;
			triggered = false;
		}

	}

}
