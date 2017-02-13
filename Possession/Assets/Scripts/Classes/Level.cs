using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Possession {
	public class Level : MonoBehaviour {
        private Scene currentLevel;
		public string nextLevel;
		public string previousLevel;

        void Start()
        {
            currentLevel = gameObject.scene;
        }

        public string GetNameCurrentName()
        {
            return currentLevel.name;
        }
    }
} // namespace Possession
