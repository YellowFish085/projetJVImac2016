using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	public class MovingPlatform : Activable<Vector3> {
		public float speed;
		private Vector3 destination;

		void Start() {
			destination = steps [0];
		}

		// Update is called once per frame
		// TODO Animate platform move 
		void Update () {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, destination, step);
		}

		/* Override OnValidate and set defaultValue before to call parent's OnValidate() to allow step creation in *
		 * unity UI */
		override
		public void OnValidate() {
			this.defaultValue = GetComponent<Transform> ().position;
			base.OnValidate ();
		}

		override
		public void process (uint stepIdx) {
			destination = steps [stepIdx];
		}
	}

}
