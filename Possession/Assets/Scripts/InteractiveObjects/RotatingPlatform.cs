using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	public class RotatingPlatform : Activable<Vector3> {
		public float speed;
		private Vector3 destination;
		private Vector3 currentRotation;

		void Start() {
			destination = steps [0];
			currentRotation = steps [0];
		}

		// Update is called once per frame
		// TODO Animate platform move 
		void Update () {
			float step = speed * Time.deltaTime;
			currentRotation = Vector3.Lerp (currentRotation, destination, step);
			transform.rotation = Quaternion.Euler (currentRotation);
		}

		/* Override OnValidate and set defaultValue before to call parent's OnValidate() to allow step creation in *
		 * unity UI */
		override
		public void OnValidate() {
			this.defaultValue = GetComponent<Transform> ().forward;
			base.OnValidate ();
		}

		override
		public void process (uint stepIdx) {
			destination = steps [stepIdx];
		}
	}

}
