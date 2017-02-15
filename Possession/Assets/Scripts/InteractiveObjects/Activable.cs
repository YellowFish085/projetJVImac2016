using System;
using UnityEngine;

namespace InteractiveObjects {

	/* There are a generic Activable class without template to facilitate use of Interruptor class 	*
	 * Template allow to define what is the steps type (Vector3 for a movingPlatform for example)	*
	 * You have to define this type in subclasses													*
	 * 																								*
	 * numberOfSteps : 	number of steps launched with the interruptor							 	*
	 * steps : 			value of each steps (e.g : for movingPlatform, positions where it moves 	*
	 * defaultValue : 	value used when you create a new step in steps array */

	public abstract class Activable : MonoBehaviour {

		public uint numberOfSteps = 0;
		protected int maxNumberOfSteps = -1;

		public abstract void process (uint stepIdx);

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public int getMaxNumberOfSteps() {
			return maxNumberOfSteps;
		}

		public void setMaxNumberOfSteps(int value) {
			maxNumberOfSteps = value;
		}

		virtual public void OnValidate() {
			
		}

	}
	
	public abstract class Activable<T> : Activable {

		protected T defaultValue;
		public T[] steps = new T[0];

		override
		public void OnValidate() {
			if (numberOfSteps != steps.Length || 
				(maxNumberOfSteps > 0 && numberOfSteps != maxNumberOfSteps)) {
				if (maxNumberOfSteps > 0 && numberOfSteps != maxNumberOfSteps) {
					numberOfSteps = (uint) maxNumberOfSteps;
				}

				int previousSize = steps.Length;
				Array.Resize<T>(ref this.steps, (int) numberOfSteps);
				for (int i = previousSize; i < numberOfSteps; i++) {
					steps [i] = this.defaultValue;
				}
			}
		}

	}

}
