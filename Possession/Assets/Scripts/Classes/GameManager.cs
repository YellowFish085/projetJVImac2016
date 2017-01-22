using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Possession {

	public class GameManager : MonoBehaviour
	{
		public enum State {PAUSE, IN_GAME, MAIN_MENU};

		private State _state;
		private static GameManager _instance;
		private static object _lock = new object();
        //private Hashtable _levelList = new Hashtable();
        private List<string> _levelList = new List<string>();
        private Player _player;
		private Camera _camera;

		public static GameManager Instance
		{
			get
			{
				if (applicationIsQuitting) {
					Debug.LogWarning("[Singleton] Instance '"+ typeof(GameManager) +
						"' already destroyed on application quit." +
						" Won't create again - returning null.");
					return null;
				}

				lock(_lock)
				{
					if (_instance == null)
					{
						_instance = (GameManager) FindObjectOfType(typeof(GameManager));

						if ( FindObjectsOfType(typeof(GameManager)).Length > 1 )
						{
							Debug.LogError("[Singleton] Something went really wrong " +
								" - there should never be more than 1 singleton!" +
								" Reopening the scene might fix it.");
							return _instance;
						}

						if (_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<GameManager>();
							singleton.name = "(singleton) "+ typeof(GameManager).ToString();

							DontDestroyOnLoad(singleton);

							_instance.Init ();

							Debug.Log("[Singleton] An instance of " + typeof(GameManager) + 
								" is needed in the scene, so '" + singleton +
								"' was created with DontDestroyOnLoad.");
						} else {
							Debug.Log("[Singleton] Using instance already created: " +
								_instance.gameObject.name);
						}
					}
					return _instance;
				}
			}
		}

		private static bool applicationIsQuitting = false;
		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public void OnDestroy () {
			applicationIsQuitting = true;
		}

		private void Init () {
			_instance.RetrieveLevels ();

		}

		private void RetrieveLevels () {
            /*var numScenes = SceneManager.sceneCount;  // Work only if the scene is loaded in the current.

			for (int i=0; i < numScenes; ++i)
			{
				_levelList.Add(SceneManager.GetSceneAt(i).name, SceneManager.GetSceneAt(i));
			}*/

            int i = 0;
            foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
            {
                if (S.enabled)
                {
                    string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - 6);
                    Debug.Log("sceneName = " + name);
                    _levelList.Add(name);
                    ++i;
                }
            }

            Debug.Log("List levels size : " + _levelList.Count);
		}

        /* Scene Managment */
        public void LoadScene(string sceneName)
        {
            Debug.Log("Load = " + sceneName);
            Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
            if (!sceneToLoad.isLoaded)
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnloadScene(string sceneName)
        {
            Debug.Log("Unload = " + sceneName);
            Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
            if (sceneToLoad.isLoaded)
                SceneManager.UnloadSceneAsync(sceneName);
        }
        /* --------------- */
    }
} // namespace Possession
