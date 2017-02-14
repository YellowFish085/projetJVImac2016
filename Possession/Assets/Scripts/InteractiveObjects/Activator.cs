﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveObjects {

	/* Generic Interruptor, create subclass to define the behaviour when player activate it calling runStep() *
	 * (see PressurePlate) *
	 * currentStep : the step launched when runStep() si called *
	 * attachedObject : Activable object linked to the Interruptor. You can attach object directly in Unity in *
	 * 					drag 'n drop *
	 * NB : To get the number of steps of the attachedObject use attachedObject.numberOfSteps */

	public abstract class Activator : MonoBehaviour {

		protected uint currentStep = 0;
		public Activable attachedObject = null;

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void attach(Activable item) {
			this.attachedObject = item;
		}

		void detach() {
			this.attachedObject = null;
		}

		// Use this when you want to run a step
		protected void runStep() {
			Debug.Log ("I run the step " + currentStep);
			attachedObject.process(currentStep);
		}
	}

}