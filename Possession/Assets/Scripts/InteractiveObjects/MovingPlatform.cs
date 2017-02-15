using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	public class MovingPlatform : Activable<Vector3> {

		// Update is called once per frame
		// TODO Animate platform move 
		void Update () {

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
			Debug.Log ("I move my ass to " + steps [stepIdx].x + ", " + steps [stepIdx].y);
			GetComponent<Transform> ().position = steps [stepIdx];
		}
	}

}
